using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Infrastructure.Configuration
{
    public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
    {
        public void Configure(EntityTypeBuilder<UserGame> builder)
        {
            builder.ToTable("TB_USUARIO_JOGO");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").HasColumnName("ISN_USUARIO_JOGO").UseIdentityColumn();
            builder.Property(p => p.DateCreation).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DateUpdate).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
            builder.Property(p => p.GameId).HasColumnType("INT").HasColumnName("ISN_JOGO").IsRequired();
            builder.Property(p => p.UserId).HasColumnType("INT").HasColumnName("ISN_USUARIO").IsRequired();

            builder.HasOne(p => p.Game)
                   .WithMany(p => p.UserGames)
                   .HasPrincipalKey(p => p.Id).IsRequired(true);
        }
    }
}
