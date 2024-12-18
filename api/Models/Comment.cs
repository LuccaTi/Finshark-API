using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;//Empty default value because of possible null references.
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;//Receives value for the exact moment the comment was instantiated.
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }//Navigation Property.

    }
}