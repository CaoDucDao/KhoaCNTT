using KhoaCNTT.Domain.Entities.NewsEntities;

namespace KhoaCNTT.Application.Interfaces.Repositories.INewsRepositories;

public interface INewsApprovalRepository : IRepository<NewsApproval>
{
    Task<NewsApproval?> GetByRequestIdAsync(int requestId);
}