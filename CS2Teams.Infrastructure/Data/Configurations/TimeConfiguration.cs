using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CS2Teams.Domain.Entities;

namespace CS2Teams.Infrastructure.Data.Configurations
{
    public class TimeConfiguration : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable("Times");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Tag)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasIndex(t => t.Tag)
                .IsUnique();

            builder.Property(t => t.Pais)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.DataFundacao)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(t => t.LogoUrl)
                .HasMaxLength(500);

            builder.Property(t => t.Ranking)
                .IsRequired();

            // Relacionamento 1:N com Jogador
            builder.HasMany(t => t.Jogadores)
                .WithOne(j => j.Time)
                .HasForeignKey(j => j.TimeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}