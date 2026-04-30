using Model;

namespace ModelTests;

public class WageEmployeeTest
{
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
        Assert.Throws<Model.IncorrectArgumentException>(
            () => employee.HourCount = hours);
    }

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
        Assert.Throws<Model.IncorrectArgumentException>(
            () => employee.Wage = wage);
    }

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
}
