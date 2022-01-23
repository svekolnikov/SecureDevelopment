using System.Runtime.CompilerServices;
using DebitCardApi.DTO;
using DebitCardApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebitCardApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountsManager _accountsManager;

        public AccountController(ILogger<AccountController> logger, IAccountsManager accountsManager)
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegistrationUserDto userDto)
        {
            try
            {
                var serviceResult = await _accountsManager.RegisterAsync(userDto);
                if (serviceResult.IsSuccess)
                    return Ok(new { Message = "User registered." });

                var errors = new { Errors = serviceResult.Failures };
                return BadRequest(errors);
            }
            catch (Exception e)
            {
                LogError(e);
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            try
            {
                var serviceResult = await _accountsManager.LoginAsync(dto);
                if (serviceResult.IsSuccess)
                {
                    return Ok(new { Message = "User has logged.", Jwt = serviceResult });
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                LogError(e);
                return StatusCode(500);
            }
        }


        private void LogError(Exception e, [CallerMemberName] string methodName = null!)
        {
            _logger.LogError(e, "Error at {0}", methodName);
        }
    }
}
