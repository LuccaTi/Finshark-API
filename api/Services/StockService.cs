using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepo;
        public StockService(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<StockDto> CreateStockAsync(CreateStockRequestDto createStockRequestDto)
        {
            var stockModel = createStockRequestDto.ToStockFromCreateDTO();
            await _stockRepo.CreateStockAsync(stockModel);
            return stockModel.ToStockDto();
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var success = await _stockRepo.DeleteStockAsync(id);
            return success;
        }

        public async Task<List<StockDto>> GetAllStocksAsync()
        {
            var stocks = await _stockRepo.GetAllStocksAsync();
            var stocksDto = stocks.Select(s => s.ToStockDto());
            return stocksDto.ToList();

        }

        public async Task<StockDto?> GetStockByIdAsync(int id)
        {
            var stock = await _stockRepo.GetStockByIdAsync(id);
            if (stock == null)
            {
                return null;
            }
            return stock.ToStockDto();
        }

        public async Task<StockDto?> UpdateStockAsync(int id, UpdateStockRequestDto updateStockRequestDto)
        {
            var stockModel = updateStockRequestDto.ToStockFromUpdateDto();
            var updatedStock = await _stockRepo.UpdateStockAsync(id, stockModel);
            if (updatedStock == null)
            {
                return null;
            }
            return updatedStock.ToStockDto();
        }

        public async Task<bool> StockExists(int id)
        {
            var exists = await _stockRepo.StockExists(id);
            return exists;
        }
    }
}