using System.Text.RegularExpressions;

namespace ECommercePlatform.Core.Common
{
    public static class StringHelper
    {
        public static string GenerateSlug(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var slug = text.ToLowerInvariant()
                .Replace(" ", "-")
                .Replace("_", "-");

            slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");

            slug = Regex.Replace(slug, @"-+", "-");

            slug = slug.Trim('-');

            return slug;
        }

        public static string GenerateRandomString(int length = 8, bool includeNumbers = true, bool includeSpecialChars = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            var availableChars = chars;
            if (includeNumbers) availableChars += numbers;
            if (includeSpecialChars) availableChars += special;

            var random = new Random();
            return new string(Enumerable.Repeat(availableChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains('@'))
                return email;

            var parts = email.Split('@');
            if (parts[0].Length <= 2)
                return $"{parts[0][0]}***@{parts[1]}";

            return $"{parts[0][0]}{new string('*', parts[0].Length - 2)}{parts[0][^1]}@{parts[1]}";
        }

        public static string MaskPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length < 4)
                return phone;

            var visibleChars = Math.Min(4, phone.Length);
            var maskedLength = phone.Length - visibleChars;
            var masked = new string('*', maskedLength);
            
            return phone[..visibleChars] + masked;
        }

        public static string Truncate(string text, int maxLength, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text[..(maxLength - ellipsis.Length)] + ellipsis;
        }

        public static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            var regex = new Regex(@"^[\+]?[0-9\s\-\(\)]{10,20}$");
            return regex.IsMatch(phone);
        }

        public static string ToTitleCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i][1..].ToLower();
                }
            }

            return string.Join(" ", words);
        }

        public static string GenerateInitials(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
                return string.Empty;

            if (words.Length == 1)
                return words[0][0].ToString().ToUpper();

            return (words[0][0].ToString() + words[^1][0].ToString()).ToUpper();
        }
    }
}
