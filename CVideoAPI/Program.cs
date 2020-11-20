using CVideoAPI.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CVideoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string json = File.ReadAllText(@"appsettings.json");
            JObject o = JObject.Parse(@json);
            AppSettings.Settings = JsonConvert.DeserializeObject<AppSettings>(o["AppSettings"].ToString());
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
