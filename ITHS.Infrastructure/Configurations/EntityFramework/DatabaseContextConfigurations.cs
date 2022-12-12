using ITHS.Webapi.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITHS.Infrastructure.Configurations.EntityFramework
{
    public static class DatabaseContextConfigurations
    {
        /// <summary>
        /// Will create the file if no sqlLight.db fie exists
        /// </summary>
        /// <param name="services"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IServiceCollection AddITHSDbContextUsingSQLLite(this IServiceCollection services, string fileName = "SqlLight.db")
        {
            return services
                .AddDbContext<ITHSDatabaseContext>(options =>
                    options.UseSqlite($"Data Source={fileName}"),
                    ServiceLifetime.Scoped
            );
        }

        public static IServiceCollection AddITHSDbContextUsingMSSQL(this IServiceCollection services, IConfiguration config)
        {
            var ConnectionString = GetConnectionString(config, "ITHSDatabase");

            return services
                .AddDbContext<ITHSDatabaseContext>(options =>
                    options.UseSqlServer(ConnectionString),
                    ServiceLifetime.Scoped
             );
        }

        private static string GetConnectionString(IConfiguration config, string name)
        {
            var ConnectionString = config.GetConnectionString(name);
            
            if (ConnectionString == null)
                throw new Exception("No connection string found in appsettings.json");

            return ConnectionString;
        }
    }
}
