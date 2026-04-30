using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Model;

namespace ModelTests
{
    /// <summary>
    /// Класс для проведения модульных тестов
    /// </summary>
    [TestFixture]
    public class ModelTests
    {
        #region Tests for FirstName

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

        #endregion

        #region Tests for LastName

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

        #endregion

        #region Tests for Age

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

        #endregion

        #region Tests for Profession

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

        #endregion

        #region Tests for SalaryEmployee.Salary

        /// <summary>
        /// Проверяет установку корректных значений оклада
        /// </summary>
        [TestCase(0, TestName = "Тест при наименьшем значении оклада")]
        [TestCase(50000, TestName = "Тест при нормальном значении оклада")]
        [TestCase(200000, TestName = "Тест при максимальном значении оклада")]

        public void Salary_ValidValue_SetsCorrectly(double salary)
        {
            var employee = new SalaryEmployee();
            employee.Salary = salary;
            Assert.AreEqual(salary, employee.Salary);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном 
        /// окладе
        /// </summary>
        [TestCase(-1, 
            TestName = "Тест при отрицательном значении оклада")]
        [TestCase(200001, 
            TestName = "Тест при значении оклада выше максимального")]
        public void Salary_InvalidValue_ThrowsException(double salary)
        {
            var employee = new SalaryEmployee();
            Assert.Throws<IncorrectArgumentException>(
                () => employee.Salary = salary);
        }

        #endregion

        #region Tests for SalaryEmployee.Commission

        /// <summary>
        /// Проверяет установку корректных значений процента 
        /// комиссионных
        /// </summary>
        [TestCase(0,
            TestName = "Тест при минимальном проценте комиссионных")]
        [TestCase(15.5,
            TestName = "Тест при нормальном проценте комиссионных")]
        [TestCase(100,
            TestName = "Тест при максимальном проценте комиссионных")]
        public void Commission_ValidValue_SetsCorrectly(
            double commission)
        {
            var employee = new SalaryEmployee();
            employee.Commission = commission;
            Assert.AreEqual(commission, employee.Commission);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном 
        /// проценте комиссионных
        /// </summary>
        [TestCase(-1,
            TestName = "Тест при отрицательном проценте комиссионных")]
        [TestCase(100.1,
            TestName = "Тест при проценте комиссионных выше 100")]
        public void Commission_InvalidValue_ThrowsException(
            double commission)
        {
            var employee = new SalaryEmployee();
            Assert.Throws<IncorrectArgumentException>(
                () => employee.Commission = commission);
        }

        #endregion

        #region Tests for SalaryEmployee.CalculateSalary

        /// <summary>
        /// Проверяет корректность расчёта зарплаты по формуле: 
        /// Salary * (1 + Commission / 100)
        /// </summary>
        [TestCase(50000, 0, 50000, "Тест при Commission = 0",
            TestName = "Тест на расчет Salary при " +
                "наименьшем проценте комиссионных")]
        [TestCase(40000, 25, 50000, "Тест при Commission = 25",
            TestName = "Тест на расчет Salary при " +
                "целочисленном проценте комиссионных")]
        [TestCase(100000, 100, 200000, "Тест при Commission = 100",
            TestName = "Тест на расчет Salary при " +
                "наибольшем проценте комиссионных")]
        [TestCase(75000, 10.5, 82875, "Тест при Commission = 10.5",
            TestName = "Тест на расчет Salary при " +
                "дробном проценте комиссионных")]
        public void CalculateSalary_SalaryEmployee_CalculatedCorrectly(
            double salary, double commission,
            double expected, string description)
        {
            var employee = new SalaryEmployee
            {
                Salary = salary,
                Commission = commission
            };
            var result = employee.CalculateSalary();
            Assert.AreEqual(expected, result, 0.01,
                $"Расчёт не совпадает для сценария: {description}");
        }

        #endregion

        #region Tests for WageEmployee.HourCount

        /// <summary>
        /// Проверяет установку корректного количества 
        /// отработанных часов
        /// </summary>
        [TestCase(0,
            TestName = "Тест при минимальном значении отработанных часов")]
        [TestCase(80.5,
            TestName = "Тест при дробном значении отработанных часов")]
        [TestCase(115,
            TestName = "Тест при максимальном значении отработанных часов")]
        public void HourCount_ValidValue_SetsCorrectly(double hours)
        {
            var employee = new WageEmployee();
            employee.HourCount = hours;
            Assert.AreEqual(hours, employee.HourCount);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном 
        /// количестве часов
        /// </summary>
        [TestCase(-1,
            TestName = "Тест при отрицательном значении отработанных часов")]
        [TestCase(116,
            TestName = "Тест при значении отработанных часов, " +
            "превышающем максимальное")]
        public void HourCount_InvalidValue_ThrowsException(
            double hours)
        {
            var employee = new WageEmployee();
            Assert.Throws<IncorrectArgumentException>(
                () => employee.HourCount = hours);
        }

        #endregion

        #region Tests for WageEmployee.Wage

        /// <summary>
        /// Проверяет установку корректной почасовой ставки
        /// </summary>
        [TestCase(0,
            TestName = "Тест при минимальном значении оплаты")]
        [TestCase(500.75,
            TestName = "Тест при дробном значении оплаты")]
        [TestCase(2000,
            TestName = "Тест при максимальном значении оплаты")]
        public void Wage_ValidValue_SetsCorrectly(double wage)
        {
            var employee = new WageEmployee();
            employee.Wage = wage;
            Assert.AreEqual(wage, employee.Wage);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректной 
        /// почасовой ставке
        /// </summary>
        [TestCase(-1,
            TestName = "Тест при отрицательном значении оплаты")]
        [TestCase(2001,
            TestName = "Тест при значении оплаты, превышающем максимальное")]
        public void Wage_InvalidValue_ThrowsException(double wage)
        {
            var employee = new WageEmployee();
            Assert.Throws<IncorrectArgumentException>(
                () => employee.Wage = wage);
        }

        #endregion

        #region Tests for WageEmployee.CalculateSalary

        /// <summary>
        /// Проверяет корректность расчёта почасовой зарплаты 
        /// по формуле: Wage * HourCount
        /// </summary>
        [TestCase(500, 100, 50000, "Тест при целых числах",
            TestName = "Тест для расчета почасовой оплаты " +
                "при целочисленных значениях")]
        [TestCase(350.75, 40.5, 14205.375, "Тест при дробных числах",
            TestName = "Тест для расчета почасовой оплаты " +
                "при дробных значениях")]
        [TestCase(1000, 0, 0, "Тест при нулевых часах",
            TestName = "Тест для расчета почасовой оплаты " +
                "при нулевых отработанных часах")]
        [TestCase(0, 50, 0, "Тест при нулевой ставке",
            TestName = "Тест для расчета почасовой оплаты " +
                "при нулевой почасовой оплате")]
        public void CalculateSalary_WageEmployee_CalculatedCorrectly(
            double wage, double hours,
            double expected, string description)
        {
            var employee = new WageEmployee
            {
                Wage = wage,
                HourCount = hours
            };
            var result = employee.CalculateSalary();
            Assert.AreEqual(expected, result, 0.001,
                $"Расчёт не совпадает для сценария: {description}");
        }

        #endregion

        #region Tests for Gender

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

        #endregion

        #region Tests for construction

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

        #endregion

        #region Tests for IncorrectArgumentException

        /// <summary>
        /// Проверяет сохранение сообщения в исключении 
        /// IncorrectArgumentException
        /// </summary>
        [TestCase("Тестовое сообщение",
            TestName = "Тест сообщения об исключении")]
        [TestCase("",
            TestName = "Тест пустого сообщения об исключении")]
        [TestCase("Ошибка валидации: поле не может быть пустым",
            TestName = "Тест длинного сообщения об исключении")]
        public void IncorrectArgumentException_Message_Preserved(
            string message)
        {
            var exception = new IncorrectArgumentException(message);
            Assert.AreEqual(message, exception.Message);
        }

        /// <summary>
        /// Проверяет корректность работы метода валидации диапазона
        /// </summary>
        /// <param name="value">Число для проверки</param>
        /// <param name="fieldName">Название поля</param>
        /// <param name="minValue">Минимальная граница</param>
        /// <param name="maxValue">Максимальная граница</param>
        /// <param name="customMessage">Пользовательское сообщение</param>
        /// <param name="shouldThrow">Ожидается ли исключение</param>
        /// <param name="description">Описание сценария</param>
        [TestCase(10, "test", 5, 20, null, false, "Внутри границ",
            TestName = "Тест исключения при значении внутри границ")]
        [TestCase(5, "test", 5, 20, null, false, "На нижней границе",
            TestName = "Тест исключения при значении равном " +
                       "минимальной границе")]
        [TestCase(20, "test", 5, 20, null, false, "На верхней границе",
            TestName = "Тест исключения при значении равном " +
                       "максимальной границе")]
        [TestCase(4, "testField", 5, 20, null, true, "Ниже минимума",
            TestName = "Тест исключения при значении меньшем " +
                       "нижней границы")]
        [TestCase(21, "testField", 5, 20, null, true, "Выше максимума",
            TestName = "Тест исключения при значении большем " +
                       "верхней границы")]
        [TestCase(3, "Исключение с сообщением", 5, 20,
            "Пользовательское сообщение", true, "CustomMessage",
            TestName = "Тест исключения с пользовательским сообщением")]
        public void ValidateRange_CorrectlyValidatesInput(
            double value, string fieldName, int minValue,
            int maxValue, string customMessage, bool shouldThrow,
            string description)
        {
            var employee = new TestEmployee();

            if (shouldThrow)
            {
                var exception = Assert.Throws<IncorrectArgumentException>(
                    () => employee.TestValidateRange(value, fieldName,
                        minValue, maxValue, customMessage));

                if (customMessage != null)
                {
                    Assert.That(exception.Message,
                        Is.EqualTo(customMessage),
                        $"Кастомное сообщение не совпадает для " +
                        $"сценария: {description}");
                }
                else
                {
                    Assert.That(exception.Message,
                        Does.Contain(fieldName),
                        $"Сообщение не содержит имя поля для " +
                        $"сценария: {description}");
                    Assert.That(exception.Message,
                        Does.Contain(minValue.ToString()).And
                            .Contain(maxValue.ToString()),
                        $"Сообщение не содержит границы диапазона " +
                        $"для сценария: {description}");
                }
            }
            else
            {
                Assert.DoesNotThrow(() =>
                    employee.TestValidateRange(value, fieldName,
                    minValue, maxValue, customMessage));
            }
        }

        #endregion

        #region Class for testing protected methods

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

        #endregion
    }
}