using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pubs.Web.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Authors";
            return View();
        }
    }
}