using Microsoft.AspNetCore.Mvc;
using BankApi.Application.UseCases;
using BankApi.Application.UseCases.Interfaces.IUser;
using BankApi.Application.DTOs;

namespace BankApi_Clean_Architecture.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginUserUseCase _loginUserUseCase;
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IRefreshTokenUseCase _refreshTokenUseCase;

        public AuthController(ILoginUserUseCase loginUserUseCase, IRegisterUserUseCase registerUserUseCase, IRefreshTokenUseCase refreshTokenUseCase)
        {
            _loginUserUseCase = loginUserUseCase;
            _registerUserUseCase = registerUserUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _loginUserUseCase.ExecuteAsync(dto);
            if (user == null) return Unauthorized("Invalid username or password");

            return Ok(new { message = "Login successful", user.Id, user.Name, user.Email, user.Balance, user.AccessToken, user.RefreshToken });
        }



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDTO tokenDto)
        {
            if (tokenDto is null)
                return BadRequest("Invalid client request");

            var tokens = await _refreshTokenUseCase.ExecuteAsync(tokenDto);

            return Ok(new
            {
                token = tokens?.AccessToken,
                refreshToken = tokens?.RefreshToken
            });
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _registerUserUseCase.ExecuteAsync(dto);
            return Ok(new { message = "Registration successful", user.Name, user.Email });
        }
    }
}
