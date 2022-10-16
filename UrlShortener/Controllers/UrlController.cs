using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.ActionFilters;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api/url")]
    [ApiController]
    public class UrlController : Controller
    {
        readonly IShortService shortService;


        public UrlController(IShortService shortService)
        {
            this.shortService = shortService;
        }
       
        [HttpGet]
        [Route("search/all/{uid}")]
        public async Task<ActionResult<List<UrlData>>> GetAllAsync([FromRoute] string uid)
        {
            var result = await shortService.GetAllAsync(uid);
            return result.ToList();
        }

        [HttpGet]
        [Route("urlById/{id}")]
        public ActionResult<UrlData> GetUrlById([FromRoute] int id)
        {
            var url = shortService.GetUrlById(id);

            return StatusCode(200, url);
        }

        [HttpGet]
        [Route("/{data}")]
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

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            shortService.DeleteUrl(id);

            return StatusCode(200);
        }
        public ExistingUrlRecord IsCreated(string originalUrl)
        {

            return this.shortService.isCreated(originalUrl);


        }

    }
}
