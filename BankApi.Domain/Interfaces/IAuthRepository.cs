using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByName(string name);
        Task<User?> GetUserByEmail(string email);
        Task<User?> Register(User newUser);
        Task SaveChangesAsync();
    }
}
