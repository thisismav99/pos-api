namespace PosAPI.DAL.Models.CompanyOne
{
    public class ProductModel : BaseModel
    {
        public required string ProductName { get; set; }

        public int ProductAmount { get; set; }
    }
}
