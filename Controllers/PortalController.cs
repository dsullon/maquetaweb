using Microsoft.AspNetCore.Mvc;

namespace Veterinaria.Controllers
{
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
