using DebitCardApi.DTO;

namespace DebitCardApi.Services.Interfaces
{
    public interface IAccountsManager
    {
        Task<IServiceResult> RegisterAsync(RegistrationUserDto user, CancellationToken cancellationToken = default);
        Task<IServiceResult<string>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    }
}
