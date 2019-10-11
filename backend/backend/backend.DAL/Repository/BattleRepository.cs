using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DAL.Repository.Interfaces;
using backend.Model.Backend;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL.Repository
{
    public class BattleRepository : IBattleRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public BattleRepository(ApplicationDbContext context)
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
        public async Task<IEnumerable<Battle>> GetElements()
        {
            return await _context.Battles.ToListAsync();
        }

        public async Task<Battle> GetElementById(string elementId)
        {

            var query = from b in _context.Battles
                        where b.Id == elementId
                        select b;
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Battle>> getElementsByUserId(string userId)
        {
            var query = from b in _context.Battles
                        where b.UserId == userId
                        select b;
            return await query.ToListAsync();
        }

        public async Task InsertElement(Battle element)
        {
            await _context.Battles.AddAsync(element);
        }

        public async Task DeleteElement(string elementId)
        {
            var query = from b in _context.Battles
                        where b.Id == elementId
                        select b;

            _context.Battles.Remove(await query.FirstOrDefaultAsync() ?? throw new InvalidOperationException());
        }

        public async Task UpdateElement(Battle element)
        {
            _context.Entry(element).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
