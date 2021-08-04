using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Alura.Challenge.BackEnd.Api.Configurations
{
    public class ConfigurationsWrapper
    {
        public static IConfigurationRoot GetConfiguration(string userSecretsKey = null)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            if (!string.IsNullOrWhiteSpace(envName))
                builder.AddJsonFile($"appsettings.{envName}.json", optional: true);
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}