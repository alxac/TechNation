using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
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
        private readonly HttpClient _httpClient;

        public LogsController(ILogConverterService logConverterService)
        {
            _logConverterService = logConverterService;
            _httpClient = new HttpClient();
        }

        [HttpPost("converter")]
        public async Task<IActionResult> ConvertLog([FromBody] string logContent)
        {
            var convertedLog = _logConverterService.ConvertLog(logContent);
            return Ok(convertedLog);
        }

        [HttpPost("byArquivo")]
        public async Task<IActionResult> ConvertLogFromFile([FromForm] IFormFile file)
        {
            if (file == null)
                return Ok("Erro");
            var reader = new StreamReader(file.OpenReadStream());
            var logContent = await reader.ReadToEndAsync();
            var convertedLog = _logConverterService.ConvertLog(logContent);
            return Ok(convertedLog);
        }

        [HttpGet("byUrl")]
        public async Task<IActionResult> ConvertLogFromUrl(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            var convertedLog = _logConverterService.ConvertLog(response);
            return Ok(convertedLog);
        }

        [HttpGet("bySalvar")]
        public async Task<IActionResult> ConvertAndSaveLogFromUrl(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            var convertedLog = _logConverterService.ConvertLog(response);

            var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            var fileName = $"converted-log-{System.Guid.NewGuid()}.txt";
            var filePath = Path.Combine(logsDirectory, fileName);
            await System.IO.File.WriteAllTextAsync(filePath, convertedLog);

            return Ok(new
            {
                Message = "Log Convertido com sucesso.",
                FilePath = filePath
            });
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Logs", "Logs" };
        }
    }
}
