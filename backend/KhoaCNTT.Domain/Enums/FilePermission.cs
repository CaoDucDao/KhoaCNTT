
namespace KhoaCNTT.Domain.Enums
{
    public enum FilePermission
    {
        Hidden,             // Chỉ Admin thấy
        PublicRead,         // Khách xem được, không tải được
        PublicDownload,     // Khách tải được
        StudentRead,        // Sinh viên xem được, không tải được
        StudentDownload     // Sinh viên tải được
    }
}
