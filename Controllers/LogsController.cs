using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TechNation.Services;

namespace TechNation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly LogConverterService _logConverterService = new LogConverterService();

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertLog([FromBody] string logContent)
        {
            var convertedLog = _logConverterService.ConvertLog(logContent);
            return Ok(convertedLog);
        }

        [HttpPost("convertFromFile")]
        public async Task<IActionResult> ConvertLogFromFile([FromForm] IFormFile file)
        {
            if (file == null)
                return Ok("Erro");
            var reader = new StreamReader(file.OpenReadStream());
            var logContent = await reader.ReadToEndAsync();
            var convertedLog = _logConverterService.ConvertLog(logContent);
            return Ok(convertedLog);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Logs", "Logs" };
        }
    }
}
