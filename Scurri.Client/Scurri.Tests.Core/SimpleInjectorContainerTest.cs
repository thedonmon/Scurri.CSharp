using Scurri.Client;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Scurri.Tests.Core
{
    public class SimpleInjectorContainerTest : IClassFixture<ScurriFixture>
    {
        private readonly ScurriFixture _fixture;
        public SimpleInjectorContainerTest(ScurriFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public void CanVerifyContainer()
        {
            var container = new Container();
            container.Options.AllowOverridingRegistrations = false;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            var config = new ScurriConfiguration
            {
                CompanySlug = _fixture.configuration["company-slug"],
                AuthToken = _fixture.configuration["authtoken"],
                UserName = _fixture.configuration["loginname"],
                Secret = _fixture.configuration["secret"]
            };
            container.RegisterInstance<IScurriConfiguration>(config);
            container.Register<IRestScurriApiClient,ScurriClient>(Lifestyle.Scoped);
            container.Verify();

        }
    }
}
