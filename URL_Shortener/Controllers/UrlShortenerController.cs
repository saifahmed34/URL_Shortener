using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;
using URL_Shortener.Models;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UrlShorteningService _urlShorteningService;

        public UrlShortenerController(AppDbContext context, UrlShorteningService urlShorteningService)
        {
            _context = context;
            _urlShorteningService = urlShorteningService;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlRequest request)
        {
            if (!Uri.TryCreate(request.OriginalUrl, UriKind.Absolute, out _))
            {
                return BadRequest("Invalid URL format");
            }

            var existingUrl = await _context.Urls
                .FirstOrDefaultAsync(s => s.OriginalUrl == request.OriginalUrl);

            if (existingUrl != null)
            {
                return Ok(new UrlResponse
                {
                    ShortUrl = _urlShorteningService.GetShortUrl(existingUrl.ShortCode),
                    OriginalUrl = existingUrl.OriginalUrl
                });
            }

            var shortCode = _urlShorteningService.GenerateShortCode();
            var shortUrl = new Url
            {
                OriginalUrl = request.OriginalUrl,
                ShortCode = shortCode,
                CreatedDate = DateTime.UtcNow
            };

            _context.Urls.Add(shortUrl);
            await _context.SaveChangesAsync();

            return Ok(new UrlResponse
            {
                ShortUrl = _urlShorteningService.GetShortUrl(shortCode),
                OriginalUrl = request.OriginalUrl
            });
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectUrl(string shortCode)
        {
            var url = await _context.Urls
                .FirstOrDefaultAsync(s => s.ShortCode == shortCode);

            if (url == null)
            {
                return NotFound("Short URL not found");
            }

            url.ClickCount++;
            await _context.SaveChangesAsync();

            return Redirect(url.OriginalUrl);
        }
    }

    public class UrlRequest
    {
        public string OriginalUrl { get; set; }
    }

    public class UrlResponse
    {
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
    }
}