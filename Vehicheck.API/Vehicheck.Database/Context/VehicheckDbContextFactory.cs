using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Infrastructure.Config;

namespace Vehicheck.Database.Context
{
    public class VehicheckDbContextFactory : IDesignTimeDbContextFactory<VehicheckDbContext>
    {
        public VehicheckDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true); // fallback option

            var configuration = builder.Build();
            AppConfig.Init(configuration);

            var optionsBuilder = new DbContextOptionsBuilder<VehicheckDbContext>();
            optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings?.VehicheckDb);

            if (AppConfig.ConsoleLogQueries)
            {
                optionsBuilder.LogTo(Console.WriteLine);
            }

            return new VehicheckDbContext(optionsBuilder.Options);
        }
    }
}
