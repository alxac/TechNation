using System;

namespace TechNation.Dominio
{
    public class LogEntity
    {
        public LogEntity()
        {
            ID = Guid.NewGuid();
            DataCadastro = DateTime.UtcNow;
        }

        public LogEntity(
            Guid? id = null,
            string provider = null,
            string httpMethod = null,
            int statusCode = 0,
            string uriPath = null,
            double timeTaken = 0,
            int responseSize = 0,
            string cacheStatus = null,
            DateTime? dataCadastro = null)
        { 
            ID = id ?? Guid.NewGuid();
            Provider = provider;
            HttpMethod = httpMethod;
            StatusCode = statusCode;
            UriPath = uriPath;
            TimeTaken = timeTaken;
            ResponseSize = responseSize;
            CacheStatus = cacheStatus;
            DataCadastro = dataCadastro ?? DateTime.UtcNow;
        }

        public Guid ID { get; set; }
        public string Provider { get; set; }
        public string HttpMethod { get; set; }
        public int StatusCode { get; set; }
        public string UriPath { get; set; }
        public double TimeTaken { get; set; }
        public int ResponseSize { get; set; }
        public string CacheStatus { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
