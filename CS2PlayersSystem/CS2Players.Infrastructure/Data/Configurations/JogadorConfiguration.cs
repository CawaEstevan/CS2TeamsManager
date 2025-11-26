using CS2Players.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS2Players.Infrastructure.Data.Configurations
{
    public class JogadorConfiguration : IEntityTypeConfiguration<Jogador>
    {
        public void Configure(EntityTypeBuilder<Jogador> builder)
        {
            builder.ToTable("Jogadores");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Nickname)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(j => j.NomeCompleto)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(j => j.Idade)
                .IsRequired();

            builder.Property(j => j.Funcao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(j => j.Salario)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(j => j.Nacionalidade)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(j => j.DataContratacao)
                .IsRequired();


            builder.Property(j => j.TimeId)
                .IsRequired();


            builder.HasOne(j => j.Time)
                .WithMany(t => t.Jogadores)
                .HasForeignKey(j => j.TimeId)
                .OnDelete(DeleteBehavior.Cascade);

 
            builder.HasIndex(j => j.Nickname)
                .IsUnique();
        }
    }
}