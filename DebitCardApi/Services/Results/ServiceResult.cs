using DebitCardApi.Services.Interfaces;

namespace DebitCardApi.Services.Results
{
    public class ServiceResult : IServiceResult
    {
        public bool IsSuccess { get; init; }
        public IReadOnlyCollection<IFailureInformation> Failures { get; init;  }
    }

    public class ServiceResult<T> : IServiceResult<T> 
    {
        public T Data { get; init; }
        public bool IsSuccess { get; init; }
        public IReadOnlyCollection<IFailureInformation> Failures { get; }
    }
}
