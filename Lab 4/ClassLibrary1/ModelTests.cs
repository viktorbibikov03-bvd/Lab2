using NUnit.Framework;
using Model;
using NUnit.Framework.Legacy;

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
        /// <param name="input">Входное значение имени</param>
        /// <param name="expected">Ожидаемое 
        /// отформатированное значение</param>
        [TestCase("иван", "Иван", TestName = "Тест форматирования имени " +
            "при вводе русский символов нижнего регистра")]
        [TestCase("ИВАН", "Иван", TestName = "Тест форматирования имени " +
            "при вводе русский символов верхнего регистра")]
        [TestCase("мария александровна", "Мария Александровна", TestName = 
            "Тест форматирования имени при вводе имени и отчества")]
        [TestCase("alex", "Alex", TestName = "Тест форматирования имени " +
            "при вводе латинских символов нижнего регистра")]
        [TestCase("ALEX", "Alex", TestName = "Тест форматирования имени " +
            "при вводе латинских символов верхнего регистра")]
        public void FirstName_ValidValue_FormattedCorrectly(
            string input, string expected)
        {
            var employee = new SalaryEmployee();

            employee.FirstName = input;

            ClassicAssert.AreEqual(expected, employee.FirstName);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения 
        /// при некорректных значениях для FirstName
        /// </summary>
        /// <param name="invalidValue">Некорректное значение</param>
        [TestCase(null, TestName = "Тест на null в имени")]
        [TestCase("", TestName = "Тест на Empty в имени")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 
            TestName = "Тест имени длиной более 30 символов")]
        public void FirstName_InvalidValue_ThrowsException(
            string invalidValue)
        {
            var employee = new SalaryEmployee();

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.FirstName = invalidValue);
        }

        #endregion

        #region Tests for LastName

        /// <summary>
        /// Проверяет установку и форматирование 
        /// корректных фамилий в свойство LastName
        /// </summary>
        /// <param name="input">Входное значение фамилии</param>
        /// <param name="expected">Ожидаемое 
        /// отформатированное значение</param>
        [TestCase("петров", "Петров", TestName = "Тест форматирования " +
            "фамилии при вводе русский символов нижнего регистра")]
        [TestCase("СМИРНОВ", "Смирнов", TestName = "Тест форматирования " +
            "фамилии при вводе русский символов нижнего регистра")]
        [TestCase("ivanov", "Ivanov", TestName = "Тест форматирования " +
            "фамилии при вводе латинских символов нижнего регистра")]
        [TestCase("IVANOV", "Ivanov", TestName = "Тест форматирования " +
            "фамилии при вводе латинских символов верхнего регистра")]
        public void LastName_ValidValue_FormattedCorrectly(
            string input, string expected)
        {
            var employee = new WageEmployee();

            employee.LastName = input;

            ClassicAssert.AreEqual(expected, employee.LastName);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при 
        /// некорректных значениях для LastName
        /// </summary>
        /// <param name="invalidValue">Некорректное значение</param>
        [TestCase(null, TestName = "Тест на null в фамилии")]
        [TestCase("", TestName = "Тест на Empty в фамилии")]
        [TestCase("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")]
        public void LastName_InvalidValue_ThrowsException(
            string invalidValue)
        {
            var employee = new WageEmployee();

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.LastName = invalidValue);
        }

        #endregion

        #region Tests for Age

        /// <summary>
        /// Данные для тестов валидного возраста: (пол, возраст, описание теста)
        /// </summary>
        public static IEnumerable<TestCaseData> ValidAgeCases
        {
            get
            {
                yield return new TestCaseData(Gender.Male, 18, "Male_MinBoundary").SetName("Age_Male_MinBoundary_Accepted");
                yield return new TestCaseData(Gender.Male, 45, "Male_Valid").SetName("Age_Male_ValidValue_Accepted");
                yield return new TestCaseData(Gender.Male, 65, "Male_MaxBoundary").SetName("Age_Male_MaxBoundary_Accepted");
                yield return new TestCaseData(Gender.Female, 18, "Female_MinBoundary").SetName("Age_Female_MinBoundary_Accepted");
                yield return new TestCaseData(Gender.Female, 50, "Female_Valid").SetName("Age_Female_ValidValue_Accepted");
                yield return new TestCaseData(Gender.Female, 60, "Female_MaxBoundary").SetName("Age_Female_MaxBoundary_Accepted");
            }
        }

        /// <summary>
        /// Проверяет установку корректного возраста в зависимости от пола сотрудника
        /// </summary>
        /// <param name="gender">Пол сотрудника</param>
        /// <param name="age">Значение возраста</param>
        /// <param name="description">Описание сценария (для отладки)</param>
        [TestCaseSource(nameof(ValidAgeCases))]
        public void Age_ValidValue_SetsCorrectly(Gender gender, int age, string description)
        {
            var employee = new SalaryEmployee 
            { 
                Gender = gender 
            };

            employee.Age = age;

            ClassicAssert.AreEqual(age, employee.Age);
        }

        /// <summary>
        /// Данные для тестов невалидного возраста
        /// </summary>
        public static IEnumerable<TestCaseData> InvalidAgeCases
        {
            get
            {
                yield return new TestCaseData(Gender.Male, 17, "BelowMin").SetName("Age_BelowMinAge_ThrowsException");
                yield return new TestCaseData(Gender.Male, 66, "AboveRetirementMale").SetName("Age_Male_AboveRetirement_ThrowsException");
                yield return new TestCaseData(Gender.Female, 61, "AboveRetirementFemale").SetName("Age_Female_AboveRetirement_ThrowsException");
                yield return new TestCaseData(Gender.Male, -5, "Negative").SetName("Age_NegativeValue_ThrowsException");
            }
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном возрасте
        /// </summary>
        /// <param name="gender">Пол сотрудника</param>
        /// <param name="age">Некорректное значение возраста</param>
        /// <param name="description">Описание сценария</param>
        [TestCaseSource(nameof(InvalidAgeCases))]
        public void Age_InvalidValue_ThrowsException(Gender gender, int age, string description)
        {
            var employee = new SalaryEmployee { Gender = gender };

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Age = age);
        }

        #endregion

        #region Параметризованные тесты для свойства Profession

        /// <summary>
        /// Проверяет установку и форматирование корректных профессий
        /// </summary>
        /// <param name="input">Входное значение профессии</param>
        /// <param name="expected">Ожидаемое отформатированное значение</param>
        [TestCase("программист", "Программист")]
        [TestCase("МЕНЕДЖЕР ПРОЕКТОВ", "Менеджер Проектов")]
        [TestCase("designer", "Designer")]
        public void Profession_ValidValue_FormattedCorrectly(string input, string expected)
        {
            var employee = new SalaryEmployee();

            employee.Profession = input;

            ClassicAssert.AreEqual(expected, employee.Profession);
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректных значениях профессии
        /// </summary>
        /// <param name="invalidValue">Некорректное значение</param>
        [TestCase(null)]
        [TestCase("")]
        [TestCase("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
        public void Profession_InvalidValue_ThrowsException(string invalidValue)
        {
            var employee = new WageEmployee();

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Profession = invalidValue);
        }

        #endregion

        #region Параметризованные тесты для SalaryEmployee.Salary

        /// <summary>
        /// Данные для тестов валидного оклада: (значение, описание)
        /// </summary>
        public static IEnumerable<TestCaseData> ValidSalaryCases
        {
            get
            {
                yield return new TestCaseData(0, "Zero").SetName("Salary_ZeroValue_Accepted");
                yield return new TestCaseData(50000, "Valid").SetName("Salary_ValidValue_Accepted");
                yield return new TestCaseData(200000, "MaxBoundary").SetName("Salary_MaxBoundaryValue_Accepted");
            }
        }

        /// <summary>
        /// Проверяет установку корректных значений оклада
        /// </summary>
        /// <param name="salary">Значение оклада</param>
        /// <param name="description">Описание сценария</param>
        [TestCaseSource(nameof(ValidSalaryCases))]
        public void Salary_ValidValue_SetsCorrectly(double salary, string description)
        {
            var employee = new SalaryEmployee();

            employee.Salary = salary;

            ClassicAssert.AreEqual(salary, employee.Salary);
        }

        /// <summary>
        /// Данные для тестов невалидного оклада: (значение, описание)
        /// </summary>
        public static IEnumerable<TestCaseData> InvalidSalaryCases
        {
            get
            {
                yield return new TestCaseData(-1, "Negative").SetName("Salary_NegativeValue_ThrowsException");
                yield return new TestCaseData(200001, "ExceedsMax").SetName("Salary_ExceedsMaxValue_ThrowsException");
            }
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном окладе
        /// </summary>
        /// <param name="salary">Некорректное значение оклада</param>
        /// <param name="description">Описание сценария</param>
        [TestCaseSource(nameof(InvalidSalaryCases))]
        public void Salary_InvalidValue_ThrowsException(double salary, string description)
        {
            var employee = new SalaryEmployee();

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Salary = salary);
        }

        #endregion

        #region Параметризованные тесты для SalaryEmployee.Commission

        /// <summary>
        /// Данные для тестов валидного процента комиссионных
        /// </summary>
        public static IEnumerable<TestCaseData> ValidCommissionCases
        {
            get
            {
                yield return new TestCaseData(0, "Zero").SetName("Commission_ZeroValue_Accepted");
                yield return new TestCaseData(15.5, "Valid").SetName("Commission_ValidValue_Accepted");
                yield return new TestCaseData(100, "MaxBoundary").SetName("Commission_MaxBoundaryValue_Accepted");
            }
        }

        /// <summary>
        /// Проверяет установку корректных значений процента комиссионных
        /// </summary>
        [TestCaseSource(nameof(ValidCommissionCases))]
        public void Commission_ValidValue_SetsCorrectly(double commission, string description)
        {
            var employee = new SalaryEmployee();

            employee.Commission = commission;

            ClassicAssert.AreEqual(commission, employee.Commission);
        }

        /// <summary>
        /// Данные для тестов невалидного процента комиссионных
        /// </summary>
        public static IEnumerable<TestCaseData> InvalidCommissionCases
        {
            get
            {
                yield return new TestCaseData(-0.1, "Negative").SetName("Commission_NegativeValue_ThrowsException");
                yield return new TestCaseData(100.1, "ExceedsMax").SetName("Commission_ExceedsMaxValue_ThrowsException");
            }
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном проценте комиссионных
        /// </summary>
        [TestCaseSource(nameof(InvalidCommissionCases))]
        public void Commission_InvalidValue_ThrowsException(double commission, string description)
        {
            var employee = new SalaryEmployee();

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Commission = commission);
        }

        #endregion

        #region Параметризованные тесты для SalaryEmployee.CalculateSalary

        /// <summary>
        /// Данные для тестов расчёта зарплаты: (оклад, комиссия, ожидаемый результат, описание)
        /// </summary>
        public static IEnumerable<TestCaseData> SalaryCalculationCases
        {
            get
            {
                yield return new TestCaseData(50000, 0, 50000, "ZeroCommission").SetName("CalculateSalary_ZeroCommission_ReturnsBaseSalary");
                yield return new TestCaseData(40000, 25, 50000, "WithCommission25").SetName("CalculateSalary_WithCommission25_CalculatedCorrectly");
                yield return new TestCaseData(100000, 100, 200000, "MaxCommission").SetName("CalculateSalary_WithMaxCommission_DoublesSalary");
                yield return new TestCaseData(75000, 10.5, 82875, "FractionalCommission").SetName("CalculateSalary_WithFractionalCommission_CalculatedCorrectly");
            }
        }

        /// <summary>
        /// Проверяет корректность расчёта зарплаты по формуле: Salary × (1 + Commission / 100)
        /// </summary>
        /// <param name="salary">Базовый оклад</param>
        /// <param name="commission">Процент комиссионных</param>
        /// <param name="expected">Ожидаемый результат расчёта</param>
        /// <param name="description">Описание сценария</param>
        [TestCaseSource(nameof(SalaryCalculationCases))]
        public void CalculateSalary_SalaryEmployee_CalculatedCorrectly(
            double salary, double commission, double expected, string description)
        {
            var employee = new SalaryEmployee
            {
                Salary = salary,
                Commission = commission
            };

            var result = employee.CalculateSalary();

            ClassicAssert.AreEqual(expected, result, 0.01,
                $"Расчёт не совпадает для сценария: {description}");
        }

        #endregion

        #region Параметризованные тесты для WageEmployee.HourCount

        /// <summary>
        /// Данные для тестов валидного количества часов
        /// </summary>
        public static IEnumerable<TestCaseData> ValidHourCountCases
        {
            get
            {
                yield return new TestCaseData(0, "Zero").SetName("HourCount_ZeroValue_Accepted");
                yield return new TestCaseData(80.5, "Valid").SetName("HourCount_ValidValue_Accepted");
                yield return new TestCaseData(115, "MaxBoundary").SetName("HourCount_MaxBoundaryValue_Accepted");
            }
        }

        /// <summary>
        /// Проверяет установку корректного количества отработанных часов
        /// </summary>
        [TestCaseSource(nameof(ValidHourCountCases))]
        public void HourCount_ValidValue_SetsCorrectly(double hours, string description)
        {
            var employee = new WageEmployee();

            employee.HourCount = hours;

            ClassicAssert.AreEqual(hours, employee.HourCount);
        }

        /// <summary>
        /// Данные для тестов невалидного количества часов
        /// </summary>
        public static IEnumerable<TestCaseData> InvalidHourCountCases
        {
            get
            {
                yield return new TestCaseData(-1, "Negative").SetName("HourCount_NegativeValue_ThrowsException");
                yield return new TestCaseData(116, "ExceedsMax").SetName("HourCount_ExceedsMaxValue_ThrowsException");
            }
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректном количестве часов
        /// </summary>
        [TestCaseSource(nameof(InvalidHourCountCases))]
        public void HourCount_InvalidValue_ThrowsException(double hours, string description)
        {
            var employee = new WageEmployee();

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.HourCount = hours);
        }

        #endregion

        #region Параметризованные тесты для WageEmployee.Wage

        /// <summary>
        /// Данные для тестов валидной почасовой ставки
        /// </summary>
        public static IEnumerable<TestCaseData> ValidWageCases
        {
            get
            {
                yield return new TestCaseData(0, "Zero").SetName("Wage_ZeroValue_Accepted");
                yield return new TestCaseData(500.75, "Valid").SetName("Wage_ValidValue_Accepted");
                yield return new TestCaseData(2000, "MaxBoundary").SetName("Wage_MaxBoundaryValue_Accepted");
            }
        }

        /// <summary>
        /// Проверяет установку корректной почасовой ставки
        /// </summary>
        [TestCaseSource(nameof(ValidWageCases))]
        public void Wage_ValidValue_SetsCorrectly(double wage, string description)
        {
            var employee = new WageEmployee();

            employee.Wage = wage;

            ClassicAssert.AreEqual(wage, employee.Wage);
        }

        /// <summary>
        /// Данные для тестов невалидной почасовой ставки
        /// </summary>
        public static IEnumerable<TestCaseData> InvalidWageCases
        {
            get
            {
                yield return new TestCaseData(-1, "Negative").SetName("Wage_NegativeValue_ThrowsException");
                yield return new TestCaseData(2001, "ExceedsMax").SetName("Wage_ExceedsMaxValue_ThrowsException");
            }
        }

        /// <summary>
        /// Проверяет выбрасывание исключения при некорректной почасовой ставке
        /// </summary>
        [TestCaseSource(nameof(InvalidWageCases))]
        public void Wage_InvalidValue_ThrowsException(double wage, string description)
        {
            var employee = new WageEmployee();

            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Wage = wage);
        }

        #endregion

        #region Параметризованные тесты для WageEmployee.CalculateSalary

        /// <summary>
        /// Данные для тестов расчёта почасовой зарплаты: (ставка, часы, ожидаемый результат, описание)
        /// </summary>
        public static IEnumerable<TestCaseData> WageCalculationCases
        {
            get
            {
                yield return new TestCaseData(500, 100, 50000, "IntegerValues").SetName("CalculateSalary_Hourly_IntegerValues_CalculatedCorrectly");
                yield return new TestCaseData(350.75, 40.5, 14205.375, "FractionalValues").SetName("CalculateSalary_WithFractionalValues_CalculatedCorrectly");
                yield return new TestCaseData(1000, 0, 0, "ZeroHours").SetName("CalculateSalary_ZeroHours_ReturnsZero");
                yield return new TestCaseData(0, 50, 0, "ZeroWage").SetName("CalculateSalary_ZeroWage_ReturnsZero");
            }
        }

        /// <summary>
        /// Проверяет корректность расчёта почасовой зарплаты по формуле: Wage × HourCount
        /// </summary>
        [TestCaseSource(nameof(WageCalculationCases))]
        public void CalculateSalary_WageEmployee_CalculatedCorrectly(
            double wage, double hours, double expected, string description)
        {
            var employee = new WageEmployee
            {
                Wage = wage,
                HourCount = hours
            };

            var result = employee.CalculateSalary();

            ClassicAssert.AreEqual(expected, result, 0.001,
                $"Расчёт не совпадает для сценария: {description}");
        }

        #endregion

        #region Параметризованные тесты для свойства Gender

        /// <summary>
        /// Проверяет установку значений перечисления Gender
        /// </summary>
        /// <param name="gender">Значение пола</param>
        [TestCase(Gender.Male, TestName = "Gender_MaleValue_SetsCorrectly")]
        [TestCase(Gender.Female, TestName = "Gender_FemaleValue_SetsCorrectly")]
        public void Gender_ValidValue_SetsCorrectly(Gender gender)
        {
            var employee = new SalaryEmployee();

            employee.Gender = gender;

            ClassicAssert.AreEqual(gender, employee.Gender);
        }

        #endregion

        #region Параметризованные тесты для конструкторов

        /// <summary>
        /// Данные для тестов инициализации конструкторами: (тип сотрудника, ожидаемые значения)
        /// </summary>
        public static IEnumerable<TestCaseData> ConstructorInitializationCases
        {
            get
            {
                yield return new TestCaseData(
                    new SalaryEmployee(),
                    string.Empty, string.Empty, string.Empty, 0, 0,
                    "SalaryEmployee").SetName("SalaryEmployee_DefaultConstructor_InitializesProperties");

                yield return new TestCaseData(
                    new WageEmployee(),
                    string.Empty, string.Empty, string.Empty, 0, 0,
                    "WageEmployee").SetName("WageEmployee_DefaultConstructor_InitializesProperties");
            }
        }

        /// <summary>
        /// Проверяет корректную инициализацию свойств конструктором по умолчанию
        /// </summary>
        /// <param name="employee">Экземпляр сотрудника</param>
        /// <param name="expectedFirstName">Ожидаемое значение FirstName</param>
        /// <param name="expectedLastName">Ожидаемое значение LastName</param>
        /// <param name="expectedProfession">Ожидаемое значение Profession</param>
        /// <param name="expectedNumeric1">Ожидаемое значение первого числового свойства</param>
        /// <param name="expectedNumeric2">Ожидаемое значение второго числового свойства</param>
        /// <param name="employeeType">Тип сотрудника для сообщения</param>
        [TestCaseSource(nameof(ConstructorInitializationCases))]
        public void DefaultConstructor_InitializesProperties(
            EmployeBase employee,
            string expectedFirstName, string expectedLastName, string expectedProfession,
            double expectedNumeric1, double expectedNumeric2,
            string employeeType)
        {
            ClassicAssert.AreEqual(expectedFirstName, employee.FirstName,
                $"FirstName не инициализирован для {employeeType}");
            ClassicAssert.AreEqual(expectedLastName, employee.LastName,
                $"LastName не инициализирован для {employeeType}");
            ClassicAssert.AreEqual(expectedProfession, employee.Profession,
                $"Profession не инициализирован для {employeeType}");
        }

        #endregion

        #region Параметризованные тесты для исключений

        /// <summary>
        /// Проверяет сохранение сообщения в исключении IncorrectArgumentException
        /// </summary>
        /// <param name="message">Тестовое сообщение</param>
        [TestCase("Тестовое сообщение", TestName = "IncorrectArgumentException_Message_Preserved")]
        [TestCase("", TestName = "IncorrectArgumentException_EmptyMessage_Preserved")]
        [TestCase("Ошибка валидации: поле не может быть пустым", TestName = "IncorrectArgumentException_LongMessage_Preserved")]
        public void IncorrectArgumentException_Message_Preserved(string message)
        {
            var exception = new IncorrectArgumentException(message);

            ClassicAssert.AreEqual(message, exception.Message);
        }

        /// <summary>
        /// Данные для тестов валидации диапазона: (значение, поле, min, max, кастомное сообщение, ожидание)
        /// </summary>
        public static IEnumerable<TestCaseData> ValidateRangeCases
        {
            get
            {
                yield return new TestCaseData(10, "test", 5, 20, null, false, "Valid_InRange")
                    .SetName("ValidateRange_ValidValue_DoesNotThrow");
                yield return new TestCaseData(5, "test", 5, 20, null, false, "Valid_MinBoundary")
                    .SetName("ValidateRange_MinBoundary_DoesNotThrow");
                yield return new TestCaseData(20, "test", 5, 20, null, false, "Valid_MaxBoundary")
                    .SetName("ValidateRange_MaxBoundary_DoesNotThrow");

                yield return new TestCaseData(4, "testField", 5, 20, null, true, "BelowMin_DefaultMessage")
                    .SetName("ValidateRange_BelowMin_ThrowsWithDefaultMessage");
                yield return new TestCaseData(21, "testField", 5, 20, null, true, "AboveMax_DefaultMessage")
                    .SetName("ValidateRange_AboveMax_ThrowsWithDefaultMessage");
                yield return new TestCaseData(3, "customField", 5, 20, "Пользовательское сообщение", true, "CustomMessage")
                    .SetName("ValidateRange_InvalidValue_ThrowsWithCustomMessage");
            }
        }

        /// <summary>
        /// Проверяет корректность работы метода валидации диапазона
        /// </summary>
        [TestCaseSource(nameof(ValidateRangeCases))]
        public void ValidateRange_CorrectlyValidatesInput(
            double value, string fieldName, int minValue, int maxValue,
            string customMessage, bool shouldThrow, string description)
        {
            var employee = new TestEmployee();

            if (shouldThrow)
            {
                var exception = ClassicAssert.Throws<IncorrectArgumentException>(
                    () => employee.TestValidateRange(value, fieldName, minValue, maxValue, customMessage));

                if (customMessage != null)
                {
                    ClassicAssert.AreEqual(customMessage, exception.Message,
                        $"Кастомное сообщение не совпадает для сценария: {description}");
                }
                else
                {
                    ClassicAssert.IsTrue(exception.Message.Contains(fieldName),
                        $"Сообщение не содержит имя поля для сценария: {description}");
                    ClassicAssert.IsTrue(exception.Message.Contains(minValue.ToString()) &&
                                       exception.Message.Contains(maxValue.ToString()),
                        $"Сообщение не содержит границы диапазона для сценария: {description}");
                }
            }
            else
            {
                Assert.DoesNotThrow(() =>
                    employee.TestValidateRange(value, fieldName, minValue, maxValue, customMessage));
            }
        }

        #endregion

        #region Вспомогательный класс для тестирования protected-методов

        /// <summary>
        /// Вспомогательный класс-наследник EmployeBase для тестирования
        /// защищённых методов базового класса
        /// </summary>
        public class TestEmployee : EmployeBase
        {
            /// <summary>
            /// Абстрактная реализация метода CalculateSalary для возможности наследования
            /// </summary>
            public override double CalculateSalary() => 0;

            /// <summary>
            /// Публичный обёрточный метод для вызова защищённого метода ValidateRange
            /// </summary>
            public void TestValidateRange(double value, string fieldName,
                int minValue, int maxValue, string customMessage = null)
            {
                ValidateRange(value, fieldName, minValue, maxValue, customMessage);
            }
        }

        #endregion
    }
}