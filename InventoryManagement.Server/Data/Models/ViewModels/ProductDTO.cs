namespace InventoryManagement.Server.Models
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int QuantityInStock { get; set; }
    }
}
