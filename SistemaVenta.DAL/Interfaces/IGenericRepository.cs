using System.Linq.Expressions;

namespace SistemaVenta.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Obtener(Expression<Func<TEntity,bool>>Filtro);
        Task<TEntity> Crear(TEntity entity);
        Task<bool> Editar(TEntity entity);
        Task<bool> Eliminar(TEntity entity);
        Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> Filtro = null);
    }
}
