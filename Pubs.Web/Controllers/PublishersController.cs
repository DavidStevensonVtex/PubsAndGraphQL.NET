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