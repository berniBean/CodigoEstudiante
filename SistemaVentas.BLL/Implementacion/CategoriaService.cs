using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Implementacion
{
    public class CategoriaService : ICategoriaService
    {
        public readonly IGenericRepository<Categoria> _GenericRepository;

        public CategoriaService(IGenericRepository<Categoria> genericRepository)
        {
            _GenericRepository = genericRepository;
        }

        public async Task<List<Categoria>> Lista()
        {
            IQueryable<Categoria> query = await _GenericRepository.Consultar();
            return await query.ToListAsync();
        }
        public async Task<Categoria> Crear(Categoria entidad)
        {
            try
            {
                Categoria categoriaCreada = await _GenericRepository.Crear(entidad);
                if (string.IsNullOrEmpty(categoriaCreada.CategoriaId.ToString()))
                    throw new TaskCanceledException("No se pudo crear la categoria");

                return categoriaCreada;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<Categoria> Editar(Categoria entidad)
        {
            try
            {
                Categoria categoriaEncontrada = await _GenericRepository
                    .Obtener(c => c.CategoriaId.Equals(entidad.CategoriaId));
                categoriaEncontrada.Descripcion = entidad.Descripcion;
                categoriaEncontrada.EsActivo = entidad.EsActivo;
                
                bool respuesta = await _GenericRepository.Editar(categoriaEncontrada);

                if(!respuesta)
                    throw new TaskCanceledException("No se pudo modificar la categoria");

                return categoriaEncontrada;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<bool> Eliminar(Guid categoriaId)
        {
            try
            {
                
                Categoria categoriaEncontrada = await _GenericRepository
                    .Obtener(c => c.CategoriaId.Equals(categoriaId));

                if (categoriaEncontrada==null)
                    throw new TaskCanceledException("La categoria no existe");

                bool respuesta = await _GenericRepository.Eliminar(categoriaEncontrada);
                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
