using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Utilites;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Hello.Atd.Controllers
{
    public class HomeController : Controller
    {
        private IMyCulture _culture;

        public HomeController(IMyCulture culture)
        {
            _culture = culture;
        }

        // GET: /<controller>/
        [Route("home/{c}")]
        public IActionResult Index(string c)
        {
            _culture.SetCulture(c);

            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return Content("Hello from ABOUT page!");
        }
    }
}
