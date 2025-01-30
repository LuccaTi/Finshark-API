using api.Dtos;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentRequestDto createCommentRequestDto)
        {
            if(createCommentRequestDto == null)
            {
                return BadRequest("Create data is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdComment = await _commentService.CreateCommentAsync(stockId, createCommentRequestDto);
            if(createdComment == null)
            {
                return BadRequest($"Stock with id #{stockId} does not exist!");
            }
            return CreatedAtAction(nameof(GetById), new { id = createdComment.Id}, createdComment);  
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
        {
            if(updateCommentRequestDto == null)
            {
                return BadRequest("Update data is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedComment = await _commentService.UpdateCommentAsync(id, updateCommentRequestDto);
            if (updatedComment == null)
            {
                return NotFound($"Comment with id #{id} not found.");
            }

            return Ok(updatedComment);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _commentService.DeleteCommentAsync(id);
            if (!success)
            {
                return NotFound("Comment does not exist!");
            }

            return NoContent();
        }


    }
}