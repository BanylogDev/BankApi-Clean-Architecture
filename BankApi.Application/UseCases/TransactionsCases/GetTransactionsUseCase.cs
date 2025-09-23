using BankApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApi.Domain.Entities;
using BankApi.Application.DTOs;
using BankApi.Application.UseCases.Interfaces.IAdmin;
using BankApi.Application.UseCases.Interfaces.ITransactions;


namespace BankApi.Application.UseCases.TransactionsCases
{
    public class GetTransactionsUseCase : IGetTransactionsUseCase
    {
        private readonly ITransactionRepository _transactionRepo;

        public GetTransactionsUseCase(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<List<Transactions>> ExecuteAsync(int id) 
        {
            return await _transactionRepo.GetTransactionsById(id);
        }  
    }
}
