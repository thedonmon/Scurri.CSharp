using Flurl.Http;
using Scurri.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scurri.Client
{
    public class Client : IRestApiClient
    {
        private readonly IScurriConfiguration _Config;
        private readonly string _AuthToken;
        private const string BaseUrl = "https://sandbox.scurri.co.uk/api/v1/company/";


        public Client(IScurriConfiguration Config)
        {
            _Config = Config;
            ValidateConfig(_Config);
            _AuthToken = GenerateToken(_Config);
        }

        private string GenerateToken(IScurriConfiguration config)
        {
            if (!NullHelper(false, config.AuthToken))
            {
                return config.AuthToken;
            }
            else if (!NullHelper(true, config.UserName, config.Secret))
            {
                return Encoding.UTF8.EncodeBase64($"{config.UserName}:{config.Secret}");
            }
            else return null;
        }

        private void ValidateConfig(IScurriConfiguration config)
        {
            if (NullHelper(false, config.CompanySlug))
                throw new ApplicationException("Company Slug is Required");
            if (NullHelper(false, config.AuthToken) && NullHelper(true, config.UserName, config.Secret))
            {
                throw new ApplicationException("If AuthToken is not provided please provide both UserName and Secret to access the API");
            }
        }

        private bool NullHelper(bool checkOne, params string[] args)
        {
            if(checkOne)
                return args.Any(x => string.IsNullOrEmpty(x));
            else
                return args.All(x => string.IsNullOrEmpty(x));

        }
        public async Task<List<Carrier>> GetCarriersAsync()
        {
            var result = await $"{BaseUrl}{_Config.CompanySlug}/carriers/".WithHeader("Authorization", $"Basic {_AuthToken}").GetJsonAsync<List<Carrier>>();
            return result;
        }
        public async Task<List<CarrierService>> GetCarrierServicesAsync(bool enhancements = false, bool package_types = false)
        {
            var result = await $"{BaseUrl}{_Config.CompanySlug}/carrierservices/".WithHeader("Authorization", $"Basic {_AuthToken}")
                .SetQueryParams(new { enhancements = enhancements, package_types = package_types })
                .GetJsonAsync<List<CarrierService>>();
            return result;
        }
        public async Task<List<Warehouse>> GetWarehousesAsync()
        {
            var result = await $"{BaseUrl}{_Config.CompanySlug}/warehouses/".WithHeader("Authorization", $"Basic {_AuthToken}")
                .GetJsonAsync<List<Warehouse>>();
            return result;
        }
        public async Task<PaginationResult<Consignment>> GetConsignmentsAsync(string offset = "", int limit = 10, string identifier = null, string status = null)
        {
            var result = await $"{BaseUrl}{_Config.CompanySlug}/consignments/".WithHeader("Authorization", $"Basic {_AuthToken}")
                .SetQueryParams(new { offset = offset, limit = limit, identifier = identifier, status = status }, Flurl.NullValueHandling.Remove)
                .GetJsonAsync<PaginationResult<Consignment>>();
            return result;
        }
        public async Task<List<Consignment>> GetConsignmentsPagedAsync(int pagesToIterate = 0, string offset = "", int limit = 10, string identifier = null, string status = null)
        {
            var allCosignments = new List<Consignment>();

            if (pagesToIterate > 0)
            {
                var count = pagesToIterate;
                var resultsToPaginate = await GetConsignmentsAsync();
                allCosignments.AddRange(resultsToPaginate.results);
                while (!string.IsNullOrEmpty(resultsToPaginate.next) && resultsToPaginate.count <= count)
                {
                    var r = await GetConsignmentsAsync(offset: resultsToPaginate.next, limit, identifier, status);
                    allCosignments.AddRange(r.results);
                    count--;
                }
            }
            else
            {
                var results = await GetConsignmentsAsync();
                allCosignments.AddRange(results.results);
                while (!string.IsNullOrEmpty(results.next))
                {
                    var r = await GetConsignmentsAsync(offset: results.next, limit, identifier, status);
                    allCosignments.AddRange(r.results);
                }
            }
            return allCosignments;
        }
        public async Task<Consignment> GetConsignmentAsync(string consignment_id)
        {
            var result = await $"{BaseUrl}{_Config.CompanySlug}/consignment/{consignment_id}".WithHeader("Authorization", $"Basic {_AuthToken}")
                .GetJsonAsync<Consignment>();
            return result;
        }
    }
}
