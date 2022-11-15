using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVCVentas.Models.ViewModel;
using MVCVentas.Utilidades.Response;
using Newtonsoft.Json;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity.Models;

namespace MVCVentas.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductoService _productoService;


        public ProductoController(IMapper mapper, IProductoService productoService)
        {
            _mapper = mapper;
            _productoService = productoService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMProducto> vMProducto = _mapper
                .Map<List<VMProducto>>(await _productoService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vMProducto });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm]IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMProducto> gResponse = new GenericResponse<VMProducto>();
            try
            {
                var vmPructoResponse = JsonConvert.DeserializeObject<VMProducto>(modelo);
                string nombreImagen = "";
                Stream imagenStream = null;

                if(foto!= null)
                {
                    string nombreCodigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreImagen = string.Concat(nombreCodigo, extension);
                    imagenStream = foto.OpenReadStream();
                }

                Producto productoCreado = await _productoService
                    .Crear(_mapper.Map<Producto>
                    (vmPructoResponse), imagenStream, nombreImagen);

                vmPructoResponse = _mapper.Map<VMProducto>(productoCreado);

                gResponse.Estado = true;
                gResponse.Objeto = vmPructoResponse;
                
            }
            catch (Exception ex)
            {
                gResponse.Estado=false;
                gResponse.Mensaje = ex.Message;
                
            }

            return StatusCode(StatusCodes.Status200OK,gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMProducto> gResponse = new GenericResponse<VMProducto>();
            try
            {
                var vmPructoResponse = JsonConvert.DeserializeObject<VMProducto>(modelo);
                Stream imagenStream = null;

                if (foto != null)
                {
                    imagenStream = foto.OpenReadStream();
                }

                Producto productoEditado = await _productoService
                    .Editar(_mapper.Map<Producto>
                    (vmPructoResponse), imagenStream);

                vmPructoResponse = _mapper.Map<VMProducto>(productoEditado);

                gResponse.Estado = true;
                gResponse.Objeto = vmPructoResponse;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }


        [HttpDelete]
        public async Task<IActionResult>Delete(string IdProducto)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();
            try
            {
                var id = new Guid(IdProducto);
                genericResponse.Estado = await _productoService.Eliminar(id);
            }
            catch (Exception ex)
            {
                genericResponse.Estado=false;
                genericResponse.Mensaje = ex.Message;
                throw;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }
    }
}
