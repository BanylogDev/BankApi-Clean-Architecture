using BankApi.Application.DTOs;
using BankApi.Application.UseCases.Interfaces.ITransactions;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.TransactionsCases
{
    public class SendFundsUseCase : ISendFundsUseCase
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IUserRepository _userRepo;

        public SendFundsUseCase(ITransactionRepository transactionRepo, IUserRepository userRepo)
        {
            _transactionRepo = transactionRepo;
            _userRepo = userRepo;
        }

        public async Task<Transactions?> ExecuteAsync(TransactionsDTO dto)
        {
            var newTransaction = new Transactions
            {
                Amount = dto.Amount,
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
            };

            var senderUser = await _userRepo.GetUserById(dto.SenderId);

            var receiverUser = await _userRepo.GetUserById(dto.ReceiverId);

            if (senderUser == null || receiverUser == null)
            {
                return null;
            }

            senderUser.Balance = senderUser.Balance - dto.Amount;
            receiverUser.Balance = receiverUser.Balance + dto.Amount;

            return await _transactionRepo.CreateTransaction(newTransaction);
        }
    }
}
