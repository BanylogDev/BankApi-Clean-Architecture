using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Interfaces
{
    public interface ICardRepository
    {
        Task<Cards?> GetCardById(int id);
        Task<Cards?> GetCardByNumber(string number);
        Task<Cards?> AddCard(Cards newCard);
        void RemoveCard(Cards card);
        Task SaveChangesAsync();
    }
}
