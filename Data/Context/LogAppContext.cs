using Microsoft.EntityFrameworkCore;
using TechNation.Data.Mapping;
using TechNation.Dominio;

namespace TechNation.Data.Context
{
    public class LogAppContext : DbContext
    {
        public DbSet<LogEntity> LogEntity { get; set; }

        public LogAppContext(DbContextOptions<LogAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configurando a criação da tabela separadamente
            modelBuilder.Entity<LogEntity>(new LogsMap().Configure);
        }
    }
}
