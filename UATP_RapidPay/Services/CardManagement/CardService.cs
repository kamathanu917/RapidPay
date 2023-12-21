using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UATP_RapidPay.Interfaces.CardManagement;
using UATP_RapidPay.Models;

namespace UATP_RapidPay.Services.CardManagement
{
    public class CardService : ICardService
    {
        private readonly List<Card> _cards = new List<Card>();

        public async Task<Card> CreateCardAsync()
        {
            var card = new Card
            {
                Id = GenerateUniqueId(),
                CardNumber = GenerateCardNumber(),
                Balance = 0
            };

            _cards.Add(card);
            return card;
        }

        public async Task<bool> PayAsync(int cardId, decimal amount)
        {
            var card = _cards.FirstOrDefault(c => c.Id == cardId);
            if (card == null)
                return false;

            if (card.Balance >= amount)
            {
                card.Balance -= amount;
                return true;
            }

            return false;
        }

        public async Task<decimal> GetCardBalanceAsync(int cardId)
        {
            var card = _cards.FirstOrDefault(c => c.Id == cardId);
            return card?.Balance ?? 0;
        }

        private int GenerateUniqueId()
        {
            // Generating a unique ID logic like using a counter
            Random random = new Random();
            return random.Next(1000, 9999);
        }

        private string GenerateCardNumber()
        {
            // Generating a card number logic (example: a 15-digit format)
            Random random = new Random();
            return string.Join("", Enumerable.Range(1, 15).Select(_ => random.Next(0, 9)));
        }
    }
}
