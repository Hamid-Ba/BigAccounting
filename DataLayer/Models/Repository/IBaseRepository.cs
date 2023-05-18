using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Repository
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        IEnumerable<TEntity> GetAllEntities();

        Task<TEntity> GetEntityByIDAsync(object id);
        TEntity GetEntityByID(object id);

        Task CreateEntity(TEntity entity);
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntity entity);

        Task CreateRangeEntity(IEnumerable<TEntity> entity);
        void EditRangeEntity(IEnumerable<TEntity> entity);
        void DeleteRangeEntity(IEnumerable<TEntity> entity);

        int GetCount();
        IEnumerable<TEntity> PaginationOfEntity(int CurrentPage, int PageSize);
    }
}
