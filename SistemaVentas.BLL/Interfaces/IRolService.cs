using SistemaVenta.Entity.Models;

namespace SistemaVentas.BLL.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> Lista();
    }
}
