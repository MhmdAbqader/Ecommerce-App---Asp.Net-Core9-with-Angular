
using System.Text;
using Ecommerce.Api.CustomMiddleware;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Core.Services;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Implementation;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Ecommerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Swagger Authorization",
                    Description = "JWt Token Authorize Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                swagger.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
                 swagger.AddSecurityRequirement(securityRequirement);
            }); 

            // database connection configuration
            builder.Services.AddDbContext<ApplicationDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("_cn"));
            });

            // Identity configuration
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddSignInManager<SignInManager<ApplicationUser>>()
                            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(jwt => {
                            jwt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            jwt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;     
                            }).AddJwtBearer(option =>
                            {

                                option.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuerSigningKey = true,
                                    ValidateIssuer = true,
                                    ValidateAudience = false,
                                    ValidIssuer = builder.Configuration["token:issuer"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["token:key"])),
                                };
                            });

            // builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            // configure IFileProvider
            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                );

            // configure Redis => Distributed Memory Cache

            builder.Services.AddSingleton<IConnectionMultiplexer>(redis => {

                var configRedisOption = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configRedisOption);
            });

            builder.Services.AddMemoryCache();

            // In Startup.cs or Program.cs
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<MyCustomExceptionMiddleware>(); // error object null refernce 
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigin");

            app.MapControllers();



            // important 

            createUserDefault(app.Services);

            app.Run();
        }

      public static  async Task createUserDefault(IServiceProvider app)
        {

            //using var scope = app.Services.CreateScope();
            using var scope = app.CreateScope();

            var service = scope.ServiceProvider;
            //var context1 = service.GetRequiredService<ApplicationDbContext>(); // get instance
            //var context2 = service.GetRequiredService<ApplicationIdentityDbContext>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();//get instance


            var logger = service.GetRequiredService<ILogger<Program>>();

            try
            {
                //await context1.Database.MigrateAsync();
                //await context2.Database.MigrateAsync();
                //await ApplicationDbContextSeed.SeedingDataAsync(context1);
                await SeedingDefaultUser.SeedingUserAsync(userManager);


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error happened!");

            }
        }
    }
}
