using Microsoft.AspNetCore.Mvc;
using UATP_RapidPay.Interfaces.PaymentFees;

namespace UATP_RapidPay.Controllers.PaymentFees
{
    [Route("api/paymentfees")]
    [ApiController]
    public class PaymentFeesController : ControllerBase
    {
        private readonly IPaymentFeesService _paymentFeesService;

        public PaymentFeesController(IPaymentFeesService paymentFeesService)
        {
            _paymentFeesService = paymentFeesService;
        }

        [HttpGet("calculate")]
        public ActionResult<decimal> CalculatePaymentFee(decimal lastFeeAmount)
        {
            decimal calculatedFee = _paymentFeesService.CalculatePaymentFee(lastFeeAmount);
            return Ok(calculatedFee);
        }
    }
}
