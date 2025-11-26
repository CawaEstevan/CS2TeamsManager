using CS2Players.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS2Players.Infrastructure.Data.Configurations
{
    public class TimeConfiguration : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable("Times");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Regiao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.AnoFundacao)
                .IsRequired();

            builder.Property(t => t.LogoUrl)
                .HasMaxLength(500);

            builder.Property(t => t.DataCriacao)
                .IsRequired();


            builder.HasMany(t => t.Jogadores)
                .WithOne(j => j.Time)
                .HasForeignKey(j => j.TimeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}