using Domain.DTO;

namespace Application.Interfaces
{
    public interface IAuthRespository
    {
        Task<ServerResponse> Register(UserModelDTO model);
        Task<LoginResponse> Login(UserModelDTO model, string secretKey); 
    }
}