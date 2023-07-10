namespace Discount.API.Entities;

public class Coupon
{
    public int Id { get; set; }
    public string ProductName{ get; set; }
    public string ProductDescription { get; set; }
    public string ProductType { get; set; }
    public int Amount { get; set; }
}
