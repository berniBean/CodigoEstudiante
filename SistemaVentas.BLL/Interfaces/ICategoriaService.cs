using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> Lista();
        Task<Categoria> Crear(Categoria entidad);
        Task<Categoria> Editar(Categoria entidad);
        Task<bool>Eliminar(Guid idEntidad);

    }
}
