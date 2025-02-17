using LearnMyCalculatorApp;
using FluentAssertions;
using Moq;

namespace TestCalculator
{
    public class Bar
    {
        public virtual Baz Baz { get; set; }
        public virtual bool Submit() { return false; }
    }

    public class Baz
    {
        public virtual string Name { get; set; }
    }

    public interface ICalculator
    {
        int Add(int x, int y);

        int Subtract(int x, int y);

        int Multiply(int x, int y);

        int? Divide(int x, int y);
    }

    [TestClass]
    public sealed class CalculatorTests
    {
        [TestMethod]
        public void CalculatorNullTest()
        {
            var calculator = new Calculator();
            Assert.IsNotNull(calculator);
        }

        [TestMethod]
        public void AddTest()
        {
            var calculator = new Calculator();
            var actual = calculator.Add(1, 1);
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void SubtractTest()
        {
            var calculator = new Calculator();
            var actual = calculator.Subtract(1, 1);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            var calculator = new Calculator();
            var actual = calculator.Multiply(1, 1);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void DivideTest()
        {
            var calculator = new Calculator();
            var actual = calculator.Divide(1, 1);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void DivideByZeroTest()
        {
            var calculator = new Calculator();
            var actual = calculator.Divide(1, 0);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void AddTestFluentassertion()
        {
            var calculator = new Calculator();
            var actual = calculator.Add(1, 1);
            actual.Should().Be(2).And.NotBe(1);
        }

        [DataTestMethod]
        [DataRow(1, 1, 2)]
        [DataRow(2, 2, 4)]
        [DataRow(3, 3, 6)]
        //[DataRow(0, 0, 1)] // The test run with this row fails
        public void AddDataTests(int x, int y, int expected)
        {
            var calculator = new Calculator();
            var actual = calculator.Add(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DivideByZeroWithMoq()
        {
            var mock = new Mock<ICalculator>();
            mock.Setup(calc => calc.Divide(0,1)).Returns(1);
            var actual = mock.Object.Divide(0,1);
            Assert.AreEqual(1, actual);
        }

        [DataTestMethod]
        [DataRow(2, 1, 2)]
        [DataRow(2, 2, 2)]
        [DataRow(1, 0, 0)]
        public void DivideWithMoqDataSet(int x, int y, int expected)
        {   
            var mock = new Mock<ICalculator>();
            mock.Setup(calc => calc.Divide(x, y)).Returns(expected);
            var actual = mock.Object.Divide(x, y);
            Assert.AreEqual(expected, actual);
        }
    }
}
