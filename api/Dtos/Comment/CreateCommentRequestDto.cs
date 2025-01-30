using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title needs to be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters long.")]
        [SwaggerSchema(Description = "Title of the comment.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content needs to be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters long.")]
        [SwaggerSchema(Description = "Content of the comment.")]
        public string Content { get; set; } = string.Empty;
    }
}