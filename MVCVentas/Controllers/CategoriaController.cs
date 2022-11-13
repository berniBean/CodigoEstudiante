using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVCVentas.Models.ViewModel;
using MVCVentas.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity.Models;

namespace MVCVentas.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(IMapper mapper, ICategoriaService categoriaService)
        {
            _mapper = mapper;
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMCategoria> vmCategoriaLista = 
               _mapper.Map<List<VMCategoria>>(await _categoriaService.Lista());

            return StatusCode(StatusCodes.Status200OK, new { data = vmCategoriaLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody]VMCategoria modelo)
        {
            GenericResponse<VMCategoria> genericResponse = new GenericResponse<VMCategoria>();

            try
            {
                Categoria categoriaCreada = await _categoriaService
                    .Crear(_mapper.Map<Categoria>(modelo));
                modelo = _mapper.Map<VMCategoria>(categoriaCreada);

                genericResponse.Estado = true;
                genericResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
                
            }

            return StatusCode(StatusCodes.Status200OK,genericResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMCategoria modelo)
        {
            GenericResponse<VMCategoria> genericResponse = new GenericResponse<VMCategoria>();

            try
            {
                Categoria categoriaEditada = await _categoriaService
                    .Editar(_mapper.Map<Categoria>(modelo));
                modelo = _mapper.Map<VMCategoria>(categoriaEditada);

                genericResponse.Estado = true;
                genericResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid idCategoria)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();

            try
            {
               
                genericResponse.Estado = await _categoriaService.Eliminar(idCategoria);
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }
    }
}
