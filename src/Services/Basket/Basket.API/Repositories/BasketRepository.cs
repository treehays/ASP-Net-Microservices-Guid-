using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _distributedCache;

    public BasketRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    }

    public async Task DeleteBasketAsync(string userName)
    {
        await _distributedCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart> GetBasketAsync(string userName)
    {
        var basket = await _distributedCache.GetStringAsync(userName);
        if (string.IsNullOrWhiteSpace(basket))
        {
            return null;
        }
        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart)
    {
        await _distributedCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
        var basket = await GetBasketAsync(shoppingCart.UserName);
        return basket;
    }
}
