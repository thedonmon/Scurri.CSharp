using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scurri.Client;
using System.Threading.Tasks;
using Xunit;

namespace Scurri.CSharp.Tests
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
                var carriers = await client.GetCarriersAsync();
            }
        }
    }
}
