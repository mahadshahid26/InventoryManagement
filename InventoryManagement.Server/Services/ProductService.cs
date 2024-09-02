using AutoMapper;
using InventoryManagement.Server.Data;
using InventoryManagement.Server.Data.Models;
using InventoryManagement.Server.Models;

namespace InventoryManagement.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ProductDTO> GetAllProducts()
        {
            var products = _context.Products.ToList();
            return _mapper.Map<List<ProductDTO>>(products);
        }
        public ProductDTO GetProductById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductDTO>(product);
        }
        public ProductDTO CreateProduct(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            _context.SaveChanges();
            return _mapper.Map<ProductDTO>(product);
        }

        public ProductDTO UpdateProduct(int id, ProductDTO productDto)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            _mapper.Map(productDto, product);
            _context.SaveChanges();
            return _mapper.Map<ProductDTO>(product);
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
