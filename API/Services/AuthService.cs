using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Services
{
    public static class AuthService
    {
        public static IServiceCollection GetAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            var secretkey = configuration.GetSection("SecretKey").Value;
            var simmetricKey = ASCIIEncoding.ASCII.GetBytes(secretkey);

            TokenValidationParameters validationParameters = new TokenValidationParameters 
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(simmetricKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => opt.TokenValidationParameters = validationParameters);
            
            return services;
        }
    }
}