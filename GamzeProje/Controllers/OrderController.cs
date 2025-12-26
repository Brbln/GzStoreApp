using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
