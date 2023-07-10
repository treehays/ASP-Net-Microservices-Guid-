namespace Basket.API.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var item in ShoppingCartItems)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
        //set
        //{

        //}
    }
    public ShoppingCart()
    {

    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}
