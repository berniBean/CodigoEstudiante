using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCVentas.Models.ViewModel;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;

using System.Security.Claims;

namespace MVCVentas.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuarioService _userService;

        public AccesoController(IUsuarioService userService)
        {
            _userService = userService;
        }

        public IActionResult Loggin()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult RestablecerClave()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RestablecerClave(VMUsuarioLogin modelo)
        {
            try
            {
                string urlPlantilla = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/RestablecerClave?clave=[clave]";

                bool resultado = await _userService.RestablecerClave(modelo.Correo, urlPlantilla);

                if (resultado)
                {
                    ViewData["Mensaje"] = "Contraseña restablecida revise en su correo";
                    ViewData["MensajeError"] = null;


                }
                else
                {
                    ViewData["MensajeError"] = "Tenemos problemas. Intente de nuevo más tarde";
                    ViewData["Mensaje"] = null;
                }
            }
            catch (Exception ex)
            {
                ViewData["MensajeError"] = ex.Message;
                ViewData["Mensaje"] = null;

                throw;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Loggin(VMUsuarioLogin modelo)
        {
            Usuario encontrado = await _userService.ObtenerPorCredenciales(modelo.Correo, modelo.Clave);


            if(encontrado == null)
            {
               
                ViewData["Mensaje"] = "No se encontraron coicidencias";
                return View();
            }

            ViewData["Mensaje"] = null;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, encontrado.Nombre),
                new Claim(ClaimTypes.NameIdentifier, encontrado.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, encontrado.RolId.ToString()),
                new Claim("UrlFoto", encontrado.UrlFoto),
                

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = modelo.MantenerSesion,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            return RedirectToAction("Index","Home");
        }
    }



}
