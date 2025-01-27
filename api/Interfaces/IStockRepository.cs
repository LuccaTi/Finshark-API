using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();//Returns all the stocks.
        public Task<Stock?> GetByIdAsync(int id);//Returns the object found with the id, it may be null if it's not found.
        public Task<Stock> CreateAsync(Stock stockModel);//Returns in the http response an Stock model of what was created. Not nullable because it's a creation.
        public Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);//Updates the object, will be null if it's not found.
        public Task<Stock?> DeleteAsync(int id);//Deletes an object, will be null if it's not found.
        public Task<bool> StockExists(int id);
    }
}