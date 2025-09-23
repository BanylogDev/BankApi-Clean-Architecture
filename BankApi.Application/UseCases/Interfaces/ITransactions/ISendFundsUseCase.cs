using BankApi.Application.DTOs;
using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.Interfaces.ITransactions
{
    public interface ISendFundsUseCase
    {
        Task<Transactions?> ExecuteAsync(TransactionsDTO dto);
    }
}
