namespace PosAPI.DAL.Models.PointOfSales.Companies
{
    public class CompanyOrgModel : BaseModel
    {
        public required string Division { get; set; }

        public required string Department { get; set; }
    }
}
