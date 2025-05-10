namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class PasswordHelper
    {
        public static string GetPasswordHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool CheckPassword(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
