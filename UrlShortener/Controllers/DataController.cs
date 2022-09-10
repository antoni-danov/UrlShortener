using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Create([FromBody] UrlData data)
        {
            try
            {

                if (data == null)
                {
                    return BadRequest("Data object is null.");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object.");
                }
                if (isCreated(data.OriginalUrl) == false)
                {
                   await shortServices.CreateUrlRecord(data);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return StatusCode(201, data);
        }

        public bool isCreated(string originalUrl)
        {

            return this.shortServices.isCreated(originalUrl);

        }

    }
}
