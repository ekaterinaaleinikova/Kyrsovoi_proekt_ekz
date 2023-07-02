using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VacationDB2.Models;

namespace VacationDB2.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveReport> LeaveReports { get; set; }

    public virtual DbSet<VacationRequestSubmission> VacationRequestSubmissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=VacationDB2;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admins__3214EC276BDB5FFE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfAdmission).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC27C63513A6");

            entity.ToTable("Department");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC274BB239CB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfHire).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FatherName).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Employees_Department");
        });

        modelBuilder.Entity<LeaveReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaveRep__3214EC278BE2AEDA");

            entity.ToTable("LeaveReport");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LeaveEndDate).HasColumnType("date");
            entity.Property(e => e.LeaveStartDate).HasColumnType("date");
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Admin).WithMany(p => p.LeaveReports)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_LeaveReport_Admins");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveReports)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_LeaveReport_Employees");
        });

        modelBuilder.Entity<VacationRequestSubmission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vacation__3214EC277815C65D");

            entity.ToTable("VacationRequestSubmission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LeaveStartDate).HasColumnType("date");
            entity.Property(e => e.Reason).HasMaxLength(100);
            entity.Property(e => e.VacationEndDate).HasColumnType("date");
			entity.Property(e => e.WantDays).HasColumnName("WantDays");

			entity.HasOne(d => d.Employee).WithMany(p => p.VacationRequestSubmissions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_VacationRequestSubmission_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
