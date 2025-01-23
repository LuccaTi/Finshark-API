using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        public string Title { get; set; } = string.Empty;//Empty default value because of possible null references.
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;//Receives default value for the exact moment the comment was instantiated.
        public int? StockId { get; set; }
    }
}