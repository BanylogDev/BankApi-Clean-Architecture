using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using BankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Transactions>> GetTransactionHistoryAsync(int id)
        {

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return await _context.Transactions
                .Where(t => t.SenderId == user.Id || t.ReceiverId == user.Id)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

        public async Task<User?> GetUserInfoAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
