using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.PaymentMethodDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface IPaymentMethodDAL
    {
        public IResult AddPaymentMethod(AddPaymentMethodDTO addPayment);
        public IResult UpdatePaymenthod(UpdatePaymentMethodDTO updatePayment);
        public Task<IDataResult<PaginatedList<GetPaymentMethodDTO>>> GetAllPaymentmethodAsync(string LangCode,int page=1);
        public IDataResult<GetPaymentMethodDTO>GetPaymentMethod(Guid Id,string LangCode);
        public IDataResult<GetPaymentMethodForUpdateDTO> GetPaymentMethodForUpdate(Guid Id);
        public IResult DeletePaymentmethod(Guid Id);


    }
}
