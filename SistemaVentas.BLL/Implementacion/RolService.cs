using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;
using SistemaVentas.BLL.Interfaces;

namespace SistemaVentas.BLL.Implementacion
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _repository;

        public RolService(IGenericRepository<Rol> repository)
        {
            _repository = repository;
        }

        public async Task<List<Rol>> Lista()
        {
            IQueryable<Rol> query = await _repository.Consultar();

            return query.ToList();
        }
    }
}
