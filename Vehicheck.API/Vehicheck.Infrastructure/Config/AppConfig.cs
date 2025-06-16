using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Infrastructure.Config
{
    public static class AppConfig
    {
        public static bool ConsoleLogQueries = true;
        public static ConnectionStrings? ConnectionStrings { get; set; }
        public static JwtConfig? Jwt { get; set; }
        public static void Init(IConfiguration configuration)
        {
            Configure(configuration);
        }

        private static void Configure(IConfiguration configuration)
        {
            ConnectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            Jwt = configuration.GetSection("Jwt").Get<JwtConfig>();
        }
    }
}
