using BankApi.Application.UseCases.Interfaces.ICard;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.UseCases.CardCases
{
    public class RemoveCardUseCase : IRemoveCardUseCase
    {
        private readonly ICardRepository _cardRepo;

        public RemoveCardUseCase(ICardRepository cardRepo)
        {
            _cardRepo = cardRepo;
        }

        public async Task<Cards?> ExecuteAsync(int id)
        {
            var card = await _cardRepo.GetCardById(id);

            if (card == null)
            {
                return null;
            }

            _cardRepo.RemoveCard(card);

            await _cardRepo.SaveChangesAsync();

            return card;
        }
    }
}
