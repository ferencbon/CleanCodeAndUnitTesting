namespace week05_homework.Shopping;

public interface ICartItem
{
    string Product { get; }
    double Price { get; }
}

public class CartItem : ICartItem
{
    public string Product { get; }
    public double Price { get; }

    public CartItem(string product, double price)
    {
        Product = product;
        Price = price;
    }
}