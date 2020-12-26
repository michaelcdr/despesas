using Microsoft.Extensions.Configuration;
using System.IO;

namespace LifeManager.Infra.DBConfiguration
{
    public class DatabaseConnection
    {
        public static IConfiguration ConnectionConfiguration
        {
            get
            {
                string directoryBasePath = Directory.GetCurrentDirectory();

                IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(directoryBasePath)
                    .AddJsonFile("appsettings.json")
                    .Build();
                return Configuration;
            }
        }
    }
}