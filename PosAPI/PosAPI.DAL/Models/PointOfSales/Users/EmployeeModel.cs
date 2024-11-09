using PosAPI.DAL.Models.PointOfSales.Companies;

namespace PosAPI.DAL.Models.PointOfSales.Users
{
    public class EmployeeModel : BaseModel
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int AddressID { get; set; }

        public int PositionID { get; set; }

        public int CompanyID { get; set; }

        public int CompanyOrgID { get; set; }

        public virtual AddressModel? Address { get; set; }

        public virtual PositionModel? Position { get; set; }

        public virtual CompanyModel? Company { get; set; }

        public virtual CompanyOrgModel? CompanyOrg { get; set; }

        // email and contact number will be on identity
    }
}
