using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.FileProviders;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
      
        private readonly IFileProvider _fileProvider;

        public ProductRepository(ApplicationDbContext context,  IFileProvider fileProvider ):base(context)
        {   
            _context = context;       
            _fileProvider = fileProvider;
        }


        public async Task<IEnumerable<ProductDto>> GetAllAsync(string search,string sort, int? catId, int pageNo, int pageSize)
        {
            var allProducts = await _context.Products.Include(a => a.Category)
                              .AsNoTracking().ToListAsync();

            int noOfProduct = 0;
            //Search by Name
            if (!string.IsNullOrEmpty(search))
                allProducts = allProducts.Where(a => a.Name.ToLower().Contains(search.ToLower())).ToList();
            
            // Filter by Category ID
            if (catId.HasValue)
                allProducts = allProducts.Where(c => c.CategoryId == catId).ToList();
           
            // Filter by Price and Name
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort) 
                {
                    case "PriceASC": allProducts = allProducts.OrderBy(p => p.Price).ToList();                     
                        break;
                    case "PriceDESC": allProducts = allProducts.OrderByDescending(p => p.Price).ToList();
                        break;
                    default: allProducts = allProducts.OrderBy(n => n.Name).ToList();
                        break;
                }
            }


             noOfProduct = allProducts.Count();


            //Simple Pagination .... Tip for me if i moved to complex_type(class) 
            pageNo = pageNo > 0 ? pageNo : 1;
            pageSize = pageSize > 0 ? pageSize : 3;
            allProducts = allProducts.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            var result = allProducts.Select(a => new ProductDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Price = a.Price,
                Img=a.ImgURL,
                CategoryName = a.Category.Name,
                Totalcount = noOfProduct,
                PageNo = pageNo,
                PageSize = pageSize,
            });


            return result;
        }
        public async Task<bool> AddAsync(CreateProductDto createProductDto)
        {
            if (createProductDto.ImgURL is not null) 
            {
                string root = "/images/products/";
                string productName = $"{Guid.NewGuid()}" + createProductDto.ImgURL.FileName;
                string src = root + productName;

                if (!Directory.Exists("wwwroot" + root)) 
                {
                    Directory.CreateDirectory("wwwroot" + root);
                }

                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;

                using(var fs = new FileStream(rootPath,FileMode.Create)) 
                {
                    await createProductDto.ImgURL.CopyToAsync(fs);
                }
                var product = new Product
                {
                    ImgURL = rootPath,
                    Description = createProductDto.Description,
                    CategoryId = createProductDto.CategoryId,
                    Name = createProductDto.Name,
                    Price = createProductDto.Price,
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;  
        }
        public async Task<bool> DeleteWithImageAsync(int id)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct is not null)
            {
                if (!string.IsNullOrEmpty(currentProduct.ImgURL)) 
                {
                    // delete  img product
                    var picInfo = _fileProvider.GetFileInfo(currentProduct.ImgURL);
                    var rootPath = picInfo.PhysicalPath;
                    System.IO.File.Delete(rootPath);
                }
                _context.Products.Remove(currentProduct);
                await _context.SaveChangesAsync();
                return true;

            }
            return false;

        }


    }
}
