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
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;

        public DeleteUserUseCase(IUserRepository userRepo, IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }   

        public async Task<User?> ExecuteAsync(DeleteAccountDTO dto)
        {
            var user = await _userRepo.GetUserById(dto.Id);
            if (user == null)
                return null;

            if (user.Password != _passwordHasher.HashPassword(dto.Password) || dto.Password != dto.ConfirmPassword)
                return null;

            _userRepo.RemoveUser(user);
            await _userRepo.SaveChangesAsync();

            return user;
        }
    }
}
