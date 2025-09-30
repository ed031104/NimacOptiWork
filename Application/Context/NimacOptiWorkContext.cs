using System;
using System.Collections.Generic;
using Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Context;

public partial class NimacOptiWorkContext : DbContext
{
    public NimacOptiWorkContext()
    {
    }

    public NimacOptiWorkContext(DbContextOptions<NimacOptiWorkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Infraestructure.Entities.Task> Tasks { get; set; }

    public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }

    public virtual DbSet<TaskState> TaskStates { get; set; }

    public virtual DbSet<TaskStatusHistory> TaskStatusHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=.;Database=NimacOptiWork;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAD57D82B591");

            entity.Property(e => e.DateEntry).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Docsu).IsFixedLength();
            entity.Property(e => e.Slstyp).IsFixedLength();
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC07D6EBE614");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Infraestructure.Entities.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Task__7C6949D1DEFEA7B5");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.TaskAssignmentId).HasName("PK__TaskAssi__75E8D25F71CFB52F");

            entity.Property(e => e.AssignedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.TaskAssignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskAssig__Assig__66603565");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAssignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskAssig__TaskI__6754599E");
        });

        modelBuilder.Entity<TaskState>(entity =>
        {
            entity.HasKey(e => e.TaskStateId).HasName("PK__TaskStat__C024635226E3FFD5");
        });

        modelBuilder.Entity<TaskStatusHistory>(entity =>
        {
            entity.HasKey(e => e.TaskStatusHistoryId).HasName("PK__TaskStat__AAC202A6B5FA61C1");

            entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskStatusHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskStatu__TaskI__7B5B524B");

            entity.HasOne(d => d.TaskState).WithMany(p => p.TaskStatusHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskStatu__TaskS__7C4F7684");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07343CF870");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC071CF52A31");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Rol).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
