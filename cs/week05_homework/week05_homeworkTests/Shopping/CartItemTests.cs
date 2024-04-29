using Microsoft.VisualStudio.TestTools.UnitTesting;
using week05_homework.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week05_homework.Shopping.Tests
{
    [TestClass()]
    public class CartItemTests
    {
            [TestMethod]
        [DataRow("TestProduct", 99.99)]
        [DataRow("TestProductWithSpecialChars&@$ß", 199.99)]
        [DataRow("Test Product with whitespaces", 299.99)]
            public void CartItemConstructor_ShouldSetPropertiesCorrectly(string name, double price)
            {

                // Arrange&Act
                CartItem cartItem = new CartItem(name, price);

                // Assert
                Assert.AreEqual(name, cartItem.Product);
                Assert.AreEqual(price, cartItem.Price);
            }
    }
}