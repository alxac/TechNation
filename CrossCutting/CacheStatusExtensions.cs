namespace TechNation.CrossCutting
{
    public static class CacheStatusExtensions
    {
        public static string ToCacheStatus(this string cacheStatus)
        {
            switch (cacheStatus)
            {
                case "HIT":
                    return "HIT";
                case "MISS":
                    return "MISS";
                case "INVALIDATE":
                    return "REFRESH_HIT";
                default:
                    return cacheStatus;
            }
        }
    }
}
