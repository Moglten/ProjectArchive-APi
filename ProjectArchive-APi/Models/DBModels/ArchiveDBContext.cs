using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectArchive_APi.Models.DBModels
{
    public partial class ArchiveDBContext : DbContext
    {
        public ArchiveDBContext()
        {
        }

        public ArchiveDBContext(DbContextOptions<ArchiveDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<AssetsDepartmentRelation> AssetsDepartmentRelation { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectDepartmentRelation> ProjectDepartmentRelation { get; set; }
        public object Configuration { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ArchiveDB;Trusted_Connection=True;");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetsDepartmentRelation>(entity =>
            {
                entity.HasKey(e => new { e.AssetId, e.DepartmentId });

                entity.HasIndex(e => e.DepartmentId);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.AssetsDepartmentRelation)
                    .HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<ProjectDepartmentRelation>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.DepartmentId });

                entity.HasIndex(e => e.DepartmentId);

            
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectDepartmentRelation)
                    .HasForeignKey(d => d.ProjectId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
