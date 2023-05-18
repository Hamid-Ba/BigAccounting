using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Repository
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        readonly DbSet<TEntity> dbSet;
        readonly TContext Context;

        public BaseRepository(TContext context)
        {
            Context = context;
            dbSet = Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAllEntities() => dbSet.AsNoTracking().ToList();

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync() => await dbSet.AsNoTracking().ToListAsync();

        public TEntity GetEntityByID(object id) => dbSet.Find(id);

        public async Task<TEntity> GetEntityByIDAsync(object id) => await dbSet.FindAsync(id);

        public async Task CreateEntity(TEntity entity) => await dbSet.AddAsync(entity);

        public void DeleteEntity(TEntity entity) => dbSet.Remove(entity);

        public void EditEntity(TEntity entity) => dbSet.Update(entity);

        public async Task CreateRangeEntity(IEnumerable<TEntity> entity) => await dbSet.AddRangeAsync(entity);

        public void EditRangeEntity(IEnumerable<TEntity> entity) => dbSet.UpdateRange(entity);

        public void DeleteRangeEntity(IEnumerable<TEntity> entity) => dbSet.RemoveRange(entity);

        public int GetCount() => dbSet.AsNoTracking().Count();

        public IEnumerable<TEntity> PaginationOfEntity(int CurrentPage, int PageSize)
        {
            var AllEntity = GetAllEntities();
            return AllEntity.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
