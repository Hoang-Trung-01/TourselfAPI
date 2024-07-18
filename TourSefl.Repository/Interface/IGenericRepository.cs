using System.Linq.Expressions;

namespace TourSefl.Repository.Interface
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
           bool? sortOrderAsc = true,
           string includeProperties = "",
           int? pageIndex = null,
           int? pageSize = null);

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
