using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVCVentas.Models.ViewModel;
using MVCVentas.Utilidades.Response;
using Newtonsoft.Json;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity.Models;

namespace MVCVentas.Controllers
{
    public class NegocioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _service;

        public NegocioController(IMapper mapper, INegocioService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            GenericResponse<VMNegocio> gResponse = new GenericResponse<VMNegocio>();

            try
            {
                VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _service.Obtener());

                gResponse.Estado = true;
                gResponse.Objeto = vmNegocio;
            }
            catch (Exception ex)
            {
                gResponse.Estado = true;
                gResponse.Mensaje = ex.Message;
                
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromForm]IFormFile logo, [FromForm]string modelo)
        {
            GenericResponse<VMNegocio> gResponse = new GenericResponse<VMNegocio>();

            try
            {
                VMNegocio vmNegocio = JsonConvert.DeserializeObject<VMNegocio>(modelo);

                string nombreLogo = "";
                Stream logoStream = null;

                if(logo != null)
                {
                    string nombreCodigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(logo.FileName);
                    nombreLogo = string.Concat(nombreCodigo,extension);
                    logoStream = logo.OpenReadStream();
                }


                Negocio negocioEditado = await _service.GuardarCambios
                    (_mapper.Map<Negocio>(vmNegocio), logoStream, nombreLogo);

                vmNegocio = _mapper.Map<VMNegocio>(negocioEditado);

                gResponse.Estado = true;
                gResponse.Objeto = vmNegocio;
            }
            catch (Exception ex)
            {
                gResponse.Estado = true;
                gResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
