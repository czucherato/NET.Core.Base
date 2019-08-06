using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NET.Core.Base.Data.Context;
using System.Collections.Generic;
using NET.Core.Base.Business.Models;
using Microsoft.EntityFrameworkCore;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        public Repository(NetCoreBaseContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        protected readonly NetCoreBaseContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
