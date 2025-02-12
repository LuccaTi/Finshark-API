using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Helpers;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllStocksAsync(QueryObject queryObject);
        public Task<Stock?> GetStockByIdAsync(int id);
        public Task<Stock> CreateStockAsync(Stock stockModel);
        public Task<Stock?> UpdateStockAsync(int id, Stock stockModel);
        public Task<bool> DeleteStockAsync(int id);
        public Task<bool> StockExists(int id);
    }
}