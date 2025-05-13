using Microsoft.AspNetCore.Mvc;
using TrueStory.Application.Services;
using TrueStory.Domain.Dtos;

namespace TrueStory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController:ControllerBase
{
    private readonly ProductApiService _productService;

    public ProductsController(ProductApiService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductsDto request)
    {
            var products = await _productService.GetAllAsync(request);
            return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

            var response = await _productService.CreateAsync(productDto);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
            var response = await _productService.DeleteAsync(id);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}
