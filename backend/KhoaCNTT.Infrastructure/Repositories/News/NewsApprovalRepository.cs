using KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NewsEntities = KhoaCNTT.Domain.Entities.NewsEntities;

namespace KhoaCNTT.Infrastructure.Repositories.News;

public class NewsApprovalRepository(AppDbContext context) : INewsApprovalRepository
{
    public async Task<NewsEntities.NewsApproval?> GetByIdAsync(int id) =>
        await context.Set<NewsEntities.NewsApproval>().FindAsync(id);

    public async Task<NewsEntities.NewsApproval?> GetByRequestIdAsync(int requestId) =>
        await context.Set<NewsEntities.NewsApproval>()
            .FirstOrDefaultAsync(a => a.NewsRequestId == requestId);

    public async Task AddAsync(NewsEntities.NewsApproval entity)
    {
        context.Set<NewsEntities.NewsApproval>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(NewsEntities.NewsApproval entity)
    {
        context.Set<NewsEntities.NewsApproval>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(NewsEntities.NewsApproval entity)
    {
        context.Set<NewsEntities.NewsApproval>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<List<NewsEntities.NewsApproval>> GetAllAsync() =>
        await context.Set<NewsEntities.NewsApproval>().ToListAsync();

    public async Task<List<NewsEntities.NewsApproval>> GetAllAsync(
        Expression<Func<NewsEntities.NewsApproval, bool>> predicate) =>
        await context.Set<NewsEntities.NewsApproval>().Where(predicate).ToListAsync();
}