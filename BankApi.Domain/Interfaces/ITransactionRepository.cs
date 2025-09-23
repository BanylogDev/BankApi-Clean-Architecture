using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transactions>> GetTransactionsById(int userId);
        Task<Transactions> CreateTransaction(Transactions newTransaction);
    }
}
