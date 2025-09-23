using BankApi.Application.UseCases.Interfaces.IAdmin;
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
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository _userRepo;

        public GetUserUseCase(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> ExecuteAsync(int id)
        {
            return await _userRepo.GetUserById(id);
        }
    }
}
