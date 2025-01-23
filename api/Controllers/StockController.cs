using api.Dtos;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]//This string is added to the end of the server URL that is hosting the API so the web browser can send http requests to it.
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockRepository _stockRepo;//Dependency Injection.
        public StockController(IStockRepository stockRepo)
        {

            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()//Http methods are async, they can happen at the same time and work in diferent ways with the data linked to the API.
        {
            var stocks = await _stockRepo.GetAllAsync();//Call to the database, it's async because it's slow.
            if (stocks == null)
            {
                return NotFound();
            }
            var stocksDto = stocks.Select(s => s.ToStockDto());//Linq used so the return is not the whole entity, only what the client needs to know.
            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid information!");
            }

            var stockModel = createDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid information!");
            }

            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}