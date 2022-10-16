using Microsoft.AspNetCore.Mvc;
using UrlShortener.Controllers;
using UrlShortener.Models;
using UrlShortener.Services;
using Xunit;

namespace UrlShortener.Test
{
    public class DataControllerTest
    {
        UrlController controller;
        IShortService services;

        public DataControllerTest()
        {
            services = new ShortService();
            controller = new UrlController(services);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Arange
            var value = "asld2";
            // Act
            var okResult = controller.GetUrl(value);
            // Assert
            var result = Assert.IsType<RedirectResult>(okResult);
        }

    }
}