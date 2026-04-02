using NUnit.Framework;
using Model;
using NUnit.Framework.Legacy;

namespace ModelTests
{
    /// <summary>
    /// Класс для проведения тестов
    /// </summary>
    [TestFixture]
    public class Class1
    {
        /// <summary>
        /// Метод для проведения теста
        /// </summary>
        [Test]
        public void TestForFirstName()
        {
            //arrange
            var salaryEmployee = new SalaryEmployee();
            const string TestFirstName = "Иван";

            //act
            salaryEmployee.FirstName = "Иван";

            //assert
            ClassicAssert.AreEqual(TestFirstName, salaryEmployee.FirstName);
        }
    }
}