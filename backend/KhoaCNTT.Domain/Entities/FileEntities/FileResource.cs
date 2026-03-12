using KhoaCNTT.Domain.Common;

namespace KhoaCNTT.Domain.Entities.FileEntities
{
    public class FileResource : BaseEntity
    {
        public string FileName { get; set; } = string.Empty; // Tên file gốc
        public string FilePath { get; set; } = string.Empty; // Đường dẫn trên ổ đĩa
        public long Size { get; set; }

        // Foreign key 
        public int CreatedBy { get; set; }
        public Admin Admin { get; set; } = null!;
    }
}