
using KhoaCNTT.Application.Interfaces.Repositories;
using KhoaCNTT.Domain.Entities.FileEntities;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KhoaCNTT.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;
        public FileRepository(AppDbContext context) => _context = context;

        public async Task<FileEntity?> GetByIdAsync(int id) =>
            await _context.Set<FileEntity>().Include(f => f.CurrentResource).FirstOrDefaultAsync(f => f.Id == id);

        public async Task AddAsync(FileEntity entity) { _context.Set<FileEntity>().Add(entity); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(FileEntity entity) { _context.Set<FileEntity>().Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(FileEntity entity) { _context.Set<FileEntity>().Remove(entity); await _context.SaveChangesAsync(); }

        public Task<List<FileEntity>> GetAllAsync() => throw new NotImplementedException();
        public Task<List<FileEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<FileEntity, bool>> predicate) => throw new NotImplementedException();

        public async Task<List<FileEntity>> SearchAsync(string keyword, List<string>? subjectCodes, int page, int pageSize)
        {
            var query = _context.Set<FileEntity>().Include(f => f.CurrentResource).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(f => f.Title.Contains(keyword));

            if (subjectCodes != null && subjectCodes.Any())
            {
                query = query.Where(f => f.SubjectCode == null || subjectCodes.Contains(f.SubjectCode));
            }
                
            return await query
                .OrderByDescending(f => f.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetStatsByFileTypeAsync()
        {
            return await _context.Set<FileEntity>()
                .GroupBy(f => f.FileType)
                .Select(g => new { Key = g.Key.ToString(), Count = g.Count() })
                .ToDictionaryAsync(x => x.Key, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetStatsBySubjectAsync()
        {
            return await _context.Set<FileEntity>()
                .Where(f => f.SubjectCode != null)
                .GroupBy(f => f.SubjectCode)
                .Select(g => new { Key = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key!, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetStatsByTrafficAsync()
        {
            var totalViews = await _context.Set<FileEntity>().SumAsync(f => f.ViewCount);
            var totalDownloads = await _context.Set<FileEntity>().SumAsync(f => f.DownloadCount);

            return new Dictionary<string, int>
            {
                { "Views", totalViews },
                { "Downloads", totalDownloads }
            };
        }
    }
}