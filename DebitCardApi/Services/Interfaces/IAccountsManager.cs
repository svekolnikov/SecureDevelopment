using DebitCardApi.DTO;

namespace DebitCardApi.Services.Interfaces
{
    public interface IAccountsManager
    {
        Task RegisterAsync(RegistrationUserDto user);
    }
}
