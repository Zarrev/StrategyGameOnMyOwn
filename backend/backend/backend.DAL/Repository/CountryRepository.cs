using backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL.Repository
{
    class CountryRepository: ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public async Task<IEnumerable<Country>> GetElements()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetElementById(string elementId)
        {

            var query = from b in _context.Countries
                        where b.Id == elementId
                        select b;
                return await query.FirstOrDefaultAsync();
        }

        public void InsertElement(Country element)
        {
            _context.Countries.Add(element);
        }

        public void DeleteElement(string elementId)
        {
            var query = from b in _context.Countries
                        where b.Id == elementId
                        select b;
            _context.Countries.Remove(query.FirstOrDefault() ?? throw new InvalidOperationException());
        }

        public void UpdateElement(Country element)
        {
            _context.Entry(element).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
}
