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

        public virtual DbSet<EpgTva> EpgTvas { get; set; }

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

            modelBuilder.Entity<EpgTva>(entity =>
            {
                entity.ToTable("EPG_TVA");

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

                entity.Property(e => e.EedLangAlb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eed_lang_alb");

                entity.Property(e => e.EedLangEng)
                    .IsUnicode(false)
                    .HasColumnName("eed_lang_eng");

                entity.Property(e => e.EedTextAlb)
                    .IsUnicode(false)
                    .HasColumnName("eed_text_alb");

                entity.Property(e => e.EedTextEng)
                    .IsUnicode(false)
                    .HasColumnName("eed_text_eng");

                entity.Property(e => e.Eid).HasColumnName("eid");

                entity.Property(e => e.Genre)
                    .HasMaxLength(250)
                    .HasColumnName("genre");

                entity.Property(e => e.PrdCountryCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prd_country_code");

                entity.Property(e => e.PrdValue).HasColumnName("prd_value");

                entity.Property(e => e.SedLangAlb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sed_lang_alb");

                entity.Property(e => e.SedLangEng)
                    .IsUnicode(false)
                    .HasColumnName("sed_lang_eng");

                entity.Property(e => e.SedNameAlb)
                    .IsUnicode(false)
                    .HasColumnName("sed_name_alb");

                entity.Property(e => e.SedNameEng)
                    .IsUnicode(false)
                    .HasColumnName("sed_name_eng");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
