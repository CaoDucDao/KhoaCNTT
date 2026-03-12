using KhoaCNTT.Domain.Common;
using KhoaCNTT.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaCNTT.Domain.Entities.FileEntities
{
    public class FileEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public int ViewCount { get; set; } = 0;
        public int DownloadCount { get; set; } = 0;
        public FilePermission Permission { get; set; }
        public FileType FileType { get; set; }

        // Môn học
        [Column(TypeName = "VARCHAR(50)")]
        public string? SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public Subject? Subject { get; set; } = null!;

        // Trỏ đến Resource hiện tại đang dùng
        public int CurrentResourceId { get; set; }
        public FileResource CurrentResource { get; set; } = null!;
        public int CreatedBy { get; set; } // FK Admin
        public Admin Admin { get; set; } = null!;
    }
}