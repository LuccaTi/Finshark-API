using api.Dtos;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {

            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockService.GetAllStocksAsync();
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            if(createStockRequestDto == null)
            {
                return BadRequest("Create data is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStock = await _stockService.CreateStockAsync(createStockRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStock.Id }, createdStock);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if (updateStockRequestDto == null)
            {
                return BadRequest("Update data is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedStock = await _stockService.UpdateStockAsync(id, updateStockRequestDto);

            if (updatedStock == null)
            {
                return NotFound($"Stock with id #{id} not found.");
            }

            return Ok(updatedStock);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _stockService.DeleteStockAsync(id);
            if (!success)
            {
                return NotFound("Stock does not exist!");
            }
            return NoContent();
        }

    }
}