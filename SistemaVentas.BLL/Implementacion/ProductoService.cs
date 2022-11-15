using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;

namespace SistemaVenta.BLL.Implementacion
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _GenericRepository;
        private readonly IFireBaseServices _fireBase;





        public ProductoService(IGenericRepository<Producto> genericRepository, IFireBaseServices fireBase = null)
        {
            _GenericRepository = genericRepository;
            _fireBase = fireBase;

        }

        public async Task<List<Producto>> Lista()
        {
            IQueryable<Producto> query = await _GenericRepository.Consultar();
            return  query.Include(c => c.CategoriaNav).ToList();
            
        }
        public async Task<Producto> Crear(Producto entidad,  Stream? Foto = null, string? NombreFoto = null)
        {
            Producto existente = await _GenericRepository.Obtener(p => p.CodigoDeBarra.Equals(entidad.CodigoDeBarra));
            if (existente != null)
                throw new TaskCanceledException("El código de barra ya existe");

            try
            {




                entidad.ProductoId = Guid.NewGuid();
                entidad.NombreImagen = NombreFoto;
                if (Foto != null)
                {
                    string fotoProducto = await _fireBase.SubirStorage(Foto, "carpeta_producto", NombreFoto);
                    entidad.UrlImagen = fotoProducto;
                }

                var nuevoProducto =  await _GenericRepository.Crear(entidad);
                if (string.IsNullOrEmpty(nuevoProducto.ProductoId.ToString()))
                
                    throw new TaskCanceledException("No se pudo crear el producto");
                
                IQueryable<Producto> query = await _GenericRepository
                    .Consultar(p => p.ProductoId.Equals(nuevoProducto.ProductoId));
                nuevoProducto = query.Include(c => c.CategoriaNav).First();

                return nuevoProducto;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Producto> Editar(Producto entidad, Stream? Foto = null)
        {
            var productoExiste = await _GenericRepository
                .Obtener(p => p.CodigoDeBarra.Equals(entidad.CodigoDeBarra)
                && p.ProductoId != entidad.ProductoId);

            if(productoExiste != null)
                throw new TaskCanceledException("El código de barra ya existe");
            try
            {
                IQueryable<Producto> queryProducto = await _GenericRepository
                    .Consultar(p => p.ProductoId.Equals(entidad.ProductoId));

                Producto editaProducto = queryProducto.First();
                
                editaProducto.CodigoDeBarra = entidad.CodigoDeBarra;
                editaProducto.Marca = entidad.Marca;
                editaProducto.Descripcion = entidad.Descripcion;
                editaProducto.CategoriaId = entidad.CategoriaId;
                editaProducto.Stock = entidad.Stock;
                editaProducto.Precio = entidad.Precio;
                editaProducto.EsActivo = entidad.EsActivo;

                if(Foto != null)
                {
                    var urlFoto = await _fireBase.SubirStorage(Foto, "carpeta_producto", entidad.NombreImagen);
                    editaProducto.UrlImagen = urlFoto;
                }

                bool respuesta = await _GenericRepository.Editar(editaProducto);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el producto");
                var editado = queryProducto.Include(c => c.CategoriaNav).First();

                return editado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> Eliminar(Guid idEntidad)
        {
            try
            {
                Producto productoEcontrado = await _GenericRepository
                    .Obtener(p=>p.ProductoId.Equals(idEntidad));

                if (productoEcontrado == null)
                    throw new TaskCanceledException("El producto no existe");
                
                string nombreFoto = productoEcontrado.NombreImagen;

                bool respuesta = await _GenericRepository.Eliminar(productoEcontrado);

                if (respuesta)
                    await _fireBase.EliminarStorage("carpeta_producto", nombreFoto);
                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
