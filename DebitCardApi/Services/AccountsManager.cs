using System.Security.Claims;
using DebitCardApi.DAL.IdentityContext;
using DebitCardApi.DAL.Models.Identity;
using DebitCardApi.DTO;
using DebitCardApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DebitCardApi.Services
{
    public class AccountsManager : IAccountsManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AccountsManager(
            ApplicationDbContext applicationDbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegistrationUserDto dto)
        {
            var user = new ApplicationUser
            {
                Email = dto.Email,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "user"));
            }
            else
            {
                
            }
        }
    }
}
