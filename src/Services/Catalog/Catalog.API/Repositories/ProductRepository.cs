using System.Xml.Linq;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context ?? throw new ArgumentException(nameof(context));
    }

    public async Task CreateProductAsync(Product product)
    {
        await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Id, id);
        var response = await _context.Products.DeleteOneAsync(filter);
        var rette = response.IsAcknowledged && response.DeletedCount > 0;
        return rette;
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryNameAsync(string categoryName)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, categoryName);
        var product = await _context.Products.Find(filter).ToListAsync();
        return product;
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        var products = await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        return products;
    }

    public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
        var product = await _context.Products.Find(filter).ToListAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var product = await _context.Products.Find(p => true).ToListAsync();
        return product;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {

        var updater = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
        var response = updater.IsAcknowledged && updater.ModifiedCount > 0;
        return response;
    }
}
