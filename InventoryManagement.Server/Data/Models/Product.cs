using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Server.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int QuantityInStock { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
