using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Scurri.Client;
using Scurri.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Scurri.Tests.Core
{
    public class ConsignmentsTest : IClassFixture<ScurriFixture>
    {
        private readonly ScurriFixture _fixture;
        public ConsignmentsTest(ScurriFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task CanCreateAndGetConsignment()
        {
            using (var scope = _fixture.Container.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<IRestScurriApiClient>();
                var carrierService = (await client.GetCarrierServicesAsyncSafe()).data.FirstOrDefault();
                var warehouse = (await client.GetWarehousesAsyncSafe()).data.FirstOrDefault();
                var consigmentToCreate = new Consignment
                {
                    warehouse_id = warehouse.identifier,
                    service = carrierService.name,
                    service_id = carrierService.identifier,
                    carrier = carrierService.name,
                    consignment_number = "ABCTEST1",
                    order_number = "ABCMYORDERTEST1",
                    order_value = 100.00,
                    invoice = new Invoice
                    {
                        incoterm = "DDP"
                    },
                    recipient = new Recipient
                    {
                        first_name = "Ship",
                        last_name = "Bob",
                        name = "ShipBob",
                        contact_number = "555-555-5555",
                        email_address = "support@shipbob.com",
                        address = new Address
                        {
                            address1 = "Innovation House",
                            address2 = "The Bullring",
                            city = "Wexford",
                            country = "IE",
                            postcode = "Y35 DW6E",
                            state = "Wexford"
                        }
                    },
                    packages = new List<Package>
                    {
                        new Package
                        {
                            reference = "CustomCustomerReference1",
                            description = "shirt",
                            height = 5.0,
                            length = 7.0,
                            width = 8.0,
                            items = new List<Item>
                            {
                                new Item
                                {
                                    harmonisation_code = "5201",
                                    name = "tshirt",
                                    sku = "tshirt black",
                                    country_of_origin = "US",
                                    quantity = 1,
                                    value = 5.00,
                                    weight = 2

                                }
                            }
                        }

                    }
                };
                var result = await client.CreateConsignmentAsyncSafe(new List<Consignment> { consigmentToCreate });
                result.success.Should().BeTrue();
                result.data.success.FirstOrDefault().Should().NotBeNullOrEmpty();

                var consigmentRetrieved = await client.GetConsignmentAsyncSafe(result.data.success.FirstOrDefault());
                consigmentRetrieved.success.Should().BeTrue();
                consigmentRetrieved.data.current_status.rejection_reason.Should().BeNull();
                consigmentRetrieved.data.identifier.Should().NotBeNullOrEmpty();
            }

        }
        [Fact]
        public async Task CanCreateAndCancelConsignment()
        {
            using (var scope = _fixture.Container.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<IRestScurriApiClient>();
                var carrierService = (await client.GetCarrierServicesAsyncSafe()).data.FirstOrDefault();
                var warehouse = (await client.GetWarehousesAsyncSafe()).data.FirstOrDefault();
                var consigmentToCreate = new Consignment
                {
                    warehouse_id = warehouse.identifier,
                    service = carrierService.name,
                    service_id = carrierService.identifier,
                    carrier = carrierService.name,
                    consignment_number = "ABCTEST1",
                    order_number = "ABCMYORDERTEST1",
                    order_value = 100.00,
                    invoice = new Invoice
                    {
                        incoterm = "DDP"
                    },
                    recipient = new Recipient
                    {
                        first_name = "Ship",
                        last_name = "Bob",
                        name = "ShipBob",
                        contact_number = "555-555-5555",
                        email_address = "support@shipbob.com",
                        address = new Address
                        {
                            address1 = "Innovation House",
                            address2 = "The Bullring",
                            city = "Wexford",
                            country = "IE",
                            postcode = "Y35 DW6E",
                            state = "Wexford"
                        }
                    },
                    packages = new List<Package>
                    {
                        new Package
                        {
                            reference = "CustomCustomerReference1",
                            description = "shirt",
                            height = 5.0,
                            length = 7.0,
                            width = 8.0,
                            items = new List<Item>
                            {
                                new Item
                                {
                                    harmonisation_code = "5201",
                                    name = "tshirt",
                                    sku = "tshirt black",
                                    country_of_origin = "US",
                                    quantity = 1,
                                    value = 5.00,
                                    weight = 2

                                }
                            }
                        }

                    }
                };
                var result = await client.CreateConsignmentAsyncSafe(new List<Consignment> { consigmentToCreate });
                result.success.Should().BeTrue();
                var consigmentCancelled = await client.CancelConsignmentAsyncSafe(result.data.success.FirstOrDefault());
                consigmentCancelled.success.Should().BeTrue();
            }

        }
        [Fact]
        public async Task CanCreateAndUpdateConsignment()
        {
            using (var scope = _fixture.Container.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<IRestScurriApiClient>();
                var carrierService = (await client.GetCarrierServicesAsyncSafe()).data.FirstOrDefault();
                var warehouse = (await client.GetWarehousesAsyncSafe()).data.FirstOrDefault();
                var consigmentToCreate = new Consignment
                {
                    warehouse_id = warehouse.identifier,
                    service = carrierService.name,
                    service_id = carrierService.identifier,
                    carrier = carrierService.name,
                    consignment_number = "ABCTEST1",
                    order_number = "ABCMYORDERTEST1",
                    order_value = 100.00,
                    invoice = new Invoice
                    {
                        incoterm = "DDP"
                    },
                    recipient = new Recipient
                    {
                        first_name = "Ship",
                        last_name = "Bob",
                        name = "ShipBob",
                        contact_number = "555-555-5555",
                        email_address = "support@shipbob.com",
                        address = new Address
                        {
                            address1 = "Innovation House",
                            address2 = "The Bullring",
                            city = "Wexford",
                            country = "IE",
                            postcode = "Y35 DW6E",
                            state = "Wexford"
                        }
                    },
                    packages = new List<Package>
                    {
                        new Package
                        {
                            reference = "CustomCustomerReference1",
                            description = "shirt",
                            height = 5.0,
                            length = 7.0,
                            width = 8.0,
                            items = new List<Item>
                            {
                                new Item
                                {
                                    harmonisation_code = "5201",
                                    name = "tshirt",
                                    sku = "tshirt black",
                                    country_of_origin = "US",
                                    quantity = 1,
                                    value = 5.00,
                                    weight = 2

                                }
                            }
                        }

                    }
                };
                var result = await client.CreateConsignmentAsyncSafe(new List<Consignment> { consigmentToCreate });
                result.success.Should().BeTrue();

                var consignmentRetrieved = await client.GetConsignmentAsyncSafe(result.data.success.FirstOrDefault());
                var consignmentToUpdate = consignmentRetrieved.data;
                consignmentToUpdate.recipient.first_name = "Shipper";
                consignmentToUpdate.recipient.last_name = "Bobber";
                consignmentToUpdate.recipient.name = "ShipperBobber";
                consignmentToUpdate.packages.Add(new Package
                {
                    description = "jeans",
                    height = 5.0,
                    length = 7.0,
                    width = 8.0,
                    items = new List<Item>
                    {
                        new Item
                                {
                                    harmonisation_code = "5201",
                                    name = "jeans",
                                    sku = "jeans black",
                                    country_of_origin = "US",
                                    quantity = 1,
                                    value = 5.00,
                                    weight = 3

                                }
                    }

                });

                var consigmentUpdated = await client.UpdateConsignmentAsyncSafe(consignmentToUpdate);
                consigmentUpdated.success.Should().BeTrue();
                consigmentUpdated.data.packages.Count().Should().Be(2);
                consigmentUpdated.data.recipient.name.Should().Be("Shipper Bobber");
                consigmentUpdated.data.recipient.first_name.Should().Be("Shipper");
                consigmentUpdated.data.recipient.last_name.Should().Be("Bobber");

            }

        }
    }
}
