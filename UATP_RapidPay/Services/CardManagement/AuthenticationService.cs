using System.Collections.Generic;

namespace UATP_RapidPay.Services.CardManagement
{
    public class AuthenticationService
    {
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "username1", "password1" },
            { "username2", "password2" },
            { "username3", "password3" }
        };

        public bool IsAuthenticated(string username, string password)
        {
            if (_users.TryGetValue(username, out var storedPassword))
            {
                // Basic authentication using comparing passwords
                return storedPassword == password;
            }
            return false;
        }
    }
}
