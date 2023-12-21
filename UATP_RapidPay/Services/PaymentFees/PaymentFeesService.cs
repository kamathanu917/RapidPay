using UATP_RapidPay.Interfaces.PaymentFees;

namespace UATP_RapidPay.Services.PaymentFees
{
    public class PaymentFeesService : IPaymentFeesService
    {
        private readonly IUfeService _ufeService;
        private decimal _lastFeeAmount = 1; // Initial fee amount

        public PaymentFeesService(IUfeService ufeService)
        {
            _ufeService = ufeService;
        }

        public decimal CalculatePaymentFee(decimal lastFeeAmount)
        {
            decimal randomDecimal = _ufeService.GetRandomDecimal();
            _lastFeeAmount = lastFeeAmount * randomDecimal;
            return _lastFeeAmount;
        }
    }
}
