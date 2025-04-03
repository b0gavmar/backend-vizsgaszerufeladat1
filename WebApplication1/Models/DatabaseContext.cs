using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=Database\\database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("author");

            entity.Property(e => e.BirthDate).HasColumnType("DATE");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("event");

            entity.Property(e => e.AuthorId)
                .HasColumnType("INT")
                .HasColumnName("AuthorID");
            entity.Property(e => e.EventDateTime).HasColumnType("DATETIME");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("ID");
            entity.Property(e => e.RegistrationDeadline).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("participant");

            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("registration");

            entity.Property(e => e.EventId)
                .HasColumnType("INT")
                .HasColumnName("EventID");
            entity.Property(e => e.ParticipantId)
                .HasColumnType("INT")
                .HasColumnName("ParticipantID");
            entity.Property(e => e.RegistrationDateTime).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("story");

            entity.Property(e => e.AuthorId)
                .HasColumnType("INT")
                .HasColumnName("AuthorID");
            entity.Property(e => e.CategoryId)
                .HasColumnType("INT")
                .HasColumnName("CategoryID");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("ID");
            entity.Property(e => e.PublishEnd).HasColumnType("DATETIME");
            entity.Property(e => e.PublishStart).HasColumnType("DATETIME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
