using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeMovies.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {            
            return View();
        }
    }
}
