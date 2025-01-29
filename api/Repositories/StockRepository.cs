using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsync(Stock stockModel)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return false;
            }
            _context.Stocks?.Remove(stockModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }

            return await _context.Stocks.Include(s => s.Comments).ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<bool> StockExists(int id)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, Stock stockModel)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }
            var existingStock = await _context.Stocks.FindAsync(id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockModel.Symbol;
            existingStock.CompanyName = stockModel.CompanyName;
            existingStock.Purchase = stockModel.Purchase;
            existingStock.LastDiv = stockModel.LastDiv;
            existingStock.Industry = stockModel.Industry;
            existingStock.MarketCap = stockModel.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}