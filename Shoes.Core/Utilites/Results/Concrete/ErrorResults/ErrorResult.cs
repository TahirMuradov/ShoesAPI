using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shoes.Core.Utilites.Results.Concrete;

namespace Shoes.Core.Utilites.Results.Concrete.ErrorResults
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message, HttpStatusCode statusCode) : base(false, message, statusCode)
        {
        }

        public ErrorResult(HttpStatusCode statusCode) : base(false, statusCode)
        {
        }
    }
}
