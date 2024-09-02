using InventoryManagement.Server.Models;

namespace InventoryManagement.Server.Services
{
    public interface IProductService
    {
        public List<ProductDTO> GetAllProducts();
        public ProductDTO GetProductById(int id);
        public ProductDTO CreateProduct(ProductDTO productDto);
        public ProductDTO UpdateProduct(int id, ProductDTO productDto);
        public void DeleteProduct(int id);
    }
}
