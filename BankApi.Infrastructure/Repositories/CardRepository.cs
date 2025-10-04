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
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        private readonly ICacheService _cacheService;

        public CardRepository(AppDbContext context, ICacheService cache)
        {
            _context = context;
            _cacheService = cache;
        }

        public async Task<Cards?> GetCardById(int id)
        {


            var cacheKey = $"cards:{id}:recent";

            // check cache
            var cached = await _cacheService.GetAsync(cacheKey);

            if (cached != null)
            {
                Console.WriteLine("Cache Hit");
                return JsonSerializer.Deserialize<Cards>(cached);
            }

            Console.WriteLine("Cache Miss");

            var card = await _context.Cards.FindAsync(id);

            // cache result
            await _cacheService.SetAsync(cacheKey,
                JsonSerializer.Serialize(card),
                TimeSpan.FromMinutes(2));

            return card;


        }

        public async Task<Cards?> GetCardByNumber(string number)
        {

            var cacheKey = $"cards:{number}:recent";

            // check cache
            var cached = await _cacheService.GetAsync(cacheKey);

            if (cached != null)
            {
                Console.WriteLine("Cache Hit");
                return JsonSerializer.Deserialize<Cards>(cached);
            }

            Console.WriteLine("Cache Miss");

            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Number == number);

            // cache result
            await _cacheService.SetAsync(cacheKey,
                JsonSerializer.Serialize(card),
                TimeSpan.FromMinutes(2));

            return card;
        }

        public async Task<Cards?> AddCard(Cards newCard)
        {
            await _context.Cards.AddAsync(newCard);

            await _context.SaveChangesAsync();

            return newCard;
        }

        public void RemoveCard(Cards card)
        {
            _context.Cards.Remove(card);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
