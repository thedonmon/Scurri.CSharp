using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Scurri.Client;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Scurri.Tests.Core
{
    public class CarriersTest : IClassFixture<ScurriFixture>
    {
        private readonly ScurriFixture _fixture;
        public CarriersTest(ScurriFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task TestCarrierGet()
        {
            using (var scope = _fixture.Container.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<IRestScurriApiClient>();
                var carriers = await client.GetCarriersAsyncSafe();
                carriers.data.Count().Should().BeGreaterThan(0);
            }
        }
        [Fact]
        public async Task TestCarrierServicesGet()
        {
            using (var scope = _fixture.Container.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<IRestScurriApiClient>();
                var carriers = await client.GetCarrierServicesAsync();
                carriers.Count().Should().BeGreaterThan(0);
            }
        }
    }
}
