using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet("[action]/{userName}", Name = "GetAllBasket")]
    [ProducesResponseType(typeof(ShoppingCart), 200)]
    public async Task<ActionResult<ShoppingCart>> GetAllBasket()
    {
        var basket = await _basketRepository.GetAllBasketAsync();
        return Ok(basket);
    }


    [HttpGet("[action]/{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), 200)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var basket = await _basketRepository.GetBasketAsync(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
    {
        var basket = await _basketRepository.UpdateBasketAsync(shoppingCart);
        return Ok(shoppingCart);
    }
    [HttpDelete("[action]/{userName}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteBasket(string userName)
    {
        await _basketRepository.DeleteBasketAsync(userName);
        return Ok();
    }
}
