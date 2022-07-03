namespace API.Services
{
    public static class ApplicationServices
    {
        public static IServiceCollection GetApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.GetAuthService(configuration);
            services.GetDbService(configuration, environment);
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IAuthRespository, AuthRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(ISpecification<>), typeof(Specification<>));

            return services;
        }
    }
}