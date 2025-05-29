using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Context;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Database.Repositories
{
    public class ComponentRepository(VehicheckDbContext databaseContext) : BaseRepository<Component>(databaseContext), IComponentRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;

        public async Task<Component?> GetComponentId(int componentId)
        {
            return await _context.Components
                .Include(c => c.Manufacturer)
                .Include(c => c.Cars)
                    .ThenInclude(cc => cc.Car)
                .Include(c => c.Fixes)
                    .ThenInclude(cf => cf.Fix)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == componentId);
        }

        public async Task<List<Component>> GetAllComponent()
        {
            return await _context.Components
                .Include(c => c.Manufacturer)
                .Include(c => c.Cars)
                    .ThenInclude(cc => cc.Car)
                .Include(c => c.Fixes)
                    .ThenInclude(cf => cf.Fix)
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Component> AddComponentAsync(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            Insert(component);
            await SaveChangesAsync();
            return component;
        }

        public async Task<Component> UpdateComponentAsync(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            Update(component);
            await SaveChangesAsync();
            return component;
        }
    }
}
