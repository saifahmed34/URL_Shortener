using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;

namespace URL_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RedirectController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var url = await _context.Urls
                .FirstOrDefaultAsync(x => x.ShortCode == shortCode);

            if (url == null)
                return NotFound("Short URL not found");

            url.ClickCount++;
            await _context.SaveChangesAsync();

            return Redirect(url.OriginalUrl);
        }
    }
}
