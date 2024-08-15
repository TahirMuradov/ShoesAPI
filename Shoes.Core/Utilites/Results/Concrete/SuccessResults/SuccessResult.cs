using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shoes.Core.Utilites.Results.Concrete;

namespace Shoes.Core.Utilites.Results.Concrete.SuccessResults
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message, HttpStatusCode statusCode) : base(true, message, statusCode)
        {
        }
        public SuccessResult(List<string> messages, HttpStatusCode statusCode) : base(true, messages, statusCode)
        {
        }
        public SuccessResult(HttpStatusCode statusCode) : base(true, statusCode)
        {
        }
    }
}
