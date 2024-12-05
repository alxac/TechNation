using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TechNation.CrossCutting;
using TechNation.Dominio;

namespace TechNation.Services
{
    public class LogConverterService : ILogConverterService
    {
        private readonly HttpClient _httpClient;

        public LogConverterService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> ConverterLog(string inputLog, bool salvarArquivo)
        {
            var retorno = "Operação não realizada.";
            //Verifica se vem de uma URL
            var origem = inputLog.ContemLink();
            if (origem)
            {
                var response = await _httpClient.GetStringAsync(inputLog.Trim());
                retorno = await ConverterLog(response, salvarArquivo);
            }
            else
            {
                var lines = inputLog.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //Configurando Versões, datas, campos e outras informações.
                var outputLog = new List<string>
            {
                "#Version: 1.0",
                $"#Date: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}",
                "#Fields: provider http-method status-code uri-path time-taken response-size cache-status"
            };

                //Percorrendo as linhas
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        var methodUriParts = parts[3].Split(' ');
                        var logEntry = new LogEntity
                        {
                            Provider = "MINHA CDN",
                            HttpMethod = methodUriParts[0],
                            StatusCode = int.Parse(parts[1]),
                            UriPath = methodUriParts[1],
                            TimeTaken = Math.Round(Convert.ToDouble(parts[4]), 0),
                            ResponseSize = int.Parse(parts[0]),
                            //Fazendo a verificação pela funções Extension de Cache de Status
                            CacheStatus = parts[2].ToCacheStatus(),
                        };
                        //TODO: Gravar no banco de dados
                        outputLog.Add($"{logEntry.Provider} {logEntry.HttpMethod} {logEntry.StatusCode} {logEntry.UriPath} {logEntry.TimeTaken} {logEntry.ResponseSize} {logEntry.CacheStatus}");
                    }
                }

                retorno = string.Join("\n", outputLog);

                if (salvarArquivo)
                {
                    var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                    if (!Directory.Exists(logsDirectory))
                        Directory.CreateDirectory(logsDirectory);

                    var fileName = $"convertido-log-{Guid.NewGuid()}.txt";
                    var filePath = Path.Combine(logsDirectory, fileName);
                    await File.WriteAllTextAsync(filePath, retorno);

                    retorno = $"Log Convertido com sucesso.\nArquivo: {filePath}";
                }
            }
            return retorno;

        }

        public async Task<string> ConverterArquivo(IFormFile file, bool salvarArquivo)
        {
            if (file == null)
                return "Erro";
            var reader = new StreamReader(file.OpenReadStream());
            var logContent = await reader.ReadToEndAsync();
            var convertedLog = ConverterLog(logContent, salvarArquivo);
            return await convertedLog;
        }
    }
}