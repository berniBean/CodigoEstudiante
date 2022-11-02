using Microsoft.AspNetCore.Mvc;

namespace MVCVentas.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
