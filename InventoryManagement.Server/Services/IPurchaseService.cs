using InventoryManagement.Server.Data.Models.ViewModels;

namespace InventoryManagement.Server.Services
{
    public interface IPurchaseService
    {
        List<PurchaseDTO> GetAllPurchases();
        PurchaseDTO GetPurchaseById(int id);
        PurchaseDTO CreatePurchase(PurchaseDTO purchaseDto);
    }
}
