using KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NewsEntities = KhoaCNTT.Domain.Entities.NewsEntities;

namespace KhoaCNTT.Infrastructure.Repositories.News;

public class NewsResourceRepository(AppDbContext context) : INewsResourceRepository
{
    public async Task<NewsEntities.NewsResource?> GetByIdAsync(int id) =>
        await context.Set<NewsEntities.NewsResource>().FindAsync(id);

    public async Task AddAsync(NewsEntities.NewsResource entity)
    {
        context.Set<NewsEntities.NewsResource>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(NewsEntities.NewsResource entity)
    {
        context.Set<NewsEntities.NewsResource>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(NewsEntities.NewsResource entity)
    {
        context.Set<NewsEntities.NewsResource>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<List<NewsEntities.NewsResource>> GetAllAsync() =>
        await context.Set<NewsEntities.NewsResource>().ToListAsync();

    public async Task<List<NewsEntities.NewsResource>> GetAllAsync(
        Expression<Func<NewsEntities.NewsResource, bool>> predicate) =>
        await context.Set<NewsEntities.NewsResource>().Where(predicate).ToListAsync();
}