using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shoes.Core.Utilites.Results.Concrete;

namespace Shoes.Core.Utilites.Results.Concrete.SuccessResults
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T response, string message, HttpStatusCode statusCode) : base(response, true, message, statusCode)
        {
        }

        public SuccessDataResult(T response, HttpStatusCode statusCode) : base(response, true, statusCode)
        {
        }

        public SuccessDataResult(string message, HttpStatusCode statusCode) : base(default, true, message, statusCode)
        {
        }
        public SuccessDataResult(List<string> message, HttpStatusCode statusCode) : base(default, true, message, statusCode)
        {
        }
        public SuccessDataResult(HttpStatusCode statusCode) : base(default, true, statusCode)
        {
        }
    }
}
