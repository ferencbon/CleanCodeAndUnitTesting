namespace week05_homework.Shopping;

public interface ICartItemFactory
{
    ICartItem Create(string product, double price);
}
