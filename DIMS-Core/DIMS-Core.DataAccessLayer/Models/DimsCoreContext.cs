﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DIMS_Core.DataAccessLayer.Models
{
    public partial class DimsCoreContext : DbContext
    {
        public DimsCoreContext()
        {
        }

        public DimsCoreContext(DbContextOptions<DimsCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskState> TaskStates { get; set; }
        public virtual DbSet<TaskTrack> TaskTracks { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<VTask> VTasks { get; set; }
        public virtual DbSet<VUserProfile> VUserProfiles { get; set; }
        public virtual DbSet<VUserProgress> VUserProgresses { get; set; }
        public virtual DbSet<VUserTask> VUserTasks { get; set; }
        public virtual DbSet<VUserTrack> VUserTracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Direction>(entity =>
                                           {
                                               entity.Property(e => e.Description).HasMaxLength(250);

                                               entity.Property(e => e.Name)
                                                     .IsRequired()
                                                     .HasMaxLength(50);
                                           });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TaskState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__TaskStat__C3BA3B3A7FDAC1F1");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TaskTrack>(entity =>
            {
                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.UserTask)
                    .WithMany(p => p.TaskTracks)
                    .HasForeignKey(d => d.UserTaskId);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserProfiles_UserId");

                entity.Property(e => e.Address).HasMaxLength(120);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__UserTasks__State__2EA5EC27");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__UserTasks__TaskI__2F9A1060");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserTasks__UserI__308E3499");
            });

            modelBuilder.Entity<VTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTasks");

                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TaskId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VUserProfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProfiles");

                entity.Property(e => e.Address).HasMaxLength(120);

                entity.Property(e => e.Direction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(101);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<VUserProgress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProgress");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(101);
            });

            modelBuilder.Entity<VUserTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaskDescription).IsRequired();

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VUserTrack>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTrack");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
