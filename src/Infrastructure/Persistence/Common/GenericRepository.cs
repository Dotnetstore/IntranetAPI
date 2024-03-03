using Application.Common.Interfaces;
using Domain.Common;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Common;

public sealed class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
{
    private readonly ApplicationDataContext _context;

    public GenericRepository(ApplicationDataContext context)
    {
        _context = context;
    }

    public IQueryable<T> Entities => _context.Set<T>();

    async Task<List<T>> IGenericRepository<T>.GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context
            .Set<T>()
            .ToListAsync(cancellationToken);
    }

    void IGenericRepository<T>.Create(T entity)
    {
        _context.Add(entity);
    }

    void IGenericRepository<T>.Update(T entity)
    {
        _context.Update(entity);
    }

    void IGenericRepository<T>.Delete(T entity)
    {
        _context.Remove(entity);
    }
}