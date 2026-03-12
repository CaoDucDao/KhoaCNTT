using KhoaCNTT.Domain.Common;
using KhoaCNTT.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaCNTT.Domain.Entities.FileEntities
{
    public class FileRequest : BaseEntity
    {
        public RequestType RequestType { get; set; }
        public bool IsProcessed { get; set; } = false;

        // Thông tin Metadata muốn tạo/sửa (Lưu tạm ở đây để khi duyệt thì bắn sang FileEntity)
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(50)")]
        public string? SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public Subject? Subject { get; set; }

        public FilePermission Permission { get; set; }
        public FileType FileType { get; set; }

        // Nếu là yêu cầu Thay thế, thì File nào đang bị thay thế?
        public int? TargetFileId { get; set; }
        public FileEntity? TargetFile { get; set; }

        // Resource mới (được upload lên)
        public int NewResourceId { get; set; }
        public FileResource NewResource { get; set; } = null!;
        // AdminID từ new resource là người tạo yêu cầu này

        // Resource cũ (nếu là replace)
        public int? OldResourceId { get; set; }
        public FileResource? OldResource { get; set; }
    }
}