using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task SaveChangesAsync();

        void RemoveUser(User user);
    }
}
