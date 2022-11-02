using Microsoft.AspNetCore.Mvc;

namespace MVCVentas.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
