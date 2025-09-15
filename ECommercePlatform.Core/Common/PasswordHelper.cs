using System.Text;

namespace ECommercePlatform.Core.Common
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (string.IsNullOrEmpty(hashedPassword))
                return false;

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public static string GenerateRandomPassword(int length = 12, bool includeSpecialChars = true)
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            var chars = lowercase + uppercase + digits;
            if (includeSpecialChars)
                chars += special;

            var random = new Random();
            var password = new StringBuilder(length);

            password.Append(lowercase[random.Next(lowercase.Length)]);
            password.Append(uppercase[random.Next(uppercase.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            
            if (includeSpecialChars)
                password.Append(special[random.Next(special.Length)]);

            for (int i = password.Length; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return new string(password.ToString().OrderBy(x => random.Next()).ToArray());
        }

        public static int GetPasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                return 0;

            int score = 0;

            if (password.Length >= 8) score += 20;
            if (password.Length >= 12) score += 10;

            if (password.Any(char.IsLower)) score += 10;
            if (password.Any(char.IsUpper)) score += 10;
            if (password.Any(char.IsDigit)) score += 10;
            if (password.Any(c => "!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c))) score += 20;

            if (password.All(char.IsLetterOrDigit)) score -= 10;
            if (password.All(char.IsDigit)) score -= 20;
            if (password.All(char.IsLetter)) score -= 10;

            return Math.Max(0, Math.Min(100, score));
        }
    }
}
