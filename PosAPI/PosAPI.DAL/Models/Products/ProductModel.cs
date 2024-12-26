namespace PosAPI.DAL.Models.Products
{
    public class ProductModel : BaseModel
    {
        public required string ProductName { get; set; }

        public int ProductAmount { get; set; }
    }
}
