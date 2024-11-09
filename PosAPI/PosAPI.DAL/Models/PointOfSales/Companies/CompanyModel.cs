namespace PosAPI.DAL.Models.PointOfSales.Companies
{
    public class CompanyModel : BaseModel
    {
        public required string CompanyName { get; set; }

        public required string CompanyEmail { get; set; }

        public required string CompanyContactNumber { get; set; }

        public int CompanyAddressID { get; set; }

        public virtual CompanyAddressModel? CompanyAddress { get; set; }
    }
}
