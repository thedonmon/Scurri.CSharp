using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Scurri.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Scurri.Tests.Core
{
    public class WarehouseTest : IClassFixture<ScurriFixture>
    {
        private readonly ScurriFixture _fixture;
        public WarehouseTest(ScurriFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task TestGetWarehouse()
        {
            using (var scope = _fixture.Container.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<IRestScurriApiClient>();
                var warehouses = await client.GetWarehousesAsyncSafe();
                warehouses.data.Count.Should().BeGreaterThan(0);
            }
        }
    }
}
