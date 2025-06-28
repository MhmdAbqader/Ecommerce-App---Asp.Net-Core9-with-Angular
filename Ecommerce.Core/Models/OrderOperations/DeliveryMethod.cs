namespace Ecommerce.Core.Models.OrderOperations
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public DeliveryMethod()
        {            
        }
        public DeliveryMethod(string deliveryMethodShortName, string description, string deliveryTime, decimal deliveryPrice)
        {
            DeliveryMethodShortName = deliveryMethodShortName;
            Description = description;
            DeliveryTime = deliveryTime;
            DeliveryPrice = deliveryPrice;
        }

        public string DeliveryMethodShortName { get; set; }
        public string Description{ get; set; }
        public string DeliveryTime{ get; set; }
        public decimal DeliveryPrice{ get; set; }
    }
}