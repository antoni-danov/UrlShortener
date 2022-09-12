using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        [HttpGet("/{data}")]
        public async Task<IActionResult> GetUrl([FromRoute] string data)
        {
            var result = this.shortServices.GetOriginalUrl(data);
            return new RedirectResult(result);
        }

        // Post: DataController/Create
        [HttpPost]
        [ServiceFilter(typeof(ValidationFiltersAttribute))]
        public async Task<IActionResult> Create([FromBody] UrlData data)
        {
            if (isCreated(data.OriginalUrl) == false)
            {
                await shortServices.CreateUrlRecord(data);
            }

            return StatusCode(201, data);
        }

        public bool isCreated(string originalUrl)
        {

            return this.shortServices.isCreated(originalUrl);

        }

    }
}
