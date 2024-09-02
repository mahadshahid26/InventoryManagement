using InventoryManagement.Server.Data.Models.ViewModels;
using InventoryManagement.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var purchases = _purchaseService.GetAllPurchases();
            return Ok(purchases);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var purchase = _purchaseService.GetPurchaseById(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PurchaseDTO purchaseDto)
        {
            if (purchaseDto == null)
            {
                return BadRequest("Purchase data is null.");
            }

            var createdPurchase = _purchaseService.CreatePurchase(purchaseDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPurchase.ProductId }, createdPurchase);
        }

    }

}
