using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public static class DatabaseService
    {
        public static IServiceCollection GetDbService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment) 
        {
            var connection = environment.IsProduction() 
            ? configuration.GetConnectionString("ProdConnection")
            : configuration.GetConnectionString("DevConnection");

            services.AddDbContext<MainContext>(opt => {
                opt.UseSqlServer(connection, b => b.MigrationsAssembly("API")); // We specify the project where migrations should be
            });

            return services;
        }
    }
}