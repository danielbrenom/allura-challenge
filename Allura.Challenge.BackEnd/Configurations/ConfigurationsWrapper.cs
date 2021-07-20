using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Allura.Challenge.BackEnd.Configurations
{
    public class ConfigurationsWrapper
    {
        public static IConfigurationRoot GetConfiguration(string userSecretsKey = null)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("local.settings.json", optional: false, reloadOnChange: true);
            if (!string.IsNullOrWhiteSpace(envName))
                builder.AddJsonFile($"{envName}.settings.json", optional: true);
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}