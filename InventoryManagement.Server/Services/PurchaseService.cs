using AutoMapper;
using InventoryManagement.Server.Data.Models.ViewModels;
using InventoryManagement.Server.Data.Models;
using InventoryManagement.Server.Data;

namespace InventoryManagement.Server.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PurchaseDTO> GetAllPurchases()
        {
            var purchases = _context.Purchases.ToList();
            return _mapper.Map<List<PurchaseDTO>>(purchases);
        }
        public PurchaseDTO GetPurchaseById(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null)
            {
                return null;
            }
            return _mapper.Map<PurchaseDTO>(purchase);
        }
        public PurchaseDTO CreatePurchase(PurchaseDTO purchaseDto)
        {
            var purchase = _mapper.Map<Purchase>(purchaseDto);
            var product = _context.Products.FirstOrDefault(x => x.ProductID == purchaseDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            product.QuantityInStock += purchaseDto.QuantityPurchased;
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            return _mapper.Map<PurchaseDTO>(purchase);
        }
    }

}
