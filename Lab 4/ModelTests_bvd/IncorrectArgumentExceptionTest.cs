using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using static ModelTests.EmployeBaseTest;

namespace ModelTests;

/// <summary>
/// Класс для проведения тестов класса IncorrectArgumentException
/// </summary>
public class IncorrectArgumentExceptionTest
{
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
        var exception = new Model.IncorrectArgumentException(message);
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
            var exception = Assert.Throws<Model.IncorrectArgumentException>(
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
}
