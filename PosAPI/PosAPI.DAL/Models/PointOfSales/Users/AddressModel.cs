namespace PosAPI.DAL.Models.PointOfSales.Users
{
    public class AddressModel : BaseModel
    {
        public required string Street { get; set; }

        public required string Barangay { get; set; }

        public required string Municipality { get; set; }

        public required string Region { get; set; }

        public required string Country { get; set; }
    }
}
