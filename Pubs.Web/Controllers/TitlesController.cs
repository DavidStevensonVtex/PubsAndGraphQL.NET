using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pubs.Web.Controllers
{
    public class TitlesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Titles";
            return View();
        }
    }
}