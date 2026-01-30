using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;
using URL_Shortener.Models;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UrlShorteningService _urlShorteningService;

        public UrlShortenerController(
            AppDbContext context,
            UrlShorteningService urlShorteningService)
        {
            _context = context;
            _urlShorteningService = urlShorteningService;
        }

        // POST: api/UrlShortener/shorten
        [HttpPost("shorten")]
        public async Task<ActionResult<UrlResponse>> ShortenUrl([FromBody] UrlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.OriginalUrl))
                return BadRequest("URL is required");

            var originalUrl = request.OriginalUrl.Trim();

            //auto-add scheme if missing
            if (!originalUrl.StartsWith("http://") &&
                !originalUrl.StartsWith("https://"))
            {
                originalUrl = "https://" + originalUrl;
            }

        
            var existingUrl = await _context.Urls
            .FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl);

            if (existingUrl != null)
            {
                return Ok(new UrlResponse
                {
                    OriginalUrl = existingUrl.OriginalUrl,
                    ShortUrl = _urlShorteningService
                    .GetShortUrl(existingUrl.ShortCode, Request)
                });
            }

            // generate short code
            string shortCode;
            do
            {
                shortCode = _urlShorteningService.GenerateShortCode();
            }
            while (await _context.Urls.AnyAsync(x => x.ShortCode == shortCode));

            var url = new Url
            {
                OriginalUrl = originalUrl,
                ShortCode = shortCode,
                CreatedDate = DateTime.UtcNow,
                ClickCount = 0
            };

            _context.Urls.Add(url);
            await _context.SaveChangesAsync();

            return Ok(new UrlResponse
            {
                OriginalUrl = originalUrl,
                ShortUrl = _urlShorteningService.GetShortUrl(shortCode, Request)
            });
        }

    }
}
