using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<Transactions>> GetTransactionHistoryAsync(int id);
        Task<User?> GetUserInfoAsync(int id);
    }
}
