
using System.Collections.Generic;
using HanmakTechnologies.BulletinBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HanmakTechnologies.BulletinBoard
{
  public class HomeController : Controller
  {
    public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
          var bulletins = new List<Bulletin>();
            return View(bulletins);
        }
  }
}