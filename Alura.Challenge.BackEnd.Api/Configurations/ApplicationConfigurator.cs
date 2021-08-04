using System;
using Alura.Challenge.BackEnd.Api.Services;
using Alura.Challenge.Database.Repositories;
using Alura.Challenge.Database.Repositories.Base;
using Alura.Challenge.Database.Repositories.Interfaces;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Validators;
using Alura.Challenge.Domain.Validators.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Alura.Challenge.BackEnd.Api.Configurations
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
            serviceCollection.AddSingleton(MapperConfiguration.Instance.Mapper)
                             .Configure<ConnectionSettings>(ConfigurationsWrapper.GetConfiguration().GetSection("ConnectionSettings"))
                             .AddSingleton<IConnectionSettings>(setting => setting.GetRequiredService<IOptions<ConnectionSettings>>().Value)
                             .AddSingleton<IValidator<Domain.Models.Data.Movie>, MovieValidator>()
                             .AddSingleton<IValidator<Domain.Models.Data.Category>, CategoryValidator>()
                             .AddSingleton<IValidator<Domain.Models.Data.User>, UserValidator>()
                             .AddTransient<IMovieRepository<Database.Models.Movie>, MovieRepository>()
                             .AddTransient<ICategoryRepository<Database.Models.Category>, CategoryRepository>()
                             .AddTransient<IUserRepository<Database.Models.User>, UserRepository>()
                             .AddTransient<IGrantTokenRepository<Database.Models.GrantToken>, GrantTokenRepository>()
                             .AddTransient<IMovieService, MovieService>()
                             .AddTransient<ICategoryService, CategoryService>()
                             .AddTransient<IUserService, UserService>()
                             .AddTransient<IGrantTokenService, GrantTokenService>()
                             .AddTransient<ILoginService, LoginService>();
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