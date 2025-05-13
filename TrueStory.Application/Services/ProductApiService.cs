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

        public async Task<PaginatedResponse<Product>> GetAllAsync(GetProductsDto request)
        {
            // http call to get all products
            var response = await _httpClient.GetStringAsync(_baseUrl);
            var items = JArray.Parse(response);
            // filter if name is provided and ignore case
            var filtered = items
                .Where(item => string.IsNullOrEmpty(request.name) ||
                               item["name"]?.ToString().Contains(request.name, StringComparison.OrdinalIgnoreCase) == true)
                .Skip((request.page - 1) * request.pageSize)
                .Take(request.pageSize)
                //project to product response
                .Select(item => new Product
                {
                    Id = item["id"]?.ToString(),
                    Name = item["name"]?.ToString(),
                    //dictionary to handle customer key and value
                    Data = item["data"]?.ToObject<Dictionary<string, object>>()
                });
            // returns paginated response
            var result= filtered.ToPaginatedResponse<Product>(request.page, request.pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateProductDto productDto)
        {
            // project to dto fro requrest
            var payload = new
            {
                name = productDto.Name,
                data = productDto.Data
            };
            //returns api response
            return await _httpClient.PostAsJsonAsync(_baseUrl, payload);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }
    }
}
