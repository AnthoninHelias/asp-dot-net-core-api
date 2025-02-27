using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public partial class ProjetGestionContext : DbContext
{
    public ProjetGestionContext()
    {
    }

    public ProjetGestionContext(DbContextOptions<ProjetGestionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banlist> Banlists { get; set; }

    public virtual DbSet<Carte> Cartes { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=Projet_gestion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("banlist", "Projet_gestion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Interdite).HasColumnName("interdite");
            entity.Property(e => e.Limitée).HasColumnName("limitée");
        });

        modelBuilder.Entity<Carte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Cartes", "Projet_gestion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.Rareté).HasMaxLength(100);
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Utilisateur", "Projet_gestion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(256)
                .HasColumnName("Mot de passe");
            entity.Property(e => e.Nom).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
