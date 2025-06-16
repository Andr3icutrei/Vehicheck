using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Services;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Core
{
    public static class DIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICarManufacturerService, CarManufacturerService>();
            services.AddScoped<ICarModelService, CarModelService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IComponentManufacturerService, ComponentManufacturerService>();
            services.AddScoped<IComponentService, ComponentService>();
            services.AddScoped<IFixService, FixService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
