using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CS2Teams.Domain.Entities;
using CS2Teams.Domain.Enums;

namespace CS2Teams.Infrastructure.Data.Configurations
{
    public class JogadorConfiguration : IEntityTypeConfiguration<Jogador>
    {
        public void Configure(EntityTypeBuilder<Jogador> builder)
        {
            builder.ToTable("Jogadores");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                .ValueGeneratedOnAdd();

            builder.Property(j => j.Nickname)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(j => j.Nickname)
                .IsUnique();

            builder.Property(j => j.NomeReal)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(j => j.Nacionalidade)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(j => j.DataNascimento)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(j => j.Role)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(j => j.Rating)
                .IsRequired()
                .HasColumnType("decimal(3,2)");

            // Chave estrangeira explícita
            builder.Property(j => j.TimeId)
                .IsRequired();

            // Relacionamento N:1 com Time
            builder.HasOne(j => j.Time)
                .WithMany(t => t.Jogadores)
                .HasForeignKey(j => j.TimeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}