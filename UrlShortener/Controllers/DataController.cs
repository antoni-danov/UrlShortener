using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrlShortener.ActionFilters;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : Controller
    {
        readonly IShortService shortService;


        public DataController(IShortService shortService)
        {
            this.shortService = shortService;
        }

        [HttpGet("/{data}")]
        public async Task<IActionResult> GetUrl([FromRoute] string data)
        {
            var result = this.shortService.GetOriginalUrl(data);

            return new RedirectResult(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFiltersAttribute))]
        public async Task<IActionResult> CreateAsync([FromBody] UrlData data)
        {
            
            var ifExist = IsCreated(data.OriginalUrl);

            if (ifExist == null)
            {
                var currentUrl =  shortService.CreateUrlRecord(data);
                return StatusCode(201, currentUrl);
            }
            else if (ifExist != null)
            {
                if (ifExist.Uid == "N/A")
                {
                    var currentUrl = shortService.CreateUrlRecord(data);
                    return StatusCode(201, currentUrl);
                }
            }

            return StatusCode(200, ifExist);
        }

        public ExistingUrlRecord IsCreated(string originalUrl)
        {

            return this.shortService.isCreated(originalUrl);


        }

    }
}
