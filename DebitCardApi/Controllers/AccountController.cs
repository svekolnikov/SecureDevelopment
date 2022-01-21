using System.Runtime.CompilerServices;
using DebitCardApi.DTO;
using DebitCardApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                await _accountsManager.RegisterAsync(userDto);
            }
            catch (Exception e)
            {
                LogError(e);
                BadRequest("Error 500");
            }

            return Ok(new { Message = "User registered." });
        }

        private void LogError(Exception e, [CallerMemberName] string methodName = null!)
        {
            _logger.LogError(e, "Error at {0}", methodName);
        }
    }
}
