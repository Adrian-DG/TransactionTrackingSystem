using Application.Interfaces;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Infrastructure.Helpers;
using Domain.Enums;


namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRespository
    {
        private MainContext _context;
        private DbSet<User> _users;
        private EncryptHelper _encrypt;
        private TokenHelper _token;
        public AuthRepository(MainContext context)
        {
            _context = context;
            _users = _context.Set<User>();
            _encrypt = new EncryptHelper();
            _token = new TokenHelper();
        }

        private async Task<bool> DoesUserExists(string username) 
        {
            return await _users.AnyAsync<User>(x => x.Username == username);
        }

        public async Task<LoginResponse> Login(UserModelDTO model, string secretKey)
        {
            // In case user couldn't be found
            if (!(await DoesUserExists(model.Username))) return new LoginResponse { Title = "Error", Message = "Check your credentials, user incorrenct", Status = false, Token = null };

            var foundUser = await _users.SingleAsync<User>(x => x.Username == model.Username);

            var isAuthenticated = _encrypt.VerifyPasswordHash(model.Password, foundUser.PasswordHash, foundUser.PasswordSalt);

            return  isAuthenticated
                    ? new LoginResponse { Title = "User Validated", Message = "This user was authenticated", Status = true, Token = _token.CreateToken(foundUser, secretKey) }
                    : new LoginResponse { Title = "Error", Message = "User validation failed", Status = false, Token = null };

        }

        public async Task<ServerResponse> Register(UserModelDTO model)
        {
            if (await DoesUserExists(model.Username)) return new ServerResponse { Title = "Error", Message = "This user is not available", Status = false };

            _encrypt.CreatePasswordHash(model.Password, out byte[] passwordHash,out byte[] passwordSalt);

            var newUser = new User 
            {
                Username = model.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Roles.Customer,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Status = true
            };

            return  await _context.SaveChangesAsync() > 0
                    ? new ServerResponse { Title = "Ok", Message = "User registered successfully!!", Status = true }
                    : new ServerResponse { Title = "Error", Message = "Something went wrong during user registration!!", Status = false };

        }
    }
}