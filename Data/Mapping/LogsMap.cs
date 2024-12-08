using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechNation.Dominio;

namespace TechNation.Data.Mapping
{
    public class LogsMap : IEntityTypeConfiguration<LogEntity>
    {
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            //Criando a tabela de forma separada
            builder.ToTable("Logs");

            builder.HasKey(e => e.ID);
            builder.HasIndex(e => e.ID)
                .IsUnique();
        }
    }
}
