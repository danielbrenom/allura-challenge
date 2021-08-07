using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Alura.Challenge.BackEnd.Api.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
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
                .ConfigureWebHostDefaults(webBuilder =>
                 {
                     //Descomentar quando buildar imagem docker
                     // webBuilder.UseKestrel(o =>
                     // {
                     //     o.Listen(new IPEndPoint(IPAddress.Any, 443), listenOptions =>
                     //     {
                     //         var certPassword = Environment.GetEnvironmentVariable("Kestrel:Certificates:Default:Password");
                     //         var certPath = Environment.GetEnvironmentVariable("Kestrel:Certificates:Default:Path");
                     //         var certificate = new X509Certificate2(certPath, certPassword);
                     //         var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions()
                     //         {
                     //             ClientCertificateMode = ClientCertificateMode.NoCertificate,
                     //             SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                     //             ServerCertificate = certificate,
                     //         };
                     //         listenOptions.UseHttps(httpsConnectionAdapterOptions);
                     //     });
                     // });
                     webBuilder.UseStartup<Startup>();
                 });
    }
}