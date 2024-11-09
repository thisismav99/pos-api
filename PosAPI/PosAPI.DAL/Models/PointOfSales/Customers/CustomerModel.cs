namespace PosAPI.DAL.Models.PointOfSales.Customers
{
    public class CustomerModel : BaseModel
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public required string Email { get; set; }

        public required string ContactNumber { get; set; }
    }
}
