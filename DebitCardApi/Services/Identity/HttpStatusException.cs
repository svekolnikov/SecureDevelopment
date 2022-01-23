using System.Net;

namespace DebitCardApi.Services.Identity
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode Status { get; }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
