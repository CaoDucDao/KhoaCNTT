using System.ComponentModel.DataAnnotations;

namespace KhoaCNTT.Domain.Entities
{
    public class Subject
    {
        [Key]
        public string SubjectCode { get; set; } // Mã môn (vd: CSE481)
        public string SubjectName { get; set; } = string.Empty;
        public int Credits { get; set; } = 3; // Số tín chỉ

        // Quan hệ: Một môn học được dạy bởi nhiều giảng viên
        public ICollection<LecturerSubject> LecturerSubjects { get; set; } = new List<LecturerSubject>();
    }
}