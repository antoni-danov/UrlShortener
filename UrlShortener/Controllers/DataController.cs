﻿using Microsoft.AspNetCore.Mvc;
using System;
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

        // Post: DataController/Create
        [HttpPost]
        public IActionResult Create([FromBody] UrlData data)
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
                    this.shortServices.CreateUrlRecord(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return StatusCode(201, data);
        }

        public Boolean isCreated(string originalUrl)
        {

            return this.shortServices.isCreated(originalUrl);

        }

    }
}
