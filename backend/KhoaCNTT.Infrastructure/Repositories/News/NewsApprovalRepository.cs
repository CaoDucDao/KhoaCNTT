using KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;
using KhoaCNTT.Domain.Entities.NewsEntities;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KhoaCNTT.Infrastructure.Repositories.News;

public class NewsApprovalRepository(AppDbContext context) : INewsApprovalRepository
{
    public async Task<NewsApproval?> GetByIdAsync(int id) =>
        await context.Set<NewsApproval>().FindAsync(id);

    public async Task<NewsApproval?> GetByRequestIdAsync(int requestId, CancellationToken ct = default) =>
        await context.Set<NewsApproval>()
            .FirstOrDefaultAsync(a => a.NewsRequestId == requestId, ct);

    public async Task AddAsync(NewsApproval entity, CancellationToken ct = default)
    {
        context.Set<NewsApproval>().Add(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(NewsApproval entity, CancellationToken ct = default)
    {
        context.Set<NewsApproval>().Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(NewsApproval entity, CancellationToken ct = default)
    {
        context.Set<NewsApproval>().Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task<List<NewsApproval>> GetAllAsync() =>
        await context.Set<NewsApproval>().ToListAsync();

    public async Task<List<NewsApproval>> GetAllAsync(Expression<Func<NewsApproval, bool>> predicate) =>
        await context.Set<NewsApproval>().Where(predicate).ToListAsync();
}