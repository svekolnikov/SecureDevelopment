using DebitCardApi.Services.Interfaces;

namespace DebitCardApi.Services.Results
{
    public class FailureInformation : IFailureInformation
    {
        public string Description { get; init; }
    }
}
