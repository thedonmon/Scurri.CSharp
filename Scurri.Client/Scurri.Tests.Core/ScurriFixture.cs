using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scurri.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scurri.Tests.Core
{
    public class ScurriFixture
    {
        internal  IServiceProvider Container { get; private set; }

        internal  IServiceCollection serviceCollection { get; set; }
        internal  IConfigurationRoot configuration { get; private set; }
        public ScurriFixture()
        {

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            RegisterServices();

        }
        private void RegisterServices()
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();

            var scurriConfig = new ScurriConfiguration
            {
                CompanySlug = configuration["company-slug"],
                AuthToken = configuration["authtoken"],
                UserName = configuration["loginname"],
                Secret = configuration["secret"]
            };
            serviceCollection.AddSingleton<IScurriConfiguration, ScurriConfiguration>((o) => scurriConfig);
            serviceCollection.AddScoped<IRestScurriApiClient, ScurriClient>();
            Container = serviceCollection.BuildServiceProvider(validateScopes: true);
        }
    }
}
