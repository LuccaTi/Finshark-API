using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace api.Examples.Stock
{
    public class CreateStockRequestExample : IExamplesProvider<CreateStockRequestDto>
    {
        public CreateStockRequestDto GetExamples()
        {
            return new CreateStockRequestDto
            {
                Symbol = "AAPL",
                CompanyName = "Apple Inc.",
                Purchase = 123.45m,
                LastDiv = 0.0123m,
                Industry = "Technology",
                MarketCap = 123456789
            };
        }
    }
}