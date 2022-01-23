namespace DebitCardApi.Services.Interfaces
{
    public interface IServiceResult
    {
        bool IsSuccess { get; }

        public IReadOnlyCollection<IFailureInformation> Failures { get; }
    }

    public interface IServiceResult<T> : IServiceResult
    {
        T Data { get; }
    }
}
