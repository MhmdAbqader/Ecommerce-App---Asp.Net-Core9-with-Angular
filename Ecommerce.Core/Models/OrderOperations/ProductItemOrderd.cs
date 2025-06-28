namespace Ecommerce.Core.Models.OrderOperations
{
    public class ProductItemOrderd 
    {
        public ProductItemOrderd()
        {            
        }
        public ProductItemOrderd(int productItemId, string productItemName)
        {
            ProductItemOrderdId = productItemId;
            ProductItemName = productItemName;
        }

        public int ProductItemOrderdId { get; set; }
        public string ProductItemName { get; set; }

        //public override bool Equals(object obj)
        //{
        //    return obj is ProductItemOrderd orderd &&        //        
        //           ProductItemId == orderd.ProductItemId &&
        //           ProductItemName == orderd.ProductItemName;
        //}
    }
}