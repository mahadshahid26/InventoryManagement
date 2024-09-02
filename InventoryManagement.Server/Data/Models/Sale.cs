using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Server.Data.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalPrice { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
