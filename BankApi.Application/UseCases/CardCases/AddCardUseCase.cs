using BankApi.Application.DTOs;
using BankApi.Application.UseCases.Interfaces.ICard;
using BankApi.Domain.Entities;
using BankApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankApi.Application.UseCases.CardCases
{
    public class AddCardUseCase : IAddCardUseCase
    {
        private readonly ICardRepository _cardRepo;

        public AddCardUseCase(ICardRepository cardRepo)
        {
            _cardRepo = cardRepo;
        }

        public async Task<Cards?> ExecuteAsync(CardDto dto)
        {
            var existingCard = await _cardRepo.GetCardByNumber(dto.Number);

            if (existingCard != null)
            {
                return null;
            }


            var newCard = new Cards
            {
                CardHoLderName = dto.CardHoLderName,
                Number = dto.Number.Trim(),
                CCV = dto.CCV,
                UserId = dto.UserId,
                ExpirationDate = dto.ExpirationDate,
                AddedAt = DateTime.UtcNow,

            };

            return await _cardRepo.AddCard(newCard);
        }
    }
}
