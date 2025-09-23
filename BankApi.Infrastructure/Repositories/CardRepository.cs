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
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cards?> GetCardById(int id)
        {
            return await _context.Cards.FindAsync(id);
        }

        public async Task<Cards?> GetCardByNumber(string number)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.Number == number);
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
