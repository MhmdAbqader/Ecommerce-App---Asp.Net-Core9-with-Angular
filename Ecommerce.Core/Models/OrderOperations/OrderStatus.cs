using System.Runtime.Serialization;

namespace Ecommerce.Core.Models.OrderOperations
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,

        [EnumMember(Value ="Payment Completed")]
        PaymentCompleted,

        PaymentFailed,

    }
}