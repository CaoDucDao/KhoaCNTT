using KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;
using KhoaCNTT.Domain.Entities.NewsEntities;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KhoaCNTT.Infrastructure.Repositories.News;

public class NewsRequestRepository(AppDbContext context) : INewsRequestRepository
{
    public async Task<NewsRequest?> GetByIdAsync(int id) =>
        await context.Set<NewsRequest>()
            .Include(r => r.NewResource)
            .Include(r => r.OldResource)
            .Include(r => r.TargetNews)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<NewsRequest?> GetByIdWithDetailsAsync(int id, CancellationToken ct = default) =>
        await context.Set<NewsRequest>()
            .Include(r => r.NewResource)
            .Include(r => r.OldResource)
            .Include(r => r.TargetNews)
            .FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<IEnumerable<NewsRequest>> GetPendingAsync(CancellationToken ct = default) =>
        await context.Set<NewsRequest>()
            .AsNoTracking()
            .Include(r => r.NewResource)
                .ThenInclude(nr => nr.Admin)
            .Include(r => r.OldResource)
            .Include(r => r.TargetNews)
            .Where(r => !r.IsProcessed)
            .OrderBy(r => r.CreatedAt)
            .ToListAsync(ct);

    public async Task AddAsync(NewsRequest entity, CancellationToken ct = default)
    {
        context.Set<NewsRequest>().Add(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(NewsRequest entity, CancellationToken ct = default)
    {
        context.Set<NewsRequest>().Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(NewsRequest entity, CancellationToken ct = default)
    {
        context.Set<NewsRequest>().Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task<List<NewsRequest>> GetAllAsync() =>
        await context.Set<NewsRequest>().ToListAsync();

    public async Task<List<NewsRequest>> GetAllAsync(Expression<Func<NewsRequest, bool>> predicate) =>
        await context.Set<NewsRequest>().Where(predicate).ToListAsync();
}