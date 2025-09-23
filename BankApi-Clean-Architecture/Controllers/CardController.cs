using BankApi.Application.DTOs;
using BankApi.Application.UseCases.Interfaces.ICard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BankApi_Clean_Architecture.Controllers
{
    [ApiController]
    [Route("api/cards")]
    [Authorize(Roles = "User,Admin")]
    public class CardController : ControllerBase
    {
        private readonly IAddCardUseCase _addCardUseCase;
        private readonly IGetCardUseCase _getCardUseCase;
        private readonly IRemoveCardUseCase _removeCardUseCase;

        public CardController(IAddCardUseCase addCardUseCase, IGetCardUseCase getCardUseCase, IRemoveCardUseCase removeCardUseCase)
        {
            _addCardUseCase = addCardUseCase;
            _getCardUseCase = getCardUseCase;
            _removeCardUseCase = removeCardUseCase;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardInfo(int id)
        {
            var card = await _getCardUseCase.ExecuteAsync(id);

            if (card == null)
            {
                return NotFound(new { message = "Error, not found!" });
            }

            return Ok(new { message = "Card Info", card.Id, card.CardHoLderName, card.Number, card.CCV, card.ExpirationDate });
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var card = await _addCardUseCase.ExecuteAsync(dto);

            if (card == null)
            {
                return NotFound(new { message = "Error, not found!" });
            }

            return Ok(new { message = "Card has been added successfully!", card.Id, card.CardHoLderName, card.Number, card.CCV, card.ExpirationDate });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCard(int id)
        {
            var card = await _removeCardUseCase.ExecuteAsync(id);

            if (card == null)
                return NotFound(new { message = $"Card with id {id} not found." });

            return Ok(new { message = "Card has been removed successfully!", card.Id, card.CardHoLderName, card.Number, card.CCV, card.ExpirationDate });
        }

    }
}
