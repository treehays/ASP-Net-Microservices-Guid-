using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ILogger<CatalogController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet("GetProductById/{id:length(24)}")]
    [ProducesResponseType(typeof(Product), 200)]
    public async Task<IActionResult> GetProduct(string id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpGet("GetAllProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productRepository.GetProductsAsync();
        return Ok(products);
    }

    [Route("[action]/{categoryName}")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryName(string categoryName)
    {
        var products = await _productRepository.GetProductByCategoryNameAsync(categoryName);
        return Ok(products);
    }

    [HttpPost("[action]")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        await _productRepository.CreateProductAsync(product);
        return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut("[action]")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        var pro = await _productRepository.UpdateProductAsync(product);
        return Ok(pro);
    }

    [HttpDelete("DeleteProduct/{id:length(24)}")]
    [ProducesResponseType(typeof(Product), 200)]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
    {
        var product = await _productRepository.DeleteProductAsync(id);
        return Ok(product);
    }

}
