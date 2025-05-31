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
    public class ComponentManufacturerRepository : BaseRepository<ComponentManufacturer>, IComponentManufacturerRepository
    {
        private readonly VehicheckDbContext _context;

        public ComponentManufacturerRepository(VehicheckDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ComponentManufacturer> AddComponentAsync(ComponentManufacturer componentManufacturer)
        {
            Insert(componentManufacturer); 
            await _context.SaveChangesAsync();
            return componentManufacturer;
        }

        public async Task<List<ComponentManufacturer>> GetAllComponentsAsync()
        {
            return await _context.ComponentManufacturers.
                Where(cm => cm.DeletedAt == null).
                Include(cm => cm.Components.Where(c => c.DeletedAt == null)).
                OrderBy(cm => cm.Id).
                ToListAsync();
        }

        public async Task<ComponentManufacturer?> GetComponentManufacturerAsync(int id)
        {
            return await _context.ComponentManufacturers.
                Where(cm => cm.DeletedAt == null).
                Include(cm => cm.Components.Where(c => c.DeletedAt == null)).
                FirstOrDefaultAsync(cm => cm.Id == id);
        }
    }
}
