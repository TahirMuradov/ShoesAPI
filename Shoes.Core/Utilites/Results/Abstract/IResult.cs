using System.Net;

namespace Shoes.Core.Utilites.Results.Abstract
{
    public interface IResult
    {
        public bool IsSuccess { get; }
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
    }
}
