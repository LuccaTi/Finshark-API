using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using Swashbuckle.AspNetCore.Filters;

namespace api.Examples.Comment
{
    public class CreateCommentRequestExample : IExamplesProvider<CreateCommentRequestDto>
    {
        public CreateCommentRequestDto GetExamples()
        {
            return new CreateCommentRequestDto
            {
                Title = "This is the comment title",
                Content = "This is what the comment is all about."
            };
        }
    }
}