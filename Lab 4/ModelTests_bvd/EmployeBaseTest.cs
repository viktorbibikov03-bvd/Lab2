using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Model;

namespace ModelTests
{
    /// <summary>
    /// Класс для проведения модульных тестов
    /// </summary>
    [TestFixture]
    public class EmployeBaseTest
    {

        /// <summary>
        /// Проверяет установку и форматирование корректных 
        /// имён в свойство FirstName
        /// </summary>
        [TestCase("иван", "Иван",
            TestName = "Тест форматирования имени при вводе " +
                       "русских символов нижнего регистра")]
        [TestCase("ИВАН", "Иван",
            TestName = "Тест форматирования имени при вводе " +
                       "русских символов верхнего регистра")]
        [TestCase("мария александровна", "Мария Александровна",
            TestName = "Тест форматирования имени при вводе " +
                       "имени и отчества")]
        [TestCase("alex", "Alex",
            TestName = "Тест форматирования имени при вводе " +
                       "латинских символов нижнего регистра")]
        [TestCase("ALEX", "Alex",
            TestName = "Тест форматирования имени при вводе " +
                       "латинских символов верхнего регистра")]
        public void FirstName_ValidValue_FormattedCorrectly(
            string input, string expected)
        {
            var employee = new SalaryEmployee();
            employee.FirstName = input;
            Assert.AreEqual(expected, employee.FirstName);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректных 
        /// значениях для FirstName
        /// </summary>
        [TestCase(null, TestName = "Тест на null в имени")]
        [TestCase("", TestName = "Тест на Empty в имени")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            TestName = "Тест имени длиной более 30 символов")]
        public void FirstName_InvalidValue_ThrowsException(
            string invalidValue)
        {
            var employee = new SalaryEmployee();
            Assert.Throws<IncorrectArgumentException>(
                () => employee.FirstName = invalidValue);
        }

