using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HRM_Project.Models
{
    public partial class HRM_DB_ProjectContext : DbContext
    {
        public HRM_DB_ProjectContext()
        {
        }

        public HRM_DB_ProjectContext(DbContextOptions<HRM_DB_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeesList> EmployeesLists { get; set; }
        public virtual DbSet<HrList> HrLists { get; set; }
        public virtual DbSet<LocationList> LocationLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HHR16QE\\SQLEXPRESS;Database=HRM_DB_Project;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EmployeesList>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Employee__78113481404C7D27");

                entity.ToTable("Employees_List");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.EmployeeAppraisalDate)
                    .HasColumnType("date")
                    .HasColumnName("Employee_AppraisalDate");

                entity.Property(e => e.EmployeeDoj)
                    .HasColumnType("date")
                    .HasColumnName("Employee_DOJ");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Name");

                entity.Property(e => e.EmployeePoints).HasColumnName("Employee_Points");

                entity.Property(e => e.EmployeeRole)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Role");

                entity.Property(e => e.EmployeeSalary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Employee_Salary");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Location_ID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.EmployeesLists)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Employees__Locat__4BAC3F29");
            });

            modelBuilder.Entity<HrList>(entity =>
            {
                entity.HasKey(e => e.HrId)
                    .HasName("PK__HR_List__420AEF75D02DACB1");

                entity.ToTable("HR_List");

                entity.Property(e => e.HrId).HasColumnName("Hr_ID");

                entity.Property(e => e.HrEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Hr_Email");

                entity.Property(e => e.HrName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Hr_Name");

                entity.Property(e => e.HrPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Hr_Password");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Location_ID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.HrLists)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__HR_List__Locatio__48CFD27E");
            });

            modelBuilder.Entity<LocationList>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Location__D2BA00C210ADBF58");

                entity.ToTable("Location_List");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Location_ID");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Location_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
