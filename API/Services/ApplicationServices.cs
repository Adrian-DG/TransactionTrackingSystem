namespace API.Services
{
    public static class ApplicationServices
    {
        public static IServiceCollection GetApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.GetAuthService(configuration);
            services.GetDbService(configuration, environment);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRespository, AuthRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}