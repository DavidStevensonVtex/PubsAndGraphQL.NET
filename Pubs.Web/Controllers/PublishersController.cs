using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pubs.Web.Controllers
{
    public class PublishersController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Publishers";
            return View();
        }
    }
}