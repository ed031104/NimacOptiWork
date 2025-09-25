using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class Configuracion
    {
        public static IConfiguration? Configuration;


        static Configuracion()
        {
            CargarConfiguracion();
        }

        private static void CargarConfiguracion()
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            // Para WinUI3: usar el directorio actual de la aplicación
            var basePath = AppContext.BaseDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        public static string? Get(string key) => Configuration?[key];
    }
}
