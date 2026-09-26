using Microsoft.EntityFrameworkCore;

namespace NvdLesson10.Models;

// DbContext theo hướng Database First; chuỗi kết nối được đăng ký trong Program.cs.
public partial class NvdLesson10EfDbContext : DbContext
{
    public NvdLesson10EfDbContext()
    {
    }

    public NvdLesson10EfDbContext(DbContextOptions<NvdLesson10EfDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NvdMember> NvdMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NvdMember>(entity =>
        {
            entity.HasKey(member => member.Id);
            entity.ToTable("NvdMember");

            entity.HasIndex(member => member.NvdEmail).IsUnique();
            entity.HasIndex(member => member.NvdUserName).IsUnique();

            entity.Property(member => member.NvdUserName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(member => member.NvdPassword)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(member => member.NvdFullName)
                .HasMaxLength(100);
            entity.Property(member => member.NvdEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(member => member.NvdPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(member => member.NvdStatus)
                .HasDefaultValue(true);
            entity.Property(member => member.NvdCreatedAt)
                .HasColumnType("datetime2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
