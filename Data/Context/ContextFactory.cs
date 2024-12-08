using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechNation.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<LogAppContext>
    {
        public LogAppContext CreateDbContext(string[] args)
        {
            //Usado para criar as migrações
            var con = "Data Source=ALEX\\SQLEXPRESS;Initial Catalog=TESTE;Integrated Security=True;Encrypt=False";
            var optionb = new DbContextOptionsBuilder<LogAppContext>();
            optionb.UseSqlServer(con);
            return new LogAppContext(optionb.Options);
        }
    }
}
