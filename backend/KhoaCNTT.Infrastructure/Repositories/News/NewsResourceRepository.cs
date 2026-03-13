using KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;
using KhoaCNTT.Domain.Entities.NewsEntities;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KhoaCNTT.Infrastructure.Repositories.News;

public class NewsResourceRepository(AppDbContext context) : INewsResourceRepository
{
    public async Task<NewsResource?> GetByIdAsync(int id) =>
        await context.Set<NewsResource>().FindAsync(id);

    public async Task<NewsResource?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await context.Set<NewsResource>().FindAsync(new object[] { id }, ct);

    public async Task AddAsync(NewsResource entity, CancellationToken ct = default)
    {
        context.Set<NewsResource>().Add(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(NewsResource entity, CancellationToken ct = default)
    {
        context.Set<NewsResource>().Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(NewsResource entity, CancellationToken ct = default)
    {
        context.Set<NewsResource>().Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task<List<NewsResource>> GetAllAsync() =>
        await context.Set<NewsResource>().ToListAsync();

    public async Task<List<NewsResource>> GetAllAsync(Expression<Func<NewsResource, bool>> predicate) =>
        await context.Set<NewsResource>().Where(predicate).ToListAsync();
}