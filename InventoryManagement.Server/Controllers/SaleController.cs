using InventoryManagement.Server.Data.Models.ViewModels;
using InventoryManagement.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sales = _saleService.GetAllSales();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sale = _saleService.GetSaleById(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SaleDTO saleDto)
        {
            if (saleDto == null)
            {
                return BadRequest("Sale data is null.");
            }

            var createdSale = _saleService.CreateSale(saleDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSale.SaleId }, createdSale);
        }

    }

}
