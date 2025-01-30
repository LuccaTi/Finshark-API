using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Dtos
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters long.")]
        [SwaggerSchema(Description = "Stock ticker symbol (max 10 characters). Example: 'AAPL' for Apple.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(15, ErrorMessage = "Company Name cannot be over 15 characters long.")]
        [SwaggerSchema(Description = "Full name of the company (max 15 characters). Example: 'Apple Inc.'")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000000000)]
        [SwaggerSchema(Description = "Current stock price. Example: 123.45")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        [SwaggerSchema(Description = "Last dividend paid per stock. Example: 0.0123")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Industry type cannot be over 15 characters long.")]
        [SwaggerSchema(Description = "Industry type of the company (max 15 characters). Example: 'Technology'")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 50000000000)]
        [SwaggerSchema(Description = "Market capitalization of the company. Example: 123456789")]
        public long MarketCap { get; set; }
    }
}