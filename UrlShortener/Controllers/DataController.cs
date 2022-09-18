using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrlShortener.ActionFilters;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private IShortServices shortServices;
       

        public DataController(IShortServices shortServices)
        {
            this.shortServices = shortServices;
        }

        // GET: DataController
        [HttpGet("{id}")]
        public IActionResult GetUrl([FromRoute] string id)
        {
            var result = this.shortServices.GetOriginalUrl(id);

            return new RedirectResult(result);
        }

        // Post: DataController/Create
        [HttpPost]
        [ServiceFilter(typeof(ValidationFiltersAttribute))]
        public async Task<IActionResult> Create([FromBody] UrlData data)
        {
            var ifExist = isCreated(data.OriginalUrl);

            if (ifExist == null)
            {
                var url = new UrlData
                {
                    UrlId = data.UrlId,
                    OriginalUrl = data.OriginalUrl,
                    ShortUrl = data.ShortUrl,
                    CreatedOn = data.CreatedOn
                };

                await shortServices.CreateUrlRecord(url);
                return  StatusCode(201, url);
            }

            return StatusCode(200, ifExist);
        }

        public ExistingUrlRecord isCreated(string originalUrl)
        {

            return this.shortServices.isCreated(originalUrl);


        }

    }
}
