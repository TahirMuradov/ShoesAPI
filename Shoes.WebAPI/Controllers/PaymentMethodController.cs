using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AllRole")]
        public IActionResult AddPaymentMenthod([FromBody] AddPaymentMethodDTO addPayment, [FromHeader] string LangCode)
        {
            var result = _paymentMethodService.AddPaymentMethod(addPayment, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPut("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult UpdatePaymentMethod([FromBody]UpdatePaymentMethodDTO updatePayment,[FromHeader] string LangCode)
        {
            var result = _paymentMethodService.UpdatePaymenthod(updatePayment, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpDelete("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult DeletePaymentMethod([FromQuery]Guid Id)
        {
            var result = _paymentMethodService.DeletePaymentmethod(Id);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult GetPaymentMethoForUpdate([FromQuery]Guid Id)
        {
            var result=_paymentMethodService.GetPaymentMethodForUpdate(Id);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult GetPaymentMethod([FromQuery] Guid Id, [FromHeader] string LangCode)
        {
            var result = _paymentMethodService.GetPaymentMethod(Id, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "AllRole")]
        public async Task<IActionResult> GetAllPaymentMethod([FromQuery] int page, [FromHeader] string LangCode)
        {
            var result = await _paymentMethodService.GetAllPaymentmethodAsync(LangCode, page);
            return StatusCode((int)result.StatusCode, result); ;
        }
        [HttpGet("[action]")]
        public IActionResult GetAllPaymentMethodForUI([FromHeader]string LangCode)
        {
            var result=_paymentMethodService.GetPaymentMethodForUI(LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
