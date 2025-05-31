using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Context;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Database.Repositories
{
    public class FixRepository : BaseRepository<Fix>, IFixRepository
    {
        private readonly VehicheckDbContext _context;
        public FixRepository(VehicheckDbContext databaseContext) : base(databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<Fix> AddFixAsync(Fix fix)
        {
            Insert(fix);
            await _context.SaveChangesAsync();
            return fix;
        }

        public async Task<List<Fix>> GetAllFixesAsync()
        {
            return await _context.Fixes
                .Where(f => f.DeletedAt == null)
                .Include(f => f.Components.Where(cf => cf.DeletedAt == null && cf.Component != null && cf.Component.DeletedAt == null))
                .ThenInclude(cf => cf.Component)
                .OrderBy(f => f.Id)
                .ToListAsync();  
        }

        public async Task<Fix?> GetFixAsync(int id)
        {
            return await _context.Fixes
                .Where(f => f.DeletedAt == null)
                .Include(f => f.Components.Where(cf => cf.DeletedAt == null && cf.Component != null && cf.Component.DeletedAt == null))
                .ThenInclude(cf => cf.Component)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> DeleteFixAsync(int id)
        {
            var fix = await GetFirstOrDefaultAsync(id);
            if (fix == null)
                return false;

            SoftDelete(fix);
            await SaveChangesAsync();
            return true;
        }

    }
}
