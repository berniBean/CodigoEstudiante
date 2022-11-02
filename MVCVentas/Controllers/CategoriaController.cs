using Microsoft.AspNetCore.Mvc;

namespace MVCVentas.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
