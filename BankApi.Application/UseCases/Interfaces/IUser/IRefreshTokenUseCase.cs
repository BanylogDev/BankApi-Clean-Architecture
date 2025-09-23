using BankApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.Interfaces.IUser
{
    public interface IRefreshTokenUseCase
    {
        Task<TokenDTO?> ExecuteAsync(TokenDTO dto);
    }
}
