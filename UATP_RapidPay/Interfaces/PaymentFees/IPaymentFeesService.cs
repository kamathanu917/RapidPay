namespace UATP_RapidPay.Interfaces.PaymentFees
{
    public interface IPaymentFeesService
    {
        decimal CalculatePaymentFee(decimal lastFeeAmount);
    }
}
