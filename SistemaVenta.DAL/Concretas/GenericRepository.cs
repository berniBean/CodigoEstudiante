using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Concretas
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly VentasDBContext _ventasDBContext;

        public GenericRepository(VentasDBContext ventasDBContext)
        {
            _ventasDBContext = ventasDBContext;
        }

        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> Filtro)
        {
            try
            {
                TEntity entidad = await _ventasDBContext.Set<TEntity>().FirstOrDefaultAsync(Filtro);
                return entidad;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<TEntity> Crear(TEntity entity)
        {
            try
            {
                _ventasDBContext.Set<TEntity>().Add(entity);
                await _ventasDBContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> Editar(TEntity entity)
        {
            try
            {
                _ventasDBContext.Update(entity);
                await _ventasDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> Eliminar(TEntity entity)
        {
            try
            {
                _ventasDBContext.Remove(entity);
                await _ventasDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> Filtro = null)
        {
            IQueryable<TEntity> queryEntidad = Filtro == null ? _ventasDBContext.Set<TEntity>() : _ventasDBContext.Set<TEntity>().Where(Filtro);
            return queryEntidad;
        }


    }
}
