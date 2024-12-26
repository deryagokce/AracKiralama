using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AracKiralama.Controllers
{
    [Authorize]

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}