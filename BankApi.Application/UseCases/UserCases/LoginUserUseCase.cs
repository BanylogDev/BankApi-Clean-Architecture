using BankApi.Application.DTOs;
using BankApi.Application.Interfaces;
using BankApi.Application.UseCases.Interfaces.IUser;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.UserCases
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;

        public LoginUserUseCase(IPasswordHasher passwordHasher, IJwtService jwtService, IAuthRepository authRepo, IConfiguration config)
        {
            _passwordHasher = passwordHasher;
            _authRepo = authRepo;
            _config = config;
            _jwtService = jwtService;
        }

        public async Task<User?> ExecuteAsync(LoginDTO dto)
        {
            var user = await _authRepo.GetUserByEmail(dto.Email);

            if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.Password))
                return null;

            var token = _jwtService.GenerateJwtToken(user, _config);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.AccessToken = token;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            
            await _authRepo.SaveChangesAsync();

            return user;
        }
    }
}
