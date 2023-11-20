using FDMC.Data;
using FDMC.Models;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace FDMC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DatabaseContext _ctx;
        private readonly IServer _server;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, DatabaseContext ctx, IServer server)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _ctx = ctx;
            _server = server;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCats()
        {
            var cats = await _ctx.Cats.ToListAsync();
            if (cats == null) { return NotFound(); }
            return View(cats);
        }

        public IActionResult GetForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Cat cat)
        {

            if(!ModelState.IsValid)
            {
                TempData["msg"] = "Fill all form fields correctly!!!";
                return new RedirectResult("/Home/GetForm", false);
            }
            TempData["msg"] = null;

            string folderPath = "Assets/uploadImg/";
            string imgPath = folderPath + ((cat.Image != null)? cat.Image.FileName: "catFace.png") ;
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imgPath);

            if (cat.Image != null)
            {              
                await cat.Image.CopyToAsync(new FileStream(fullPath, FileMode.Create));
            }

            try
            {
                _ctx.Add(new CatDto(cat.Id,cat.Name,cat.Age ,cat.Breed, imgPath));
                _ctx.SaveChanges();
            }catch(Exception)
            {
                throw;
            }
            return new RedirectResult("/Home/GetCats", true);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}