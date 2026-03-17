using KhoaCNTT.Domain.Entities.NewsEntities;

namespace KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;

public interface INewsRepository : IRepository<News>
{
    Task<News?> GetByIdWithResourceAsync(int newsId);
    Task<IEnumerable<News>> GetAllWithResourceAsync();
    Task IncrementViewCountAsync(int newsId);
}