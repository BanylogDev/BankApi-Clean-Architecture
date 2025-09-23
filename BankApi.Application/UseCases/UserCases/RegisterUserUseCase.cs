using BankApi.Application.DTOs;
using BankApi.Application.Interfaces;
using BankApi.Application.UseCases.Interfaces.IUser;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankApi.Application.UseCases.UserCases
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthRepository _authRepo;

        public RegisterUserUseCase(IPasswordHasher passwordHasher, IAuthRepository authRepo)
        {
            _passwordHasher = passwordHasher;
            _authRepo = authRepo;
        }

        public async Task<User?> ExecuteAsync(RegisterDTO dto)
        {

            var existingName = await _authRepo.GetUserByName(dto.Name);

            var existingEmail = await _authRepo.GetUserByEmail(dto.Email);

            if (existingName != null || existingEmail != null)
            {
                return null;
            }

            var hashedPassord = _passwordHasher.HashPassword(dto.Password);

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email.ToLower().Trim(),
                Password = hashedPassord,
                Role = "User",
                Balance = 0,
                PhoneNumber = dto.PhoneNumber.Trim(),
                CreatedAt = DateTime.UtcNow,
            };

            return await _authRepo.Register(newUser);
        }
    }
}
