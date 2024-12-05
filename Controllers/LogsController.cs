using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TechNation.Services;

namespace TechNation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogConverterService _logConverterService;

        public LogsController(ILogConverterService logConverterService)
        {
            _logConverterService = logConverterService;
        }

        // Teste de URL - POST https://localhost:44377/api/logs/byTexto
        // Teste de URL - POST https://localhost:44377/api/logs/byTexto?salvarArquivo=true
        /// <summary>
        /// Realiza a conversão com base em um texto ou uma URL.
        /// </summary>
        /// <param name="logContent"></param>
        /// <returns></returns>
        [HttpPost("byTexto")]
        public async Task<IActionResult> ConvertLog([FromBody] string logContent, [FromQuery] bool salvarArquivo = false)
        {
            var convertedLog = await _logConverterService.ConverterLog(logContent, salvarArquivo);
            return Ok(convertedLog);
        }

        // Teste de URL - POST https://localhost:44377/api/logs/byArquivo
        // Teste de URL - POST https://localhost:44377/api/logs/byArquivo?salvarArquivo=true
        /// <summary>
        /// Realiza a conversão com base em um Arquivo enviado.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("byArquivo")]
        public async Task<IActionResult> ConverterArquivo([FromForm] IFormFile file, [FromQuery] bool salvarArquivo = false)
        {
            var convertedLog = await _logConverterService.ConverterArquivo(file, salvarArquivo);
            return Ok(convertedLog);
        }


        // Teste de URL - GET https://localhost:44377/api/logs/bySalvar
        // Teste de URL - GET https://localhost:44377/api/logs/bySalvar?salvarArquivo=true
        /// <summary>
        /// Realiza a conversão e salva em arquivo na pasta LOGS
        /// </summary>
        /// <param name="logContent"></param>
        /// <returns></returns>
        [HttpGet("bySalvar")]
        public async Task<IActionResult> ConvertSalvar([FromBody]  string logContent, [FromQuery] bool salvarArquivo = false)
        {
            var convertedLog = await _logConverterService.ConverterLog(logContent, salvarArquivo);
            return Ok(convertedLog);
        }


        // Teste de URL - GET https://localhost:44377/api/logs
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Logs", "Logs" };
        }
    }
}
