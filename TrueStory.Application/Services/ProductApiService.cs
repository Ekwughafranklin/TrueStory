using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TrueStory.Application.Helpers;
using TrueStory.Domain.Dtos;
using TrueStory.Domain.Entities;

namespace TrueStory.Application.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://api.restful-api.dev/objects";

        public ProductApiService(HttpClient httpClient, IOptions<ProductServiceOptions> options)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(GetProductsDto request)
        {
            var response = await _httpClient.GetStringAsync(_baseUrl);
            var items = JArray.Parse(response);

            var filtered = items
                .Where(item => string.IsNullOrEmpty(request.name) ||
                               item["name"]?.ToString().Contains(request.name, StringComparison.OrdinalIgnoreCase) == true)
                .Skip((request.page - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(item => new Product
                {
                    Id = item["id"]?.ToString(),
                    Name = item["name"]?.ToString(),
                    Data = item["data"]?.ToObject<Dictionary<string, object>>()
                });

            return filtered;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateProductDto productDto)
        {
            var payload = new
            {
                name = productDto.Name,
                data = productDto.Data
            };
            return await _httpClient.PostAsJsonAsync(_baseUrl, payload);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }
    }
}
