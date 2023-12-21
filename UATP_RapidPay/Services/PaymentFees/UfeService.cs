using Microsoft.Extensions.Configuration;
using System;
using UATP_RapidPay.Interfaces.PaymentFees;

namespace UATP_RapidPay.Services.PaymentFees
{
    public class UfeService : IUfeService
    {
        private readonly Random _random = new Random();
        private readonly IConfiguration _configuration;

        public UfeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public decimal GetRandomDecimal()
        {
            var maxRandomValue = _configuration.GetValue<double>("UfeSettings:MaxRandomDecimalValue");
            return Convert.ToDecimal(_random.NextDouble() * maxRandomValue); // Random decimal between 0 and maxRandomValue (2)
        }
    }
}
