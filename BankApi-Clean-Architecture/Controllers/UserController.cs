using BankApi.Application.DTOs;
using BankApi.Application.UseCases.Interfaces.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankApi_Clean_Architecture.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly IGetUserUseCase _getUserUseCase;

        public UserController(IUpdateUserUseCase updateUserUseCase, IDeleteUserUseCase deleteUserUseCase, IGetUserUseCase getUserUseCase    )
        {
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _getUserUseCase = getUserUseCase;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _updateUserUseCase.ExecuteAsync(dto);

            if (user == null)
                return NotFound(new { message = $"User with id {id} not found" });

            return Ok(new { message = "Update successful", user.Email });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteAccount(int id, [FromBody] DeleteAccountDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _deleteUserUseCase.ExecuteAsync(dto);

            if (user == null)
                return NotFound(new { message = $"User with id {id} not found" });

            return Ok(new { message = "Account has been deleted successfully", user.Name });
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetUserInfo(int id)
        {

            var stopwatch = Stopwatch.StartNew();   

            var user = await _getUserUseCase.ExecuteAsync(id);

            if (user == null)
                return NotFound(new { message = $"User with id {id} not found" });

            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return Ok(new { message = $"{user?.Name}'s Info,", user?.Name, user?.Email, user?.PhoneNumber, user?.Role, user?.CreatedAt });
        }
    }
}
