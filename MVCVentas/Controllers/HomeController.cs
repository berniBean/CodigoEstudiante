using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCVentas.Models;
using MVCVentas.Models.ViewModel;
using MVCVentas.Utilidades.Response;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;

namespace MVCVentas.Controllers;
[Authorize]
public class HomeController : Controller
{

    private readonly IUsuarioService _userService;
    private readonly IMapper _mapper;

    public HomeController( IUsuarioService userService = null, IMapper mapper = null)
    {
       
        _userService = userService;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Perfil()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerUsuario()
    {
        GenericResponse<VMUsuraio> response = new GenericResponse<VMUsuraio>();

        try
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            string idUser = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();

            VMUsuraio user = _mapper.Map<VMUsuraio>(await _userService.ObtenerPorId(new Guid(idUser)));

            response.Estado = true;
            response.Objeto = user;

        }
        catch (Exception ex)
        {
            response.Estado = false;
            response.Mensaje = ex.Message;
            
        }

        return StatusCode(StatusCodes.Status200OK,response);
    }


    [HttpPost]
    public async Task<IActionResult> GuardarPerfil([FromBody] VMUsuraio modelo)
    {
        GenericResponse<VMUsuraio> response = new GenericResponse<VMUsuraio>();

        try
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            string idUser = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();

            Usuario entidad = _mapper.Map<Usuario>(modelo);

            entidad.UsuarioId = new Guid(idUser);
            bool resultado = await _userService.GuardarPerfil(entidad);



            response.Estado = resultado;


        }
        catch (Exception ex)
        {
            response.Estado = false;
            response.Mensaje = ex.Message;

        }

        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost]
    public async Task<IActionResult> CambiarClave([FromBody] VMCambiarClave modelo)
    {
        GenericResponse<bool> response = new GenericResponse<bool>();

        try
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            string idUser = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();

            bool resultado = await _userService.CambiarClave(
                 new Guid(idUser),
                 modelo.ClaveActual,
                 modelo.ClaveNueva
                );



            response.Estado = resultado;


        }
        catch (Exception ex)
        {
            response.Estado = false;
            response.Mensaje = ex.Message;

        }

        return StatusCode(StatusCodes.Status200OK, response);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public async Task<IActionResult> Salir()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Loggin","Acceso");
    }
}
