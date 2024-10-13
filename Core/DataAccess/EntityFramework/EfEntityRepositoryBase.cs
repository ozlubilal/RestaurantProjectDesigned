using Core.DataAccess.EntityFramework;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : BaseEntity<Guid>, new()
    where TContext : DbContext
{
    private readonly TContext _context;

    // Constructor üzerinden context nesnesi enjekte ediliyor
    public EfEntityRepositoryBase(TContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        entity.CreatedDate = DateTime.Now;

        var addedEntity = _context.Entry(entity);
        addedEntity.State = EntityState.Added;
        _context.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        var deletedEntity = _context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        return _context.Set<TEntity>().FirstOrDefault(filter);
    }

    public List<TEntity> GetList(
       Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
       Expression<Func<TEntity, bool>> predicate = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (include != null)
        {
            query = include(query);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query.ToList();
    }


    public void Update(TEntity entity)
    {
        entity.UpdatedDate = DateTime.Now;

        var updatedEntity = _context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        _context.SaveChanges();
    }
}
