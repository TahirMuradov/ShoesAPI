using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Core.Utilites.Results.Abstract
{
    public interface IDataResult<T> : IResult
    {
        T Response { get; }
    }
}
