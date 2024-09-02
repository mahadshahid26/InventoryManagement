namespace InventoryManagement.Server.Data.Models.ViewModels
{
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public int QuantityPurchased { get; set; }
        public decimal TotalCost { get; set; }
        public int ProductId { get; set; }
    }
}
