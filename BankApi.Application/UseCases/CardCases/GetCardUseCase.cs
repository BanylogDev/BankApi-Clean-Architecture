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
    public class GetCardUseCase : IGetCardUseCase
    {
        private readonly ICardRepository _cardRepo;

        public GetCardUseCase(ICardRepository cardRepo)
        {
            _cardRepo = cardRepo;
        }

        public async Task<Cards?> ExecuteAsync(int id)
        {
            return await _cardRepo.GetCardById(id);
        }
    }
}
