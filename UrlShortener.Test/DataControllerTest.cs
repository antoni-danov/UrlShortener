using Microsoft.AspNetCore.Mvc;
using UrlShortener.Controllers;
using UrlShortener.Models;
using UrlShortener.Services;
using Xunit;

namespace UrlShortener.Test
{
    public class DataControllerTest
    {
        DataController controller;
        IShortServices services;

        public DataControllerTest()
        {
            services = new ShortServices();
            controller = new DataController(services);
        }

        [Fact]
        public void AddUrl()
        {
            //arrange
            var correctUrlData = new UrlData()
            {
                OriginalUrl = "https://code-maze.com/aspnetcore-webapi-best-practices/",
                ShortUrl = "https://localhost:44347/1IsPCG",
                CreatedOn = "12 September 2022"
            };
            //act
            var createdResponse = this.controller.CreateAsync(correctUrlData);
            //assert
            Assert.NotNull(createdResponse);

            //arrange
            var incorrectUrlData = new UrlData()
            {
                ShortUrl = "https://localhost:44347/1IsPCG",
                CreatedOn = "12 September 2022"
            };
            //act
            controller.ModelState.AddModelError("OriginalUrl", "THe Original Url is required.");
            var createdFalseResponse = controller.CreateAsync(incorrectUrlData);

            //assert
            Assert.IsType<BadRequestObjectResult>(createdFalseResponse);
            
        }
    }
}