using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TechNation.Services
{
    public interface ILogConverterService
    {
        Task<string> ConverterLog(string inputLog, bool salvarArquivo);
        Task<string> ConverterArquivo(IFormFile file, bool salvarArquivo);
    }
}