        /// <summary>
        /// Проверяет установку и форматирование корректных 
        /// фамилий в свойство LastName
        /// </summary>
        [TestCase("петров", "Петров",
            TestName = "Тест форматирования фамилии при вводе " +
                       "русских символов нижнего регистра")]
        [TestCase("СМИРНОВ", "Смирнов",
            TestName = "Тест форматирования фамилии при вводе " +
                       "русских символов верхнего регистра")]
        [TestCase("ivanov", "Ivanov",
            TestName = "Тест форматирования фамилии при вводе " +
                       "латинских символов нижнего регистра")]
        [TestCase("IVANOV", "Ivanov",
            TestName = "Тест форматирования фамилии при вводе " +
                       "латинских символов верхнего регистра")]
        public void LastName_ValidValue_FormattedCorrectly(
            string input, string expected)
        {
            var employee = new WageEmployee();
            employee.LastName = input;
            Assert.AreEqual(expected, employee.LastName);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректных 
        /// значениях для LastName
        /// </summary>
        [TestCase(null, TestName = "Тест на null в фамилии")]
        [TestCase("", TestName = "Тест на Empty в фамилии")]
        [TestCase("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
            TestName = "Тест фамилии длиной более 30 символов")]
        public void LastName_InvalidValue_ThrowsException(
            string invalidValue)
        {
            var employee = new WageEmployee();
            Assert.Throws<IncorrectArgumentException>(
                () => employee.LastName = invalidValue);
        }

        /// <summary>
        /// Проверка установки корректного возраста в зависимости 
        /// от пола сотрудника
        /// </summary>
        [TestCase(Gender.Male, 18, 
            TestName = "Тест при минимальном возрасте мужчин")]
        [TestCase(Gender.Male, 45, 
            TestName = "Тест при нормальном возрасте мужчин")]
        [TestCase(Gender.Male, 65, 
            TestName = "Тест при максимальном возрасте мужчин")]
        [TestCase(Gender.Female, 18, 
            TestName = "Тест при минимальном возрасте женщин")]
        [TestCase(Gender.Female, 50, 
            TestName = "Тест при нормальном возрасте женщин")]
        [TestCase(Gender.Female, 60, 
            TestName = "Тест при максимальном возрасте женщин")]
        public void Age_ValidValue_SetsCorrectly(
            Gender gender, int age)
        {
            var employee = new SalaryEmployee { Gender = gender };
            employee.Age = age;
            Assert.AreEqual(age, employee.Age);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном 
        /// возрасте
        /// </summary>
        [TestCase(Gender.Male, 17,
            TestName = "Тест при возрасте ниже минимального")]
        [TestCase(Gender.Male, 66,
            TestName = "Тест при возрасте выше максимального для мужчин")]
        [TestCase(Gender.Female, 61,
            TestName = "Тест при возрасте выше максимального для женщин")]
        [TestCase(Gender.Male, -5,
            TestName = "Тест при отрицательном возрасте")]
        public void Age_InvalidValue_ThrowsException(
            Gender gender, int age)
        {
            var employee = new SalaryEmployee { Gender = gender };
            Assert.Throws<IncorrectArgumentException>(
                () => employee.Age = age);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при установке возраста 
        /// с невалидным значением Gender
        /// </summary>
        [Test]
        [Property("Description", "Тест проверки Age при невалидном Gender")]
        public void Age_InvalidGender_ThrowsException()
        {
            var employee = new SalaryEmployee();

            typeof(EmployeBase)
                .GetProperty(nameof(EmployeBase.Gender))
                ?.SetValue(employee, (Gender)999);

            Assert.Throws<IncorrectArgumentException>(
                () => employee.Age = 25);
        }

        /// <summary>
        /// Проверяет установку и форматирование корректных 
        /// профессий
        /// </summary>
        [TestCase("программист", "Программист",
            TestName = "Тест форматирования профессии при вводе " +
                       "русских символов")]
        [TestCase("МЕНЕДЖЕР ПРОЕКТОВ", "Менеджер Проектов",
            TestName = "Тест форматирования профессии при вводе " +
                       "символов разных регистров")]
        [TestCase("designer", "Designer",
            TestName = "Тест форматирования профессии при вводе " +
                       "латинских символов")]
        public void Profession_ValidValue_FormattedCorrectly(
            string input, string expected)
        {
            var employee = new SalaryEmployee();
            employee.Profession = input;
            Assert.AreEqual(expected, employee.Profession);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректных 
        /// значениях профессии
        /// </summary>
        [TestCase(null, TestName = "Тест на null в профессии")]
        [TestCase("", TestName = "Тест на Empty в профессии")]
        [TestCase("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
            TestName = "Тест профессии длиной более 30 символов")]
        public void Profession_InvalidValue_ThrowsException(
            string invalidValue)
        {
            var employee = new WageEmployee();
            Assert.Throws<IncorrectArgumentException>(
                () => employee.Profession = invalidValue);
        }

        /// <summary>
        /// Проверяет установку значений перечисления Gender
        /// </summary>
        [TestCase(Gender.Male,
            TestName = "Тест при установке мужского пола")]
        [TestCase(Gender.Female,
            TestName = "Тест при установке женского пола")]
        public void Gender_ValidValue_SetsCorrectly(Gender gender)
        {
            var employee = new SalaryEmployee();
            employee.Gender = gender;
            Assert.AreEqual(gender, employee.Gender);
        }

        /// <summary>
        /// Проверяет корректную инициализацию свойств 
        /// конструктором по умолчанию
        /// </summary>
        [TestCase("Wage", TestName = "Тест конструктора WageEmployee")]
        [TestCase("Salary", TestName = "Тест конструктора SalaryEmployee")]
        public void DefaultConstructor_InitializesProperties(
            string employeeType)
        {
            EmployeBase employee = employeeType switch
            {
                "Wage" => new WageEmployee(),
                "Salary" => new SalaryEmployee(),
                _ => throw new ArgumentException("Неизвестный тип")
            };

            Assert.That(employee.FirstName, Is.Empty);
            Assert.That(employee.LastName, Is.Empty);
            Assert.That(employee.Profession, Is.Empty);
        }

        /// <summary>
        /// Вспомогательный класс-наследник EmployeBase для 
        /// тестирования защищённых методов
        /// </summary>
        public class TestEmployee : EmployeBase
        {
            public override double CalculateSalary() => 0;

            public void TestValidateRange(double value,
                string fieldName, int minValue, int maxValue,
                string customMessage = null)
            {
                ValidateRange(value, fieldName, minValue, maxValue,
                    customMessage);
            }
        }
    }
}