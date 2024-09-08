using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.PaymentMethodDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [HttpPost("[action]")]
        public IActionResult AddPaymentMenthod([FromBody] AddPaymentMethodDTO addPayment, [FromHeader] string LangCode)
        {
            var result = _paymentMethodService.AddPaymentMethod(addPayment, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdatePaymentMethod([FromBody]UpdatePaymentMethodDTO updatePayment,[FromHeader] string LangCode)
        {
            var result = _paymentMethodService.UpdatePaymenthod(updatePayment, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult DeletePaymentMethod([FromQuery]Guid Id)
        {
            var result = _paymentMethodService.DeletePaymentmethod(Id);
               return result.IsSuccess ? Ok(result) :BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetPaymentMethoForUpdate([FromQuery]Guid Id)
        {
            var result=_paymentMethodService.GetPaymentMethodForUpdate(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetPaymentMethod([FromQuery] Guid Id, [FromHeader] string LangCode)
        {
            var result = _paymentMethodService.GetPaymentMethod(Id, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPaymentMethod([FromQuery] int page, [FromHeader] string LangCode)
        {
            var result = await _paymentMethodService.GetAllPaymentmethodAsync(LangCode, page);
            return result.IsSuccess? Ok(result) : BadRequest(result);
        }
    }
}
