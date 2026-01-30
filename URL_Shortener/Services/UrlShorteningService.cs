namespace URL_Shortener.Services
{
    public class UrlShorteningService
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random _random = new Random();

        public string GenerateShortCode(int len = 6)
        {
            var chars = new char[len];
            for (int i = 0; i < len; i++)
            {
                chars[i] = Alphabet[_random.Next(Alphabet.Length)];
            }
            return new string(chars);
        }

        public string GetShortUrl(string shortCode, HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}/{shortCode}";
        }
    }
}