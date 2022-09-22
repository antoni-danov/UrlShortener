﻿using Microsoft.AspNetCore.Mvc;
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
        readonly IShortServices shortServices;


        public DataController(IShortServices shortServices)
        {
            this.shortServices = shortServices;
        }

        [HttpGet("/{data}")]
        public async Task<IActionResult> GetUrl([FromRoute] string data)
        {
            var result = this.shortServices.GetOriginalUrl(data);

            return new RedirectResult(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFiltersAttribute))]
        public IActionResult Create([FromBody] UrlData data)
        {
            var ifExist = IsCreated(data.OriginalUrl);

            if (ifExist == null)
            {
                var url = new UrlData
                {
                    OriginalUrl = data.OriginalUrl,
                    ShortUrl = data.ShortUrl,
                    CreatedOn = data.CreatedOn,
                };

                var currentUrl = shortServices.CreateUrlRecord(url);
                return StatusCode(201, currentUrl);
            }

            return StatusCode(200, ifExist);
        }

        public ExistingUrlRecord IsCreated(string originalUrl)
        {

            return this.shortServices.isCreated(originalUrl);


        }

    }
}
