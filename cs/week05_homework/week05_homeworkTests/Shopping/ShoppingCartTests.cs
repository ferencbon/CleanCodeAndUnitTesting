using Microsoft.VisualStudio.TestTools.UnitTesting;
using week05_homework.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace week05_homework.Shopping.Tests
{
    [TestClass()]
    public class ShoppingCartTests
    {
        private Mock<ICartItemFactory> _mockCartItemFactory;
        private ShoppingCart _shoppingCart;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCartItemFactory = new Mock<ICartItemFactory>();
            _shoppingCart = new ShoppingCart(_mockCartItemFactory.Object);
        }

        [TestMethod]
        public void AddItem_ShouldAddItemToCart()
        {
            // Arrange
            var cartItem = new Mock<ICartItem>();
            _mockCartItemFactory.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<double>())).Returns(cartItem.Object);

            // Act
            _shoppingCart.AddItem("TestProduct", 99.99);

            // Assert
            _mockCartItemFactory.Verify(f => f.Create("TestProduct", 99.99), Times.Once);
        }

        [TestMethod]
        public void CalculateTotal_ShouldReturnCorrectTotal()
        {
            // Arrange
            double price = 50;
            var cartItem1 = new Mock<ICartItem>();
            cartItem1.Setup(i => i.Price).Returns(price);
            var cartItem2 = new Mock<ICartItem>();
            cartItem2.Setup(i => i.Price).Returns(price);
            _mockCartItemFactory.SetupSequence(f => f.Create(It.IsAny<string>(), It.IsAny<double>()))
                .Returns(cartItem1.Object)
                .Returns(cartItem2.Object);
            
            _shoppingCart.AddItem("dummy", 0);
            _shoppingCart.AddItem("dummy", 0);

            double expectedResult = price * 2;

            // Act
            double actual = _shoppingCart.CalculateTotal();

            // Assert
            Assert.AreEqual(expectedResult, actual);
        }
        [TestMethod]
        public void CalculateTotal_ShouldReturnZeroWhenCartIsEmpty()
        {
           // Act
            double actual = _shoppingCart.CalculateTotal();

            // Assert
            Assert.AreEqual(0, actual);
        }
    }
}