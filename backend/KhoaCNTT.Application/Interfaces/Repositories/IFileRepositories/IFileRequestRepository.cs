using KhoaCNTT.Domain.Entities;
using KhoaCNTT.Domain.Entities.FileEntities;

namespace KhoaCNTT.Application.Interfaces.Repositories
{
    public interface IFileRequestRepository : IRepository<FileRequest>
    {
        // Hàm lấy danh sách chờ duyệt với chi tiết thông tin liên quan
        Task<List<FileRequest>> GetPendingRequestsWithDetailsAsync();
    }
}