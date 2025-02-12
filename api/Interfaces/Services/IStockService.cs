using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Stock;
using api.Models;
using api.Helpers;

namespace api.Services
{
    public interface IStockService
    {
        public Task<List<StockDto>> GetAllStocksAsync(QueryObject queryObject);
        public Task<StockDto?> GetStockByIdAsync(int id);
        public Task<StockDto> CreateStockAsync(CreateStockRequestDto createStockRequestDto);
        public Task<StockDto?> UpdateStockAsync(int id, UpdateStockRequestDto updateStockRequestDto);
        public Task<bool> DeleteStockAsync(int id);
        public Task<bool> StockExists(int id);

    }
}