using PosAPI.DAL.Models.PointOfSales.Companies;

namespace PosAPI.DAL.Models.PointOfSales.Products
{
    public class ProductModel : BaseModel
    {
        public required string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public string? ProductDescription { get; set; }

        public int CompanyID { get; set; }

        public virtual CompanyModel? Company { get; set; }
    }
}
