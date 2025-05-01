namespace URL_Shortener.Services
{
    public class UrlShorteningService
    {
        private const string Alphapet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random _random = new Random();

        public string UrlShortening(int len = 6)
        {
            var chars = new char[len];
            for (int i = 0; i < len; i++)
            {
                chars[i] = Alphapet[_random.Next(Alphapet.Length)];

            }
            return new string(chars);
        }
        public string GenUrl(string url)
        {
            return $"https://saif.com/{url}";
        }
    }
}
