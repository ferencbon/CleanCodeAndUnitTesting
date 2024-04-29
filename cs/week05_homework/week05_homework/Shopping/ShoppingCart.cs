using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week05_homework.Shopping
{
    public class ShoppingCart
    {
        private readonly ICartItemFactory _cartItemFactory;
        private List<ICartItem> items = new List<ICartItem>();

        public ShoppingCart(ICartItemFactory cartItemFactory)
        {
            _cartItemFactory = cartItemFactory;
        }

        public void AddItem(string product, double price)
        {
            ICartItem newItem = _cartItemFactory.Create(product, price);
            items.Add(newItem);
        }

        public double CalculateTotal()
        {
            double total = 0;

            total = items.Sum(x => x.Price);
           
            return total;
        }
    }
}
