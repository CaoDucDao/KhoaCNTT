using KhoaCNTT.Domain.Common;
using KhoaCNTT.Domain.Enums;

namespace KhoaCNTT.Domain.Entities.FileEntities
{
    public class FileApproval : BaseEntity
    {
        public int FileRequestId { get; set; }
        public FileRequest FileRequest { get; set; } = null!;

        public int ApproverId { get; set; }
        public Admin Admin { get; set; } = null!;

        public ApprovalDecision Decision { get; set; }
        public string? Reason { get; set; }
    }
}