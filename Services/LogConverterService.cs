using System;
using System.Collections.Generic;

namespace TechNation.Services
{
    public class LogConverterService
    {
        public string ConvertLog(string inputLog)
        {
            var lines = inputLog.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var outputLog = new List<string>
            {
                "#Version: 1.0",
                $"#Date: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}",
                "#Fields: provider http-method status-code uri-path time-taken response-size cache-status"
            };

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 5)
                {
                    string cacheStatus;
                    switch (parts[2])
                    {
                        case "HIT":
                            cacheStatus = "HIT";
                            break;
                        case "MISS":
                            cacheStatus = "MISS";
                            break;
                        case "INVALIDATE":
                            cacheStatus = "REFRESH_HIT";
                            break;
                        default:
                            cacheStatus = parts[2];
                            break;
                    }

                    var methodUriParts = parts[3].Split(' ');
                    outputLog.Add($"\"MINHA CDN\" {methodUriParts[0]} {parts[1]} {methodUriParts[1]} {Math.Round(Convert.ToDouble(parts[4]), 0)} {parts[0]} {cacheStatus}");
                }
            }

            return string.Join("\n", outputLog);
        }
    }
}