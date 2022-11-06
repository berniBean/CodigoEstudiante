using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVCVentas.Models.ViewModel;
using MVCVentas.Utilidades.Response;
using Newtonsoft.Json;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;

namespace MVCVentas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRolService _rolService;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IMapper mapper, IRolService rolService, IUsuarioService usuarioService)
        {
            _mapper = mapper;
            _rolService = rolService;
            _usuarioService = usuarioService;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaRoles()
        {
            List<VMRol> vmListaRoles = _mapper.Map<List<VMRol>>(await _rolService.Lista());

            return StatusCode(StatusCodes.Status200OK, vmListaRoles);
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMUsuraio> vMUsuraioLista = 
                _mapper.Map<List<VMUsuraio>>(await _usuarioService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vMUsuraioLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMUsuraio> genericResponse = new GenericResponse<VMUsuraio>();

            try
            {
                VMUsuraio vmUsuraio = JsonConvert.DeserializeObject<VMUsuraio>(modelo);

                string nombreFoto = "";
                Stream fotoStream = null;
                if (foto != null)
                {
                    string nombreCodigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto = string.Concat(nombreCodigo, extension);
                    fotoStream = foto.OpenReadStream();
                }

                string urlPlantillaCorreo = 
                    $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/EnviarClave?correo=[correo]&clave=[clave]";
                Usuario usuarioCreado = await _usuarioService.Crear(_mapper.Map<Usuario>(vmUsuraio), fotoStream, nombreFoto, urlPlantillaCorreo);

                vmUsuraio = _mapper.Map<VMUsuraio>(usuarioCreado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmUsuraio;

            }
            catch (Exception ex )
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
                
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMUsuraio> genericResponse = new GenericResponse<VMUsuraio>();

            try
            {
                VMUsuraio vmUsuraio = JsonConvert.DeserializeObject<VMUsuraio>(modelo);

                string nombreFoto = "";
                Stream fotoStream = null;
                if (foto != null)
                {
                    string nombreCodigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto = string.Concat(nombreCodigo, extension);
                    fotoStream = foto.OpenReadStream();
                }


                Usuario usuarioEditado = await _usuarioService.Editar
                    (_mapper.Map<Usuario>(vmUsuraio), fotoStream, nombreFoto);

                vmUsuraio = _mapper.Map<VMUsuraio>(usuarioEditado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmUsuraio;

            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Editar(Guid IdUsuario)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();

            try
            {
                genericResponse.Estado =  await _usuarioService.Eliminar(IdUsuario);
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
                throw;
            }
            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }
    }
}
