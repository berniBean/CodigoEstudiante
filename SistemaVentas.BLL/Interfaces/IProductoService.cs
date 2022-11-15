using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> Lista();
        Task<Producto> Crear(Producto entidad, Stream? Foto=null, string? NombreFoto=null);
        Task<Producto> Editar(Producto entidad, Stream? Foto = null);
        Task<bool> Eliminar(Guid idEntidad);
    }
}
