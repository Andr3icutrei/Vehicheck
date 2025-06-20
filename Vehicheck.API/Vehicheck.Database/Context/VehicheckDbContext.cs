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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // User entity configuration for password hashing
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(e => e.Email)
                    .IsUnique();
            });

            ApplySoftDeleteFilter<Car>(modelBuilder);
            ApplySoftDeleteFilter<CarManufacturer>(modelBuilder);
            ApplySoftDeleteFilter<CarModel>(modelBuilder);
            ApplySoftDeleteFilter<CarModelComponent>(modelBuilder);
            ApplySoftDeleteFilter<Component>(modelBuilder);
            ApplySoftDeleteFilter<ComponentFix>(modelBuilder);
            ApplySoftDeleteFilter<ComponentManufacturer>(modelBuilder);
            ApplySoftDeleteFilter<Fix>(modelBuilder);
            ApplySoftDeleteFilter<User>(modelBuilder);

        }

        private void ApplySoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : class
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => EF.Property<DateTime?>(e, "DeletedAt") == null);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModelComponent> CarModelsComponents { get;set; }
        public DbSet<CarManufacturer> CarManufacturers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentFix> ComponentsFixes { get; set; }
        public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
        public DbSet<Fix> Fixes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
