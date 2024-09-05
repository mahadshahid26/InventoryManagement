using AutoMapper;
using InventoryManagement.Server.Data.Models.ViewModels;
using InventoryManagement.Server.Data.Models;
using InventoryManagement.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Server.Services
{
    public class SaleService : ISaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SaleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<SaleDTO> GetAllSales()
        {
            var sales = _context.Sales
                .Include(p => p.Product)
                .ToList();
            return _mapper.Map<List<SaleDTO>>(sales);
        }
        public SaleDTO GetSaleById(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale == null)
            {
                return null;
            }
            return _mapper.Map<SaleDTO>(sale);
        }
        public SaleDTO CreateSale(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            var product = _context.Products.FirstOrDefault(x => x.ProductID == saleDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            product.QuantityInStock -= saleDto.QuantitySold;
            _context.Sales.Add(sale);
            _context.SaveChanges();
            return _mapper.Map<SaleDTO>(sale);
        }
    }

}
