using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Infrastructure.Configuration
{
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("TB_JOGO");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).HasColumnType("INT").HasColumnName("ISN_JOGO").UseIdentityColumn();
            builder.Property(g => g.Title).HasColumnType("VARCHAR(500)").HasColumnName("DSC_TITULO").IsRequired();
            builder.Property(g => g.Description).HasColumnType("VARCHAR(2000)").HasColumnName("DSC_DESCRICAO").IsRequired();
            builder.Property(p => p.DateCreation).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DateUpdate).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
            builder.Property(P => P.Price).HasColumnType("DECIMAL(18,2)").HasColumnName("VLR_PRECO");
            builder.Property(P => P.Discount).HasColumnType("DECIMAL(18,2)").HasColumnName("VLR_DESCONTO");
            builder.Property(p => p.PlataformId).HasColumnType("INT").HasColumnName("ISN_PLATAFORMA").IsRequired();
            builder.Property(p => p.GenderId).HasColumnType("INT").HasColumnName("ISN_GENERO").IsRequired();

            builder.HasOne(p => p.Plataform)
               .WithMany(p => p.Games)
               .HasPrincipalKey(p => p.Id).IsRequired(true);

            builder.HasOne(p => p.Gender)
               .WithMany(p => p.Games)
               .HasPrincipalKey(p => p.Id).IsRequired(true);
        }
    }
}
