using KhoaCNTT.Domain.Enums;

namespace KhoaCNTT.Application.DTOs.File
{
    public class FileRequestDto
    {
        public int Id { get; set; }
        public RequestType Type { get; set; }
        public string Title { get; set; }

        // Thông tin file mới upload lên
        public string NewFileName { get; set; }
        public long NewFileSize { get; set; }

        // Thông tin người yêu cầu
        public string RequesterName { get; set; }
        public DateTime CreatedAt { get; set; }

        // Nếu là replace thì hiện tên file cũ để so sánh
        public string? OldFileName { get; set; }
        public long? OldFileSize { get; set; }
    }
}