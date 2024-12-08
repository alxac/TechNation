using TechNation.Data.Repositorio;

namespace TechNation.Services
{
    public class LogService
    {
        private ILogRepositorio _repositorio;

        public LogService(ILogRepositorio repositorio)
        {
            _repositorio = repositorio;            
        }

    }
}
