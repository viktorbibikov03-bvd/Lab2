using NUnit.Framework;
using Model;
using NUnit.Framework.Legacy;

namespace ModelTests
{
    /// <summary>
    /// Класс для проведения модульных тестов компонентов модели сотрудников
    /// </summary>
    [TestFixture]
    public class ModelTests
    {
        #region Tests for FirstName

        /// <summary>
        /// Проверяет, что корректное имя устанавливается в свойство 
        /// FirstName и автоматически форматируется 
        /// (первая буква заглавная, остальные строчные)
        /// </summary>
        [Test]
        public void FirstName_ValidValue_SetsCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee();
            const string expected = "Иван";

            // Act
            employee.FirstName = "иван";

            // Assert
            ClassicAssert.AreEqual(expected, employee.FirstName);
        }

        /// <summary>
        /// Проверяет, что попытка установить пустую строку в свойство 
        /// FirstName, выбрасывает исключение IncorrectArgumentException
        /// </summary>
        [Test]
        public void FirstName_EmptyString_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.FirstName = string.Empty);
        }

        /// <summary>
        /// Проверяет, что попытка установить значение null в свойство 
        /// FirstName выбрасывает исключение IncorrectArgumentException
        /// </summary>
        [Test]
        public void FirstName_NullValue_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.FirstName = null);
        }

        /// <summary>
        /// Проверяет, что имя длиннее максимального допустимого значения 
        /// вызывает исключение при установке в свойство FirstName
        /// </summary>
        [Test]
        public void FirstName_ExceedsMaxLength_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee();
            string longName = new string('a', 31);

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.FirstName = longName);
        }
        #endregion

        #region Tests for LastName

        /// <summary>
        /// Проверяет, что корректная фамилия устанавливается в свойство 
        /// LastName и форматируется в соответствии с правилами (TitleCase)
        /// </summary>
        [Test]
        public void LastName_ValidValue_SetsCorrectly()
        {
            // Arrange
            var employee = new WageEmployee();
            const string expected = "Петров";

            // Act
            employee.LastName = "петров";

            // Assert
            ClassicAssert.AreEqual(expected, employee.LastName);
        }

        /// <summary>
        /// Проверяет, что пустая строка в свойстве LastName 
        /// вызывает исключение IncorrectArgumentException
        /// </summary>
        [Test]
        public void LastName_EmptyString_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.LastName = string.Empty);
        }

        /// <summary>
        /// Проверяет, что имя длиннее максимального допустимого значения 
        /// вызывает исключение при установке в свойство LastName
        /// </summary>
        [Test]
        public void LastName_ExceedsMaxLength_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();
            string longName = new string('b', 35);

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.LastName = longName);
        }
        #endregion

        #region Tests for Age

        /// <summary>
        /// Проверяет установку корректного возраста для сотрудника мужского 
        /// пола в допустимом диапазоне от 18 до 65 лет
        /// </summary>
        [Test]
        public void Age_ValidValue_Male_SetsCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Gender = Gender.Male
            };
            const int expectedAge = 45;

            // Act
            employee.Age = expectedAge;

            // Assert
            ClassicAssert.AreEqual(expectedAge, employee.Age);
        }

        /// <summary>
        /// Проверяет установку корректного возраста для сотрудника женского 
        /// пола в допустимом диапазоне от 18 до 60 лет
        /// </summary>
        [Test]
        public void Age_ValidValue_Female_SetsCorrectly()
        {
            // Arrange
            var employee = new WageEmployee
            {
                Gender = Gender.Female
            };
            const int expectedAge = 55;

            // Act
            employee.Age = expectedAge;

            // Assert
            ClassicAssert.AreEqual(expectedAge, employee.Age);
        }

        /// <summary>
        /// Проверяет, что возраст младше минимального порога (18 лет)
        /// вызывает исключение при установке
        /// </summary>
        [Test]
        public void Age_BelowMinAge_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Gender = Gender.Male
            };

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Age = 17);
        }

        /// <summary>
        /// Проверяет, что возраст старше пенсионного для мужчин (65 лет)
        /// вызывает исключение при установке
        /// </summary>
        [Test]
        public void Age_AboveRetirementAge_Male_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Gender = Gender.Male
            };

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Age = 66);
        }

        /// <summary>
        /// Проверяет, что возраст старше пенсионного для женщин (60 лет)
        /// вызывает исключение при установке
        /// </summary>
        [Test]
        public void Age_AboveRetirementAge_Female_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee
            {
                Gender = Gender.Female
            };

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Age = 61);
        }

        /// <summary>
        /// Проверяет принятие минимального граничного 
        /// значения возраста (18 лет)
        /// </summary>
        [Test]
        public void Age_MinBoundaryValue_Accepted()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Gender = Gender.Male
            };

            // Act
            employee.Age = 18;

            // Assert
            ClassicAssert.AreEqual(18, employee.Age);
        }

        /// <summary>
        /// Проверяет принятие максимального граничного значения
        /// возраста для мужчин (65 лет)
        /// </summary>
        [Test]
        public void Age_MaxBoundaryValue_Male_Accepted()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Gender = Gender.Male
            };

            // Act
            employee.Age = 65;

            // Assert
            ClassicAssert.AreEqual(65, employee.Age);
        }

        /// <summary>
        /// Проверяет принятие максимального граничного значения 
        /// возраста для женщин (60 лет)
        /// </summary>
        [Test]
        public void Age_MaxBoundaryValue_Female_Accepted()
        {
            // Arrange
            var employee = new WageEmployee
            {
                Gender = Gender.Female
            };

            // Act
            employee.Age = 60;

            // Assert
            ClassicAssert.AreEqual(60, employee.Age);
        }
        #endregion

        #region Tests for Profession

        /// <summary>
        /// Проверяет, что корректное название профессии 
        /// устанавливается в свойство Profession
        /// и форматируется в стиле TitleCase
        /// </summary>
        [Test]
        public void Profession_ValidValue_SetsCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee();
            const string expected = "Программист";

            // Act
            employee.Profession = "программист";

            // Assert
            ClassicAssert.AreEqual(expected, employee.Profession);
        }

        /// <summary>
        /// Проверяет, что пустая строка в свойстве Profession 
        /// вызывает исключение
        /// </summary>
        [Test]
        public void Profession_EmptyString_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Profession = string.Empty);
        }

        /// <summary>
        /// Проверяет, что профессия длиннее 30 символов вызывает исключение
        /// </summary>
        [Test]
        public void Profession_ExceedsMaxLength_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();
            string longProfession = new string('x', 31);

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Profession = longProfession);
        }
        #endregion

        #region Tests for SalaryEmployee

        /// <summary>
        /// Проверяет установку корректного значения оклада в свойство Salary
        /// </summary>
        [Test]
        public void Salary_ValidValue_SetsCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee();
            const double expectedSalary = 75000;

            // Act
            employee.Salary = expectedSalary;

            // Assert
            ClassicAssert.AreEqual(expectedSalary, employee.Salary);
        }

        /// <summary>
        /// Проверяет, что нулевое значение оклада является допустимым
        /// </summary>
        [Test]
        public void Salary_ZeroValue_Accepted()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act
            employee.Salary = 0;

            // Assert
            ClassicAssert.AreEqual(0, employee.Salary);
        }

        /// <summary>
        /// Проверяет принятие максимального граничного 
        /// значения оклада (200 000 ₽)
        /// </summary>
        [Test]
        public void Salary_MaxBoundaryValue_Accepted()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act
            employee.Salary = 200000;

            // Assert
            ClassicAssert.AreEqual(200000, employee.Salary);
        }

        /// <summary>
        /// Проверяет, что оклад, превышающий максимальный лимит (200 000 ₽),
        /// вызывает исключение при установке
        /// </summary>
        [Test]
        public void Salary_ExceedsMaxValue_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Salary = 200001);
        }

        /// <summary>
        /// Проверяет, что отрицательное значение оклада вызывает исключение
        /// </summary>
        [Test]
        public void Salary_NegativeValue_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Salary = -1000);
        }

        /// <summary>
        /// Проверяет установку корректного процента комиссионных 
        /// в свойство Commission
        /// </summary>
        [Test]
        public void Commission_ValidValue_SetsCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee();
            const double expectedCommission = 15.5;

            // Act
            employee.Commission = expectedCommission;

            // Assert
            ClassicAssert.AreEqual(expectedCommission, employee.Commission);
        }

        /// <summary>
        /// Проверяет, что нулевое значение процента 
        /// комиссионных является допустимым
        /// </summary>
        [Test]
        public void Commission_ZeroValue_Accepted()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act
            employee.Commission = 0;

            // Assert
            ClassicAssert.AreEqual(0, employee.Commission);
        }

        /// <summary>
        /// Проверяет принятие максимального граничного 
        /// значения комиссионных (100%)
        /// </summary>
        [Test]
        public void Commission_MaxBoundaryValue_Accepted()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act
            employee.Commission = 100;

            // Assert
            ClassicAssert.AreEqual(100, employee.Commission);
        }

        /// <summary>
        /// Проверяет, что процент комиссионных свыше 
        /// 100% вызывает исключение
        /// </summary>
        [Test]
        public void Commission_ExceedsMaxValue_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Commission = 101);
        }

        /// <summary>
        /// Проверяет, что отрицательное значение процента 
        /// комиссионных вызывает исключение
        /// </summary>
        [Test]
        public void Commission_NegativeValue_ThrowsException()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Commission = -5);
        }

        /// <summary>
        /// Проверяет расчёт зарплаты при нулевом проценте комиссионных:
        /// результат должен быть равен базовому окладу
        /// </summary>
        [Test]
        public void CalculateSalary_WithZeroCommission_ReturnsBaseSalary()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Salary = 50000,
                Commission = 0
            };
            const double expected = 50000;

            // Act
            var result = employee.CalculateSalary();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Проверяет корректность расчёта зарплаты с учётом комиссионных
        /// </summary>
        [Test]
        public void CalculateSalary_WithCommission_CalculatedCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Salary = 40000,
                Commission = 25
            };
            const double expected = 50000; //40000 * 1,25

            // Act
            var result = employee.CalculateSalary();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Проверяет расчёт зарплаты при максимальном проценте комиссионных
        /// </summary>
        [Test]
        public void CalculateSalary_WithMaxCommission_CalculatedCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee
            {
                Salary = 100000,
                Commission = 100
            };
            const double expected = 100000 * (1 + 100 / 100);

            // Act
            var result = employee.CalculateSalary();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }
        #endregion

        #region Tests for WageEmployee

        /// <summary>
        /// Проверяет установку корректного количества отработанных часов 
        /// в свойство HourCount
        /// </summary>
        [Test]
        public void HourCount_ValidValue_SetsCorrectly()
        {
            // Arrange
            var employee = new WageEmployee();
            const double expectedHours = 80.5;

            // Act
            employee.HourCount = expectedHours;

            // Assert
            ClassicAssert.AreEqual(expectedHours, employee.HourCount);
        }

        /// <summary>
        /// Проверяет, что нулевое количество часов является 
        /// допустимым значением
        /// </summary>
        [Test]
        public void HourCount_ZeroValue_Accepted()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act
            employee.HourCount = 0;

            // Assert
            ClassicAssert.AreEqual(0, employee.HourCount);
        }

        /// <summary>
        /// Проверяет принятие максимального граничного 
        /// значения часов в месяц
        /// </summary>
        [Test]
        public void HourCount_MaxBoundaryValue_Accepted()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act
            employee.HourCount = 115;

            // Assert
            ClassicAssert.AreEqual(115, employee.HourCount);
        }

        /// <summary>
        /// Проверяет, что количество часов свыше лимита вызывает исключение
        /// </summary>
        [Test]
        public void HourCount_ExceedsMaxValue_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.HourCount = 116);
        }

        /// <summary>
        /// Проверяет, что отрицательное количество часов вызывает исключение
        /// </summary>
        [Test]
        public void HourCount_NegativeValue_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.HourCount = -10);
        }

        /// <summary>
        /// Проверяет установку корректной почасовой ставки в свойство Wage
        /// </summary>
        [Test]
        public void Wage_ValidValue_SetsCorrectly()
        {
            // Arrange
            var employee = new WageEmployee();
            const double expectedWage = 750.5;

            // Act
            employee.Wage = expectedWage;

            // Assert
            ClassicAssert.AreEqual(expectedWage, employee.Wage);
        }

        /// <summary>
        /// Проверяет, что нулевая почасовая ставка 
        /// является допустимым значением
        /// </summary>
        [Test]
        public void Wage_ZeroValue_Accepted()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act
            employee.Wage = 0;

            // Assert
            ClassicAssert.AreEqual(0, employee.Wage);
        }

        /// <summary>
        /// Проверяет принятие максимального граничного 
        /// значения почасовой ставки (2000 ₽/час)
        /// </summary>
        [Test]
        public void Wage_MaxBoundaryValue_Accepted()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act
            employee.Wage = 2000;

            // Assert
            ClassicAssert.AreEqual(2000, employee.Wage);
        }

        /// <summary>
        /// Проверяет, что почасовая ставка свыше лимита (2000 ₽) 
        /// вызывает исключение
        /// </summary>
        [Test]
        public void Wage_ExceedsMaxValue_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Wage = 2001);
        }

        /// <summary>
        /// Проверяет, что отрицательная почасовая ставка вызывает исключение
        /// </summary>
        [Test]
        public void Wage_NegativeValue_ThrowsException()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act & Assert
            ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.Wage = -100);
        }

        /// <summary>
        /// Проверяет корректность расчёта почасовой зарплаты
        /// </summary>
        [Test]
        public void CalculateSalary_Hourly_CalculatedCorrectly()
        {
            // Arrange
            var employee = new WageEmployee
            {
                Wage = 500,
                HourCount = 100
            };
            const double expected = 500 * 100;

            // Act
            var result = employee.CalculateSalary();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Проверяет расчёт зарплаты с дробными значениями часов и ставки
        /// </summary>
        [Test]
        public void CalculateSalary_WithFractionalHours_CalculatedCorrectly()
        {
            // Arrange
            var employee = new WageEmployee
            {
                Wage = 350.75,
                HourCount = 40.5
            };
            const double expected = 40.5 * 350.75;

            // Act
            var result = employee.CalculateSalary();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }
        #endregion

        #region Tests for Exception

        /// <summary>
        /// Проверяет, что сообщение исключения IncorrectArgumentException
        /// корректно сохраняется и передаётся через конструктор
        /// </summary>
        [Test]
        public void IncorrectArgumentException_Message_Preserved()
        {
            // Arrange
            const string expectedMessage = "Тестовое сообщение ошибки";

            // Act
            var exception = new IncorrectArgumentException(expectedMessage);

            // Assert
            ClassicAssert.AreEqual(expectedMessage, exception.Message);
        }

        /// <summary>
        /// Проверяет, что при валидации диапазона с переданным кастомным 
        /// сообщением исключение содержит именно это сообщение
        /// </summary>
        [Test]
        public void ValidateRange_CustomMessage_UsedWhenProvided()
        {
            // Arrange
            var employee = new TestEmployee();
            const string customMessage = "Какое-то сообщение";

            // Act & Assert
            var exception = ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.TestValidateRange(
                    5, "test", 10, 20, customMessage));
            ClassicAssert.AreEqual(customMessage, exception.Message);
        }

        /// <summary>
        /// Проверяет, что при валидации диапазона без кастомного сообщения
        /// генерируется сообщение по умолчанию с указанием поля и границ
        /// </summary>
        [Test]
        public void ValidateRange_DefaultMessage_UsedWhenCustomIsNull()
        {
            // Arrange
            var employee = new TestEmployee();

            // Act & Assert
            var exception = ClassicAssert.Throws<IncorrectArgumentException>(
                () => employee.TestValidateRange(5, "testField", 10, 20));
            ClassicAssert.IsTrue(exception.Message.Contains("testField"));
            ClassicAssert.IsTrue(exception.Message.Contains("10"));
            ClassicAssert.IsTrue(exception.Message.Contains("20"));
        }
        #endregion

        #region Tests for Gender

        /// <summary>
        /// Проверяет установку значения мужского пола в свойство Gender
        /// </summary>
        [Test]
        public void Gender_MaleValue_SetsCorrectly()
        {
            // Arrange
            var employee = new SalaryEmployee();

            // Act
            employee.Gender = Gender.Male;

            // Assert
            ClassicAssert.AreEqual(Gender.Male, employee.Gender);
        }

        /// <summary>
        /// Проверяет установку значения женского пола в свойство Gender
        /// </summary>
        [Test]
        public void Gender_FemaleValue_SetsCorrectly()
        {
            // Arrange
            var employee = new WageEmployee();

            // Act
            employee.Gender = Gender.Female;

            // Assert
            ClassicAssert.AreEqual(Gender.Female, employee.Gender);
        }
        #endregion

        #region Tests for Construction

        /// <summary>
        /// Проверяет, что конструктор по умолчанию SalaryEmployee
        /// корректно инициализирует все строковые свойства пустыми строками,
        /// а числовые — нулевыми значениями
        /// </summary>
        [Test]
        public void SalaryEmployee_DefaultConstructor_InitializesProperties()
        {
            // Arrange & Act
            var employee = new SalaryEmployee();

            // Assert
            ClassicAssert.AreEqual(string.Empty, employee.FirstName);
            ClassicAssert.AreEqual(string.Empty, employee.LastName);
            ClassicAssert.AreEqual(string.Empty, employee.Profession);
            ClassicAssert.AreEqual(0, employee.Salary);
            ClassicAssert.AreEqual(0, employee.Commission);
        }

        /// <summary>
        /// Проверяет, что конструктор по умолчанию WageEmployee
        /// корректно инициализирует все строковые свойства пустыми строками,
        /// а числовые — нулевыми значениями
        /// </summary>
        [Test]
        public void WageEmployee_DefaultConstructor_InitializesProperties()
        {
            // Arrange & Act
            var employee = new WageEmployee();

            // Assert
            ClassicAssert.AreEqual(string.Empty, employee.FirstName);
            ClassicAssert.AreEqual(string.Empty, employee.LastName);
            ClassicAssert.AreEqual(string.Empty, employee.Profession);
            ClassicAssert.AreEqual(0, employee.HourCount);
            ClassicAssert.AreEqual(0, employee.Wage);
        }
        #endregion

        #region Tests for protected methods

        /// <summary>
        /// Вспомогательный класс-наследник EmployeBase для тестирования
        /// protected методов базового класса
        /// </summary>
        public class TestEmployee : EmployeBase
        {
            /// <summary>
            /// Абстрактная реализация метода CalculateSalary 
            /// для возможности наследования
            /// </summary>
            /// <returns>Всегда возвращает 0</returns>
            public override double CalculateSalary() => 0;

            /// <summary>
            /// Метод для вызова защищённого метода ValidateRange
            /// из тестов
            /// </summary>
            /// <param name="value">Проверяемое числовое значение</param>
            /// <param name="fieldName">Название поля для 
            /// сообщения об ошибке</param>
            /// <param name="minValue">Минимальная граница диапазона</param>
            /// <param name="maxValue">Максимальная граница диапазона</param>
            /// <param name="customMessage">Кастомное сообщение</param>
            public void TestValidateRange(double value, string fieldName,
                int minValue, int maxValue, string customMessage = null)
            {
                ValidateRange(value, fieldName, minValue,
                    maxValue, customMessage);
            }
        }
        #endregion
    }
}