using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using Swashbuckle.AspNetCore.Filters;

namespace api.Examples.Stock
{
    public class UpdateStockRequestExample : IExamplesProvider<UpdateStockRequestDto>
    {
        public UpdateStockRequestDto GetExamples()
        {
            return new UpdateStockRequestDto
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