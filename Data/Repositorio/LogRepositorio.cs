using Microsoft.EntityFrameworkCore;
using TechNation.Data.Context;
using TechNation.Dominio;

namespace TechNation.Data.Repositorio
{
    public class LogRepositorio : BaseRepositorio<LogEntity>, ILogRepositorio
    {
        private readonly DbSet<LogEntity> _dataset;

        public LogRepositorio(LogAppContext context) : base(context)
        {
            _dataset = context.Set<LogEntity>();
        }

        //Implementar aqui novas funções que não existe na classe Base
    }
}
