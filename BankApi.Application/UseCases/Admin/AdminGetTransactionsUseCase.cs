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
    public class AdminGetTransactionsUseCase : IAdminGetTransactionsUseCase
    {
        private readonly ITransactionRepository _transactionRepo;

        public AdminGetTransactionsUseCase(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<List<Transactions>> ExecuteAsync(int id)
        {
            return await _transactionRepo.GetTransactionsById(id);
        }
    }
}
