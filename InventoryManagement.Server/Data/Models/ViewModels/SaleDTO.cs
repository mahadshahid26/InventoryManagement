namespace InventoryManagement.Server.Data.Models.ViewModels
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Now;
        public int QuantitySold { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
    }
}
