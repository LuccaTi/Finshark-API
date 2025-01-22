using api.Data;
using api.Dtos;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]//This string is added to the end of the server URL that is hosting the API so the web browser can send http requests to it.
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()//Http methods are async, they can happen at the same time and work in diferent ways with the data linked to the API.
        {
            if (_context == null || _context.Stocks == null)
            {
                return Problem("Database context is not available or DbSet is null."); // Mensagem de erro genérica
            }

            var stocks = await _stockRepo.GetAllAsync();//Call to the database, it's async because it's slow.
            if(stocks == null)
            {
                return NotFound();
            }
            var stocksDto = stocks.Select(s => s.ToStockDto());//Linq used so the return is not the whole entity, only what the client needs to know.
            return Ok(stocksDto);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (_context == null || _context.Stocks == null)
            {
                return Problem("Database context is not available or DbSet is null."); // Mensagem de erro genérica
            }
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Insira informações válidas!");
            }
            if (_context == null || _context.Stocks == null)
            {
                return Problem("Database context is not available or DbSet is null."); // Mensagem de erro genérica
            }

            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (_context == null || _context.Stocks == null)
            {
                return Problem("Database context is not available or DbSet is null."); // Mensagem de erro genérica
            }
            var stockModel = await _context.Stocks.FindAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            }
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (_context == null || _context.Stocks == null)
            {
                return Problem("Database context is not available or DbSet is null."); // Mensagem de erro genérica
            }
            var stockModel = await _context.Stocks.FindAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks?.Remove(stockModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
               
    }
}