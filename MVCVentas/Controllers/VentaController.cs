using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCVentas.Models.ViewModel;
using MVCVentas.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity.Models;
using System.Security.Claims;

namespace MVCVentas.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        private readonly ITipoDocumentoService _DocService;
        
        private readonly IVentaService _VentaService;
        private readonly IMapper _mapper;

        public VentaController(ITipoDocumentoService docService, IVentaService ventaService, IMapper mapper)
        {
            _DocService = docService;
            _VentaService = ventaService;
            _mapper = mapper;
        }

        public IActionResult NuevaVenta()
        {
            return View();
        }

        public IActionResult HistorialVenta()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoVenta()
        {
            List<VMTipoDocumentoDeVenta> vmTipoDocVenta = _mapper
                .Map<List<VMTipoDocumentoDeVenta>>(await _DocService.Lista());

            return StatusCode(StatusCodes.Status200OK, vmTipoDocVenta);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<VMProducto> vmListaProductos = _mapper
                .Map<List<VMProducto>>(await _VentaService.ObtenerProductos(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaProductos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody] VMVenta modelo)
        {
            GenericResponse<VMVenta> gResponse = new GenericResponse<VMVenta>();


            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUser = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.UsuarioId = idUser;
                modelo.VentaId = Guid.NewGuid().ToString();

                var venta = await _VentaService.Registrar(_mapper.Map<Venta>(modelo));
                modelo = _mapper.Map<VMVenta>(venta);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;

            }
            catch (Exception ex)
            {
                gResponse.Estado = true;
                gResponse.Mensaje = ex.Message;
                throw;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
            List<VMVenta> historialVenta = _mapper
                .Map<List<VMVenta>>(await _VentaService.Historial(numeroVenta,fechaInicio,fechaFin));

            return StatusCode(StatusCodes.Status200OK, historialVenta);
        }
    }
}
