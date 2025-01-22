using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            if (_context.Stocks == null)
            {
                throw new InvalidOperationException("Stocks is not initialized in the DbContext.");
            }       

            return _context.Stocks.ToListAsync();
        }
    }
}