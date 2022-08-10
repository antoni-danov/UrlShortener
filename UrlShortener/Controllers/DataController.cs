using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // GET: DataController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DataController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                this.shortServices.CreateUrlRecord(data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return StatusCode(201);
        }


        //// GET: DataController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// POST: DataController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DataController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: DataController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DataController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: DataController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
