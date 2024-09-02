using InventoryManagement.Server.Data.Models.ViewModels;

namespace InventoryManagement.Server.Services
{
    public interface ISaleService
    {
        List<SaleDTO> GetAllSales();
        SaleDTO GetSaleById(int id);
        SaleDTO CreateSale(SaleDTO saleDto);
    }
}
