using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dojo.CoffeeMachine.Tests
{
    [TestClass]
    public class OrderTranslatorTests
    {
        private readonly IDrinkMaker _mockDrinkMaker = Mock.Of<IDrinkMaker>();
        private readonly DrinkMaker _realDrinkMaker = new DrinkMaker();

        [TestMethod]
        public void GivenAnOrderWithCoffeeAndOneSugarThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Coffee(), 1) { Sugars = 1 });

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("C:1:0"), Times.Once);
        }

        [TestMethod]
        public void GivenAGenericOrderWithChocolateAndNoSugarThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Chocolate(), 1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("H::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderWithTeaAnd2SugarsThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Tea(), 1) { Sugars = 2 });

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("T:2:0"), Times.Once);
        }

        [TestMethod]
        public void GivenAMessageFromDrinkMakerThenCoffeeMachineDisplaysTheMessage()
        {
            const string expected = "Hello dear customer!";
            
            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            _realDrinkMaker.SendMessage(expected);

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }
        
        [TestMethod]
        public void GivenAnOrderWithInsuffisantAmountOfMoneyThenGetExpectedMessage()
        {
            const string expected = "Missing 0,40 €";

            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Tea()));

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }

        [TestMethod]
        public void GivenAnOrderWithExtraHotChocolateThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new ExtraHot(new Chocolate()), 1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("Hh::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderWithExtraHotCoffeeThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new ExtraHot(new Coffee()), 1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("Ch::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderWithExtraHotTeaThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new ExtraHot(new Tea()), 1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("Th::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderWithOrangeJuiceThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new OrangeJuice(), 1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("O::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderOfOrangeJuiceWithInsuffisantAmountOfMoneyThenGetExpectedMessage()
        {
            const string expected = "Missing 0,60 €";

            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new OrangeJuice()));

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }

        [TestMethod]
        public void GivenAnOrderOfCoffeeWithMachiattoOptionAndInsuffisantAmountOfMoneyThenGetExpectedMessage()
        {
            // l'option Macchiato coute 0,30 €, donc le café macchiato coute 0,60 + 0,30 = 0,90 €
            const string expected = "Missing 0,90 €";

            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Macchiato(new Coffee())));

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }

        [TestMethod]
        public void GivenAnOrderOfCoffeeWithMachiattoAndMilkOptionsAndInsuffisantAmountOfMoneyThenGetExpectedMessage()
        {
            // l'option Macchiato coute 0,30 €, l'option lait coute 0,10 €, donc le café latte macchiato coute 0,60 + 0,30 + 0,10 = 1,00 €
            const string expected = "Missing 1,00 €";

            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Milk(new Macchiato(new Coffee()))));

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }

        [TestMethod]
        public void GivenAnOrderOfCoffeeWithMachiattoAndMilkOptionsAndTowSugarsAndInsuffisantAmountOfMoneyThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Milk(new Macchiato(new Coffee())), 1) { Sugars = 2 });

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("Cml:2:0"), Times.Once);
        }

        [TestMethod]
        public void GivenOneOrderOfCoffeeThenGetExpectedReport()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new Coffee(), 1));

            IEnumerable<string> actual = coffeeMachine.GetReport();

            Assert.AreEqual(1, actual.Count(), "Le rapport ne contient pas le bon nombre de boissons");

            Assert.IsTrue(actual.Any(o => o == "C:1:0,60")); // 1 café pour un total de 0,60 €
        }
        [TestMethod]
        public void GivenSomeOrdersThenGetExpectedReport()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order(new OrangeJuice(), 1));
            coffeeMachine.Order(new Order(new ExtraHot(new Chocolate()), 1));
            coffeeMachine.Order(new Order(new OrangeJuice(), 1));
            coffeeMachine.Order(new Order(new ExtraHot(new Chocolate()), 1));
            coffeeMachine.Order(new Order(new Milk(new Macchiato(new Coffee())), 1) { Sugars = 2 });
            coffeeMachine.Order(new Order(new ExtraHot(new Chocolate()), 1));

            IEnumerable<string> actual = coffeeMachine.GetReport();

            Assert.AreEqual(3, actual.Count(), "Le rapport ne contient pas le bon nombre de boissons");

            Assert.IsTrue(actual.Any(o => o == "O:2:1,20")); // 2 jus d'orange pour un total de 1,20 €
            Assert.IsTrue(actual.Any(o => o == "Hh:3:1,50")); // 3 chocolats extra chaud pour un total de 1,50 €
            Assert.IsTrue(actual.Any(o => o == "Cml:1:1,00")); // 1 café latté macchiato pour un total de 1 €
        }
    }
}