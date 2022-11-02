using Microsoft.AspNetCore.Mvc;

namespace MVCVentas.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
