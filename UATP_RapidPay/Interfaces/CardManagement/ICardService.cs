using System.Threading.Tasks;
using UATP_RapidPay.Models;

namespace UATP_RapidPay.Interfaces.CardManagement
{
    public interface ICardService
    {
        Task<Card> CreateCardAsync();
        Task<bool> PayAsync(int cardId, decimal amount);
        Task<decimal> GetCardBalanceAsync(int cardId);
    }
}
