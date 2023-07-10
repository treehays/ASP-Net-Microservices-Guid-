using Basket.API.Entities;

namespace Basket.API.Repositories;

public interface IBasketRepository
{
    Task DeleteBasketAsync(string userName);
    Task<ShoppingCart> GetBasketAsync(string userName);
    Task<List<ShoppingCart>> GetAllBasketAsync();
    Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart);

}
