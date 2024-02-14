using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol is too long")]
        [MinLength(1, ErrorMessage = "Symbol is too short")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Company Name is too long")]
        [MinLength(3, ErrorMessage = "Company Name is too short")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Industry is too long")]
        [MinLength(3, ErrorMessage = "Industry is too short")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000)]
        public long MarketCap { get; set; }
    }
}