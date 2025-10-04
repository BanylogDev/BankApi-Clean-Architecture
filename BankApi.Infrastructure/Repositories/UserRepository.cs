using BankApi.Application.Interfaces;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using BankApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ICacheService _cacheService;

        public UserRepository(AppDbContext context, ICacheService cache)
        {
            _context = context;
            _cacheService = cache;
        }

        public async Task<User?> GetUserById(int id)
        {


            var cacheKey = $"users:{id}:recent";

            // check cache
            var cached = await _cacheService.GetAsync(cacheKey);

            if (cached != null)
            {
                Console.WriteLine("Cache Hit");
                return JsonSerializer.Deserialize<User>(cached);
            }

            Console.WriteLine("Cache Miss");

            var user = await _context.Users.FindAsync(id);

            // cache result
            await _cacheService.SetAsync(cacheKey,
                JsonSerializer.Serialize(user),
                TimeSpan.FromMinutes(2));

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void RemoveUser(User user)
        {
            _context.Remove(user);
        }
    }
}
