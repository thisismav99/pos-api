using PosAPI.DAL.Models.PointOfSales.Customers;
using PosAPI.DAL.Models.PointOfSales.Products;
using PosAPI.DAL.Models.PointOfSales.Users;

namespace PosAPI.DAL.Models.PointOfSales.Transactions
{
    public class TransactionModel : BaseModel
    {
        public int? EmployeeID { get; set; }

        public int? ProductID { get; set; }

        public int? CustomerID { get; set; }

        public bool IsVoid { get; set; }

        public virtual EmployeeModel? Employee { get; set; }

        public virtual ProductModel? Product { get; set; }

        public virtual CustomerModel? Customer { get; set; }
    }
}
