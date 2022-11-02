using Microsoft.AspNetCore.Mvc;

namespace MVCVentas.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
