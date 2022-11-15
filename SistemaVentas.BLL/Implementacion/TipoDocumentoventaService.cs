
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Implementacion
{
    public class TipoDocumentoventaService : ITipoDocumentoService
    {
        private readonly IGenericRepository<TipoDocumentoVenta> _repository;

        public TipoDocumentoventaService(IGenericRepository<TipoDocumentoVenta> repository)
        {
            _repository = repository;
        }

        public async Task<List<TipoDocumentoVenta>> Lista()
        {
            IQueryable<TipoDocumentoVenta> query = await _repository.Consultar();
            return query.ToList();
        }
    }
}
