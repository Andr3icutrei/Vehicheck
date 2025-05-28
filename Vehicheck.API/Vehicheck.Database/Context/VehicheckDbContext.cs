using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Infrastructure.Config;

namespace Vehicheck.Database.Context
{
    public class VehicheckDbContext : DbContext
    {
        public VehicheckDbContext(DbContextOptions<VehicheckDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings?.VehicheckDb);

            if (AppConfig.ConsoleLogQueries)
            {
                optionsBuilder.LogTo(Console.WriteLine);
            }
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarComponent> CarsComponents { get;set; }
        public DbSet<CarManufacturer> CarManufacturers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentFix> ComponentsFixes { get; set; }
        public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
        public DbSet<Fix> Fixes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
