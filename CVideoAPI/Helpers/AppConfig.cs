using Microsoft.Extensions.Configuration;
using System;

namespace CVideoAPI.Helpers
{
    public static class AppConfig
    {
        private static IConfiguration currentConfig;
        public static void SetConfig(IConfiguration configuration)
        {
            currentConfig = configuration;
        }
        public static string GetConnectionString(string key)
        {
            try
            {
                string connectionString = currentConfig["ConnectionString:" + key];
                return connectionString;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static string GetRedisConnectionString()
        {
            try
            {
                string connectionString = currentConfig["RedisCacheSettings:ConnectionString"];
                return connectionString;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
