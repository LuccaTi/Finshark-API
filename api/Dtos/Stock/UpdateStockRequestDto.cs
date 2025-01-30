using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters long.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(15, ErrorMessage = "Company Name cannot be over 15 characters long.")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Industry type cannot be over 15 characters long.")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 50000000000)]
        public long MarketCap { get; set; }
    }
}