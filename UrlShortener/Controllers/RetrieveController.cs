using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrlShortener.Controllers
{
    [Route("{data}")]
    [ApiController]
    public class RetrieveController : ControllerBase
    {
        private IShortServices shortServices;

        public RetrieveController(IShortServices shortServices)
        {
            this.shortServices = shortServices;
        }

        // GET: DataController
        [HttpGet]
        public ActionResult Index([FromRoute] string data)
        {
            var result = this.shortServices.GetOriginalUrl(data);
            return new RedirectResult(result);
        }

    }
}
