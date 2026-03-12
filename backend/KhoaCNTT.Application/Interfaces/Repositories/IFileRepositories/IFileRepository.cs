using KhoaCNTT.Domain.Entities;
using KhoaCNTT.Domain.Entities.FileEntities;

namespace KhoaCNTT.Application.Interfaces.Repositories
{
    public interface IFileRepository : IRepository<FileEntity>
    {
        Task<List<FileEntity>> SearchAsync(string keyword, List<string>? subjectCodes, int page, int pageSize);
        Task<Dictionary<string, int>> GetStatsByFileTypeAsync();
        Task<Dictionary<string, int>> GetStatsBySubjectAsync();
        Task<Dictionary<string, int>> GetStatsByTrafficAsync();
    }
}