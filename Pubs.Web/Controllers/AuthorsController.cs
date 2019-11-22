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