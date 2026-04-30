using Model;

namespace ModelTests;
public class SalaryEmployeeTest
{
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
        Assert.Throws<Model.IncorrectArgumentException>(
            () => employee.Salary = salary);
    }

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
        Assert.Throws<Model.IncorrectArgumentException>(
            () => employee.Commission = commission);
    }

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
}