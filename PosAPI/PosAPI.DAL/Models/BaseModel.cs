namespace PosAPI.DAL.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        public required string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }
    }
}
