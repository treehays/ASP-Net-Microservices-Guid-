using Dapper;
using Discount.API.Entities;
using Discount.API.Repositories;
using Npgsql;

namespace Discount.API;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;
    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affected = await connection.ExecuteAsync($"INSERT INTO Coupon (ProductName, ProductDescription, Amount,ProductType) VALUES (@ProductName, @ProductDescription, @Amount, @ProductType)", new
        {
            ProductName = coupon.ProductName,
            ProductDescription = coupon.ProductDescription,
            Amount = coupon.Amount,
            ProductType = coupon.ProductType,
        });
        if (affected is 0) return false;
      
        return true;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var coupon = await connection.QueryFirstAsync<Coupon>($"select * from Coupon where ProductName = {productName}");
        if (coupon is null) return new Coupon
        {
            ProductName = "No product",
            Amount = 0,
            ProductDescription = "No product found",
            ProductType = "Maggi",
        };
        return coupon;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affected = await connection.ExecuteAsync($"Update Coupon set ProductName = @ProductName, ProductDescription = @ProductDescription, Amount = @Amount,ProductType = @ProductType", new
        {
            ProductName = coupon.ProductName,
            ProductDescription = coupon.ProductDescription,
            Amount = coupon.Amount,
            ProductType = coupon.ProductType,
        });
        if (affected is 0) return false;

        return true;
    }
}
