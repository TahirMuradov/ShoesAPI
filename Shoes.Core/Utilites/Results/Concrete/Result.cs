using Shoes.Core.Utilites.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Core.Utilites.Results.Concrete
{
    public class Result : IResult
    {
        public bool IsSuccess { get; }

        public string Message { get; }
        public List<string> Messages { get; }
        public HttpStatusCode StatusCode { get; }
        public Result(bool IsSuccess, HttpStatusCode statusCode)
        {
            this.IsSuccess = IsSuccess;
            StatusCode = statusCode;
        }
        public Result(bool IsSuccess, string message, HttpStatusCode statusCode) : this(IsSuccess, statusCode)
        {
            Message = message;

        }
        public Result(bool IsSuccess, List<string> messages, HttpStatusCode statusCode) : this(IsSuccess, statusCode)
        {
            Messages = messages;

        }
    }
}
