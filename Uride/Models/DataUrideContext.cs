using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Uride.Models
{
    public partial class DataUrideContext : DbContext
    {
        public DataUrideContext()
        {
        }

        public DataUrideContext(DbContextOptions<DataUrideContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Stranka> Stranka { get; set; }
        public virtual DbSet<Transakcija> Transakcija { get; set; }
        public virtual DbSet<Vozilo> Vozilo { get; set; }
        public virtual DbSet<Voznik> Voznik { get; set; }
        public virtual DbSet<Voznja> Voznja { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DataUride;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stranka>(entity =>
            {
                entity.HasIndex(e => e.UpImeId)
                    .HasName("UQ__Stranka__4C07975FF6393DF3")
                    .IsUnique();

                entity.Property(e => e.StrankaId).HasColumnName("StrankaID");

                entity.Property(e => e.Ime)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MobStev)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Naslov)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Priimek)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpImeId).HasMaxLength(128);
            });

            modelBuilder.Entity<Transakcija>(entity =>
            {
                entity.HasKey(e => e.StRacuna);

                entity.Property(e => e.StRacuna).HasColumnName("stRacuna");

                entity.Property(e => e.Idvoznje).HasColumnName("IDvoznje");

                entity.HasOne(d => d.IdvoznjeNavigation)
                    .WithMany(p => p.Transakcija)
                    .HasForeignKey(d => d.Idvoznje)
                    .HasConstraintName("FK__Transakci__IDvoz__32E0915F");
            });

            modelBuilder.Entity<Vozilo>(entity =>
            {
                entity.HasKey(e => e.AvtoId);

                entity.HasIndex(e => e.Model)
                    .HasName("UQ__Vozilo__FB104C133D693197")
                    .IsUnique();

                entity.Property(e => e.AvtoId).HasColumnName("AvtoID");

                entity.Property(e => e.CenaKm).HasColumnName("cenaKM");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StSedezev).HasColumnName("stSedezev");

                entity.Property(e => e.Znamka)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Voznik>(entity =>
            {
                entity.HasKey(e => e.StVozniske);

                entity.HasIndex(e => e.UpImeId)
                    .HasName("UQ__Voznik__4C07975F5BE2F6FC")
                    .IsUnique();

                entity.Property(e => e.StVozniske).HasColumnName("stVozniske");

                entity.Property(e => e.AvtoId).HasColumnName("AvtoID");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MobStev)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Naslov)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Priimek)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpImeId).HasMaxLength(128);

                entity.HasOne(d => d.Avto)
                    .WithMany(p => p.Voznik)
                    .HasForeignKey(d => d.AvtoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Voznik__AvtoID__29572725");
            });

            modelBuilder.Entity<Voznja>(entity =>
            {
                entity.Property(e => e.VoznjaId).HasColumnName("VoznjaID");

                entity.Property(e => e.UporabnikId).HasColumnName("uporabnikID");

                entity.Property(e => e.VoznikId).HasColumnName("voznikID");

                entity.HasOne(d => d.Uporabnik)
                    .WithMany(p => p.Voznja)
                    .HasForeignKey(d => d.UporabnikId)
                    .HasConstraintName("FK__Voznja__uporabni__2F10007B");

                entity.HasOne(d => d.Voznik)
                    .WithMany(p => p.Voznja)
                    .HasForeignKey(d => d.VoznikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Voznja__voznikID__300424B4");
            });
        }
    }
}
