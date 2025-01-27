using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//This string is added to the end of the server URL that is hosting the API so the web browser can send http requests to it.
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _commentRepo;//Dependency Injection.
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {

            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()//Http methods are async, they can happen at the same time and work in diferent ways with the data linked to the API.
        {
            var comments = await _commentRepo.GetAllAsync();//Call to the database, it's async because it's slow.
            if (comments == null)
            {
                return NotFound();
            }
            var commentsDto = comments.Select(c => c.ToCommentDto());//Linq used so the return is not the whole entity, only what the client needs to know.
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]//Stock Id is necessary because the comment (fk) can't exist in this context without a stock (Pk).
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid information!");
            }
            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist!");
            }

            var commentModel = createDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id}, commentModel.ToCommentDto());  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid information!");
            }

            var comment = await _commentRepo.UpdateAsync(id, updateDto);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}