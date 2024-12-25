using Microsoft.AspNetCore.Mvc;

namespace AracKiralama.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}