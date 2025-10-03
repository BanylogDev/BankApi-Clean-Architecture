using BankApi.Application.Interfaces;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using BankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace BankApi.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly ICacheService _cacheService;

        public TransactionRepository(AppDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<List<Transactions>> GetTransactionsById(int userId)
        {

            var cacheKey = $"transactions:{userId}:recent";

            // check cache
            var cached = await _cacheService.GetAsync(cacheKey);

            if (cached != null)
            {
                Console.WriteLine("Cache Hit");
                return JsonSerializer.Deserialize<List<Transactions>>(cached);
            }

            Console.WriteLine("Cache Miss");

            // fallback to DB
            var transactions = await _context.Transactions
                .Where(t => t.SenderId == userId || t.ReceiverId == userId)
                .OrderBy(t => t.Date)
                .ToListAsync();

            // cache result
            await _cacheService.SetAsync(cacheKey,
                JsonSerializer.Serialize(transactions),
                TimeSpan.FromMinutes(2));

            return transactions;
        }   

        public async Task<Transactions> CreateTransaction(Transactions newTransaction)
        {
            await _context.Transactions.AddAsync(newTransaction);

            await _context.SaveChangesAsync();

            return newTransaction;
        }
    }
}
