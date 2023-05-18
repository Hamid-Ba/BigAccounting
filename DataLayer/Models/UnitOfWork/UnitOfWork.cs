using DataLayer.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        public BigAccountingContext Context;

        public UnitOfWork(BigAccountingContext context) => Context = context;

        public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class
        {
            IBaseRepository<TEntity> repository = new BaseRepository<TEntity, BigAccountingContext>(Context);
            return repository;
        }

        public void Dispose() => Context.Dispose();

        public async Task Commit() => await Context.SaveChangesAsync();

    }
}
