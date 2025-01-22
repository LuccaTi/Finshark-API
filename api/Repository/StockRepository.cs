using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository//Interface is implemented because of dependency injection on the controller.
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;//Dependency injected.
        }

        public async Task<Stock> CreateAsync(Stock stockModel)//The model is received through the requests in the controller.
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }  
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }  
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }
            _context.Stocks?.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }       

            return await _context.Stocks.ToListAsync();//Dependency used to access the database context.
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }  
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }  
           var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
           if(existingStock == null)
           {
            return null;
           }
            existingStock.Symbol = updateDto.Symbol;
            existingStock.CompanyName = updateDto.CompanyName;
            existingStock.Purchase = updateDto.Purchase;
            existingStock.LastDiv = updateDto.LastDiv;
            existingStock.Industry = updateDto.Industry;
            existingStock.MarketCap = updateDto.MarketCap;
            
            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}