using System;
using Allura.Challenge.Database.Repositories.Base;
using Allura.Challenge.Database.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Allura.Challenge.BackEnd.Configurations
{
    public class ApplicationConfigurator
    {
        public static ApplicationConfigurator Instance { get; protected set; }
        protected internal ServiceProvider Container;

        public static void Instantiate()
        {
            if (Instance != null)
                return;
            Instance = new ApplicationConfigurator();
        }

        protected internal static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(MapperConfiguration.Instance.Mapper);
            serviceCollection.Configure<ConnectionSettings>(ConfigurationsWrapper.GetConfiguration().GetSection(nameof(ConnectionSettings)));
            serviceCollection.AddSingleton<IConnectionSettings>(setting =>  setting.GetRequiredService<IOptions<ConnectionSettings>>().Value);
        }

        public object GetService(Type tipo)
        {
            return Container.GetService(tipo);
        }

        public TType GetService<TType>()
        {
            return Container.GetService<TType>();
        }
    }

    public static class ApplicationConfiguratorExtension
    {
        public static IServiceCollection ConfigureContainer(this IServiceCollection services, ApplicationConfigurator configurator)
        {
            ApplicationConfigurator.Initialize(services);
            configurator.Container = services.BuildServiceProvider();
            return services;
        }

        public static ApplicationConfigurator Get(this ApplicationConfigurator applicationConfigurator)
        {
            return applicationConfigurator;
        }
    }
}