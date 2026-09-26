using Microsoft.EntityFrameworkCore;
using NguyenVanDat2410900021_exam.Models;

namespace NguyenVanDat2410900021_exam.Data;

public class NvdDbContext : DbContext
{
    public NvdDbContext(DbContextOptions<NvdDbContext> options) : base(options)
    {
    }

    public DbSet<NvdStudent> NvdStudents => Set<NvdStudent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NvdStudent>(entity =>
        {
            entity.ToTable("NvdStudent");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.NvdStudentCode).IsUnique();

            entity.Property(e => e.NvdStudentCode).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.NvdName).HasMaxLength(100);
            entity.Property(e => e.NvdGender).HasMaxLength(10);
            entity.Property(e => e.NvdBirthDay).HasColumnType("date");
            entity.Property(e => e.NvdEmail).HasMaxLength(255);
            entity.Property(e => e.NvdPhone).HasMaxLength(15).IsUnicode(false);
            entity.Property(e => e.NvdClass).HasMaxLength(30).IsUnicode(false);
        });
    }
}
