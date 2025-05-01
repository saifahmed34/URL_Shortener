using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Data;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UrlShorteningService _urlShorteningService;
        public UrlShortenerController(AppDbContext db , UrlShorteningService url) {
        _appDbContext = db;
        _urlShorteningService = url;
        }

    }
}
