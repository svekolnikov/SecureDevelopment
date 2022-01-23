using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using DebitCardApi.DAL.IdentityContext;
using DebitCardApi.DAL.Models.Identity;
using DebitCardApi.DTO;
using DebitCardApi.Services.Interfaces;
using DebitCardApi.Services.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DebitCardApi.Services.Identity
{
    public class AccountsManager : IAccountsManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AccountsManager> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountsManager(
            ApplicationDbContext applicationDbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService,
            ILogger<AccountsManager> logger, IHttpContextAccessor contextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task<IServiceResult> RegisterAsync(RegistrationUserDto dto, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "user"));
                return new ServiceResult{ IsSuccess  = true};
            }

            var strBuilder = new StringBuilder();
            var failures = new List<IFailureInformation>();
            foreach (var error in result.Errors)
            {
                strBuilder.AppendLine(error.Description);
                failures.Add(new FailureInformation{ Description = error.Description });
            }
                
            LogError(strBuilder.ToString());
            var serviceResult = new ServiceResult { IsSuccess = false , Failures = new List<IFailureInformation>(failures)};
            return serviceResult;
        }

        public async Task<IServiceResult<string>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
        {
            //Find user in database
            var user = await _applicationDbContext.Users.SingleOrDefaultAsync(x =>
                x.Email == dto.Email, 
                cancellationToken);

            if (user == null)
                return new ServiceResult<string>() { IsSuccess = false };

            //Validate password
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!signInResult.Succeeded)
                throw new HttpStatusException(HttpStatusCode.Unauthorized, "User unauthorized");

            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

            //Generate JWT with claims  
            var jwt = _jwtService.GenerateToken(claims);
            return new ServiceResult<string>() { IsSuccess = true , Data = jwt};
        }


        private void LogError(Exception e, [CallerMemberName] string methodName = null!)
        {
            _logger.LogError(e, "Error at {0}", methodName);
        }

        private void LogError(string e, [CallerMemberName] string methodName = null!)
        {
            _logger.LogError(e, "Error at {0}", methodName);
        }
    }
}
