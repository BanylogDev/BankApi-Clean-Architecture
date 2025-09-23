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

namespace BankApi.Application.UseCases.UserCases
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserUseCase(IUserRepository userRepo, IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> ExecuteAsync(UpdateDTO dto)
        {
            var user = await _userRepo.GetUserById(dto.Id);

            if (user == null)
                return null;

            if (user.Password != _passwordHasher.HashPassword(dto.Password) || dto.Password != dto.ConfirmPassword)
                return null;

            user.Email = dto.Email;
            user.Password = _passwordHasher.HashPassword(dto.Password);

            await _userRepo.SaveChangesAsync();

            return user;
        }
    }
}
