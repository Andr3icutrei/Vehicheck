using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Context;
using Vehicheck.Database.Repositories;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Database
{
    public static class DIConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<VehicheckDbContext>();
            services.AddScoped<DbContext, VehicheckDbContext>();

            services.AddScoped<ICarManufacturerRepository, CarManufacturerRepository>();
            services.AddScoped<ICarModelRepository, CarModelRepository>();
            services.AddScoped<ICarRepository,CarRepository>(); 
            services.AddScoped<IComponentManufacturerRepository, ComponentManufacturerRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();    
            services.AddScoped<IFixRepository, FixRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
