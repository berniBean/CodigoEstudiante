using Microsoft.AspNetCore.Mvc;

namespace MVCVentas.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
