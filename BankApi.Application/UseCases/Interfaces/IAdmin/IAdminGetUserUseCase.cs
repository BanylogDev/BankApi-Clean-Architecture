using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.Interfaces.IAdmin
{
    public interface IAdminGetUserUseCase
    {
        Task<User?> ExecuteAsync(int id);
    }
}
