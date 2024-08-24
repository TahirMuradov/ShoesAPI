using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.PaymentMethodDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IPaymentMethodService
    {
        public IResult AddPaymentMethod(AddPaymentMethodDTO addPayment,string LangCode);
        public IResult UpdatePaymenthod(UpdatePaymentMethodDTO updatePayment, string LangCode);
        public Task<IDataResult<PaginatedList<GetPaymentMethodDTO>>> GetAllPaymentmethodAsync(string LangCode, int page = 1);
        public IDataResult<GetPaymentMethodDTO> GetPaymentMethod(Guid Id, string LangCode);
        public IResult DeletePaymentmethod(Guid Id);
    }
}
