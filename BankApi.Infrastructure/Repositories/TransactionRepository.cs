using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApi.Domain.Interfaces;
using BankApi.Domain.Entities;
using BankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace BankApi.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transactions>> GetTransactionsById(int userId)
        {
            return await _context.Transactions
                .Where(t => t.SenderId == userId || t.ReceiverId == userId)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }   

        public async Task<Transactions> CreateTransaction(Transactions newTransaction)
        {
            await _context.Transactions.AddAsync(newTransaction);

            await _context.SaveChangesAsync();

            return newTransaction;
        }
    }
}
