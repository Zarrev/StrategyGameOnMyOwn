using backend.DAL.Repository.Interfaces;
using backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL.Repository
{
    public class CountryRepository : ICountryRepository
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

        public async Task<Country> getElementByUserId(string userId)
        {
            var query = from b in _context.Countries
                        where b.UserId == userId
                        select b;
            return await query.FirstOrDefaultAsync();
        }

        public async Task InsertElement(Country element)
        {
            await _context.Countries.AddAsync(element);
        }

        public async Task DeleteElement(string elementId)
        {
            var query = from b in _context.Countries
                        where b.Id == elementId
                        select b;

            _context.Countries.Remove(await query.FirstOrDefaultAsync() ?? throw new InvalidOperationException());
        }

        public async Task UpdateElement(Country element)
        {
            _context.Entry(element).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> getCurrentRank(string userId)
        {
            //var index = 0;
            //var query = from country in _context.Countries
            //            orderby country.Points descending
            //            select country;
            //var query2 = query.Select((x, i) => new { Country = x, Rank = i+1 });
            //var query3 = from countryWithRank in query2
            //             where countryWithRank.Country.UserId == userId
            //             select countryWithRank.Rank;

            var countries = _context.Countries;
            var query = from country in countries
                        orderby country.Points descending
                        select new
                        {
                            UserId = country.UserId,
                            Rank = (from otherCountry in countries
                                    where otherCountry.Points > country.Points
                                    select otherCountry).Count() + 1
                        };
            var query2 = from uIdWithRank in query
                         where uIdWithRank.UserId == userId
                         select uIdWithRank.Rank;

            return await query2.FirstOrDefaultAsync();
        }
    }

}
