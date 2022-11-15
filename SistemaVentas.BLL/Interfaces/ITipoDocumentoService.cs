using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ITipoDocumentoService
    {
        Task<List<TipoDocumentoVenta>> Lista();
    }
}
