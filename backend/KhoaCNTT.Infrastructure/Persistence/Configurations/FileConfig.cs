// KhoaCNTT.Infrastructure/Persistence/Configurations/FileConfiguration.cs
using KhoaCNTT.Domain.Entities.FileEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KhoaCNTT.Infrastructure.Persistence.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<FileEntity>,
      IEntityTypeConfiguration<FileResource>,
      IEntityTypeConfiguration<FileRequest>,
      IEntityTypeConfiguration<FileApproval>
    {
        public void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            builder.ToTable("FileEntity");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("FileID");
            builder.Property(x => x.Title).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.FileType).HasConversion<string>();
            builder.Property(x => x.Permission).HasConversion<string>();
            builder.HasOne(x => x.CurrentResource).WithMany().HasForeignKey(x => x.CurrentResourceId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Admin).WithMany().HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
        }

        public void Configure(EntityTypeBuilder<FileResource> builder)
        {
            builder.ToTable("FileResource");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("FileResourceID");
            builder.Property(x => x.FileName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.FilePath).HasColumnType("char(255)").IsRequired();
            builder.HasOne(x => x.Admin).WithMany().HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
        }

        public void Configure(EntityTypeBuilder<FileRequest> builder)
        {
            builder.ToTable("FileRequest");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("FileRequestID");
            builder.Property(x => x.Title).HasColumnType("nvarchar(100)");
            builder.Property(x => x.RequestType).HasConversion<string>();
            builder.Property(x => x.FileType).HasConversion<string>();
            builder.Property(x => x.Permission).HasConversion<string>();

            builder.HasOne(x => x.TargetFile).WithMany().HasForeignKey(x => x.TargetFileId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.NewResource).WithMany().HasForeignKey(x => x.NewResourceId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.OldResource).WithMany().HasForeignKey(x => x.OldResourceId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Subject).WithMany().HasForeignKey(x => x.SubjectCode).OnDelete(DeleteBehavior.SetNull);
        }

        public void Configure(EntityTypeBuilder<FileApproval> builder)
        {
            builder.ToTable("FileApproval");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("FileApprovalID");
            builder.Property(x => x.Decision).HasConversion<string>();
            builder.Property(x => x.Reason).HasColumnType("nvarchar(255)");

            builder.HasOne(x => x.Admin).WithMany().HasForeignKey(x => x.ApproverId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.FileRequest).WithMany().HasForeignKey(x => x.FileRequestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}