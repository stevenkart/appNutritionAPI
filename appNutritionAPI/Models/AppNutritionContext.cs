using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace appNutritionAPI.Models
{
    public partial class AppNutritionContext : DbContext
    {
        public AppNutritionContext()
        {
        }

        public AppNutritionContext(DbContextOptions<AppNutritionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExerciseRoutine> ExerciseRoutines { get; set; } = null!;
        public virtual DbSet<NutritionalPlan> NutritionalPlans { get; set; } = null!;
        public virtual DbSet<Reminder> Reminders { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("SERVER=localhost;DATABASE=AppNutrition;INTEGRATED SECURITY=TRUE; User Id=;Password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseRoutine>(entity =>
            {
                entity.HasKey(e => e.IdRoutine)
                    .HasName("PK__exercise__99D02BFF617E5E91");

                entity.ToTable("exerciseRoutine");

                entity.Property(e => e.IdRoutine).HasColumnName("idRoutine");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ExerciseXample)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("exerciseXample");

                entity.Property(e => e.IdState).HasColumnName("idState");

                entity.Property(e => e.RoutineName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("routineName");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.ExerciseRoutines)
                    .HasForeignKey(d => d.IdState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKexerciseRo248564");
            });

            modelBuilder.Entity<NutritionalPlan>(entity =>
            {
                entity.HasKey(e => e.IdPlan)
                    .HasName("PK__nutritio__BECFB99659447117");

                entity.ToTable("nutritionalPlan");

                entity.Property(e => e.IdPlan).HasColumnName("idPlan");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdState).HasColumnName("idState");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PlanXample)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("planXample");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.NutritionalPlans)
                    .HasForeignKey(d => d.IdState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKnutritiona967283");
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.HasKey(e => e.IdReminder)
                    .HasName("PK__Reminder__567582FD9D203641");

                entity.Property(e => e.IdReminder).HasColumnName("idReminder");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Detail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("detail");

                entity.Property(e => e.Done).HasColumnName("done");

                entity.Property(e => e.Hour).HasColumnName("hour");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReminders365558");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.IdState)
                    .HasName("PK__State__98CB372377FDF99D");

                entity.ToTable("State");

                entity.Property(e => e.IdState).HasColumnName("idState");

                entity.Property(e => e.Detail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("detail");

                entity.Property(e => e.StateName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("stateName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__Users__3717C982C5EE733D");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FatPercent)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("fatPercent");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.Genre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("genre");

                entity.Property(e => e.Hight)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("hight");

                entity.Property(e => e.IdPlan).HasColumnName("idPlan");

                entity.Property(e => e.IdRoutine).HasColumnName("idRoutine");

                entity.Property(e => e.IdState).HasColumnName("idState");

                entity.Property(e => e.Password)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RecoveryCode).HasColumnName("recoveryCode");

                entity.Property(e => e.Weight)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("weight");

                entity.HasOne(d => d.IdPlanNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdPlan)
                    .HasConstraintName("FKUsers760639");

                entity.HasOne(d => d.IdRoutineNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRoutine)
                    .HasConstraintName("FKUsers134593");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUsers285260");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
