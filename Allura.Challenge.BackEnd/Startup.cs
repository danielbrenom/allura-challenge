using Allura.Challenge.BackEnd.Configurations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Allura.Challenge.BackEnd.Startup))]
namespace Allura.Challenge.BackEnd
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ApplicationConfigurator.Instantiate();
            MapperConfiguration.Initialize();
            builder.Services.ConfigureContainer(ApplicationConfigurator.Instance.Get());
        }
    }
}