using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EPG_Api.Models
{
    public partial class EPGContext : DbContext
    {
        public EPGContext()
        {
        }

        public EPGContext(DbContextOptions<EPGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Credit> Credits { get; set; }
        public virtual DbSet<Epg> Epgs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlConnectionString.Get(0));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Credit>(entity =>
            {
                entity.ToTable("credits");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AssetId).HasColumnName("assetId");

                entity.Property(e => e.DateInserted)
                    .HasColumnType("datetime")
                    .HasColumnName("dateInserted");

                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Credits)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_credits_EPG_TVA");
            });

            modelBuilder.Entity<Epg>(entity =>
            {
                entity.ToTable("EPGS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CdNibble1).HasColumnName("cd_nibble1");

                entity.Property(e => e.CdNibble2).HasColumnName("cd_nibble2");

                entity.Property(e => e.Channel)
                    .IsUnicode(false)
                    .HasColumnName("channel");

                entity.Property(e => e.DateProd)
                    .HasColumnType("date")
                    .HasColumnName("date_prod");

                entity.Property(e => e.Duration)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("duration");

                entity.Property(e => e.Eid).HasColumnName("eid");

                entity.Property(e => e.ExtendedAlb)
                    .IsUnicode(false)
                    .HasColumnName("extended_alb");

                entity.Property(e => e.ExtendedEng)
                    .IsUnicode(false)
                    .HasColumnName("extended_eng");

                entity.Property(e => e.Genre)
                    .HasMaxLength(250)
                    .HasColumnName("genre");

                entity.Property(e => e.Poster)
                    .IsUnicode(false)
                    .HasColumnName("poster");

                entity.Property(e => e.Prd).HasColumnName("prd");

                entity.Property(e => e.PrdCountryCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prd_country_code");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ShortAlb)
                    .IsUnicode(false)
                    .HasColumnName("short_alb");

                entity.Property(e => e.ShortEng)
                    .IsUnicode(false)
                    .HasColumnName("short_eng");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Trailer)
                    .IsUnicode(false)
                    .HasColumnName("trailer");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apikey)
                    .IsUnicode(false)
                    .HasColumnName("apikey");

                entity.Property(e => e.Company)
                    .IsUnicode(false)
                    .HasColumnName("company");

                entity.Property(e => e.VerifyCode)
                    .IsUnicode(false)
                    .HasColumnName("verifycode");

                entity.Property(e => e.Dateinserted)
                    .HasColumnType("datetime")
                    .HasColumnName("dateinserted");

                entity.Property(e => e.Email)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lname");

                entity.Property(e => e.Nrequests).HasColumnName("nrequests");
                entity.Property(e => e.AccountStatus).HasColumnName("acountstatus");

                entity.Property(e => e.Passwd)
                    .IsUnicode(false)
                    .HasColumnName("passwd");

                entity.Property(e => e.Subscription).HasColumnName("subscription");

                entity.Property(e => e.Usr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usr");
            });

            modelBuilder.Entity<Channels>(entity =>
            {
                entity.ToTable("channels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Channel)
                    .IsUnicode(false)
                    .HasColumnName("channel");

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasColumnName("status");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
