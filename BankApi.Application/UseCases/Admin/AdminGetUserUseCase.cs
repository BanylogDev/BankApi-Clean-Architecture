using BankApi.Application.UseCases.Interfaces.IAdmin;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.AdminCases
{
    public class AdminGetUserUseCase : IAdminGetUserUseCase
    {
        private readonly IUserRepository _userRepo;

        public AdminGetUserUseCase(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> ExecuteAsync(int id)
        {
            return await _userRepo.GetUserById(id);
        }
    }
}
