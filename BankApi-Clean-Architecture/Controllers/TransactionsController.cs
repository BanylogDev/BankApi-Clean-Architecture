using BankApi.Application.DTOs;
using BankApi.Application.UseCases.Interfaces.ITransactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankApi_Clean_Architecture.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class TransactionsController : ControllerBase
    {
        private readonly IGetTransactionsUseCase _getTransactionUseCase;
        private readonly ISendFundsUseCase _sendFundsUseCase;

        public TransactionsController(IGetTransactionsUseCase getTransactionUseCase, ISendFundsUseCase sendFundsUseCase )
        {
            _getTransactionUseCase = getTransactionUseCase;
            _sendFundsUseCase = sendFundsUseCase;
        }

        [HttpPost()]
        public async Task<IActionResult> SendMoney([FromBody] TransactionsDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = await _sendFundsUseCase.ExecuteAsync(dto);

            if (transaction == null)
            {
                return NotFound(new { message = "Error, not found!" });
            }

            return Ok(new
            {
                message = "Transaction has been completed successfully!",
                transaction?.SenderId,
                transaction?.ReceiverId,
                transaction?.Amount,
                transaction?.Date,
            });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonalTransactions(int id)
        {

            var stopwatch = Stopwatch.StartNew();

            var transactions = await _getTransactionUseCase.ExecuteAsync(id);

            if (transactions == null)
            {
                return NotFound(new { message = "Error, not found!" });
            }
            
            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return Ok(new
            {
                message = "Transactions History",
                transactions

            });
        }

    }
}
