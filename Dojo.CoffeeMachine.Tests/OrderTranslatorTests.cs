using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dojo.CoffeeMachine.Tests
{
    [TestClass]
    public class OrderTranslatorTests
    {
        private OrderTranslator _orderTranslator;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _orderTranslator = new OrderTranslator();
        }

        [TestMethod]
        public void GivenAnOrderWithCoffeeAndOneSugarThenGetExpectedCommand()
        {
            // Given
            var order = new Order { Drink = new Coffee(), Sugars = 1 };

            // Then
            const string expected = "C:1:0";

            string actual = _orderTranslator.Translate(order);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAGenericOrderWithCoffeeAndOneSugarThenGetExpectedCommand()
        {
            // Given
            var order = new Order<Coffee> { Sugars = 1 };
            
            // Then
            const string expected = "C:1:0";

            string actual = _orderTranslator.Translate(order);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAGenericOrderWithChocolateAndNoSugarThenGetExpectedCommand()
        {
            // Given
            var order = new Order<Chocolate>();

            // Then
            const string expected = "H::";

            string actual = _orderTranslator.Translate(order);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAGenericOrderWithTeaAnd2SugarsThenGetExpectedCommand()
        {
            // Given
            var order = new Order<Tea>{ Sugars = 2 };

            // Then
            const string expected = "T:2:0";

            string actual = _orderTranslator.Translate(order);

            Assert.AreEqual(expected, actual);
        }
    }
}