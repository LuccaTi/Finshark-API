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
        public CommentController(ICommentRepository commentRepo)
        {

            _commentRepo = commentRepo;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid information!");
            }
            var comment = createDto.ToCommentFromCreateDto();
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
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