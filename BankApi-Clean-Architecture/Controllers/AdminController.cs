using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankApi.Application.Interfaces;
using BankApi.Domain.Interfaces;
using BankApi.Application.UseCases.AdminCases;
using BankApi.Application.UseCases.Interfaces.IAdmin;

namespace BankApi_Clean_Architecture.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminGetUserUseCase _getUserUseCase;
        private readonly IAdminGetTransactionsUseCase _getTransactionsUseCase;

        public AdminController(IAdminGetUserUseCase getUserUseCase, IAdminGetTransactionsUseCase getTransactionsUseCase)
        {
            _getUserUseCase = getUserUseCase;
            _getTransactionsUseCase = getTransactionsUseCase;
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            var user = await _getUserUseCase.ExecuteAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "Error, not found!" });
            }

            return Ok(new
            {
                message = $"{user?.Name}'s Info",
                user?.Id,
                user?.Name,
                user?.Email,
                user?.Role,
                user?.CreatedAt,
            });
        }

        [HttpGet("transactions/{id}")]
        public async Task<IActionResult> GetAnyoneTransactions(int id)
        {
            var transactions = await _getTransactionsUseCase.ExecuteAsync(id);

            if (transactions == null)
            {
                return NotFound(new { message = "Error, not found!" });
            }

            return Ok(new
            {
                message = "Transactions History",
                transactions

            });
        }
    }
}
