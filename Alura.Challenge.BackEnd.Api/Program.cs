using Alura.Challenge.BackEnd.Api.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Alura.Challenge.BackEnd.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApplicationConfigurator.Instantiate();
            MapperConfiguration.Initialize();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}