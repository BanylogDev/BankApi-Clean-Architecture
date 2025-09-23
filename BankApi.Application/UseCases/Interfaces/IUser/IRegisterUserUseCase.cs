using BankApi.Application.DTOs;
using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.Interfaces.IUser
{
    public interface IRegisterUserUseCase
    {
        Task<User?> ExecuteAsync(RegisterDTO dto);
    }
}
