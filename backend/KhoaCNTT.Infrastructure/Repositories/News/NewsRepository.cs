using KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NewsEntities = KhoaCNTT.Domain.Entities.NewsEntities;

namespace KhoaCNTT.Infrastructure.Repositories.News;

public class NewsRepository(AppDbContext context) : INewsRepository
{
    public async Task<NewsEntities.News?> GetByIdAsync(int id) =>
        await context.Set<NewsEntities.News>()
            .Include(n => n.CurrentResource)
            .FirstOrDefaultAsync(n => n.Id == id);

    public async Task<NewsEntities.News?> GetByIdWithResourceAsync(int newsId, CancellationToken ct = default) =>
        await context.Set<NewsEntities.News>()
            .Include(n => n.CurrentResource)
            .FirstOrDefaultAsync(n => n.Id == newsId, ct);

    public async Task<IEnumerable<NewsEntities.News>> GetAllWithResourceAsync(CancellationToken ct = default) =>
        await context.Set<NewsEntities.News>()
            .Include(n => n.CurrentResource)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(ct);

    public async Task IncrementViewCountAsync(int newsId, CancellationToken ct = default) =>
        await context.Set<NewsEntities.News>()
            .Where(n => n.Id == newsId)
            .ExecuteUpdateAsync(s => s.SetProperty(n => n.ViewCount, n => n.ViewCount + 1), ct);

    public async Task AddAsync(NewsEntities.News entity, CancellationToken ct = default)
    {
        context.Set<NewsEntities.News>().Add(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(NewsEntities.News entity, CancellationToken ct = default)
    {
        context.Set<NewsEntities.News>().Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(NewsEntities.News entity, CancellationToken ct = default)
    {
        context.Set<NewsEntities.News>().Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task<List<NewsEntities.News>> GetAllAsync() =>
        await context.Set<NewsEntities.News>().ToListAsync();

    public async Task<List<NewsEntities.News>> GetAllAsync(Expression<Func<NewsEntities.News, bool>> predicate) =>
        await context.Set<NewsEntities.News>().Where(predicate).ToListAsync();
}