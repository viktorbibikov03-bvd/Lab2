using Model;

/// <summary>
/// Вспомогательный класс для отображения данных в таблице
/// </summary>
public class EmployeeDisplayData
{
    /// <summary>
    /// Свойство для имени сотрудника
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Свойство для фамилии сотрудника
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Свойство для профессии сотрудника
    /// </summary>
    public string Profession { get; set; } = string.Empty;

    /// <summary>
    /// Свойство для пола сотрудника
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Свойтсво для возраста сотрудника
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Свойтсво для типа оплаты
    /// </summary>
    public string EmployeeType { get; set; } = string.Empty;

    /// <summary>
    /// Свойства для рассчитанной зарплаты сотрудника
    /// </summary>
    public double Salary { get; set; }

    /// <summary>
    /// Ссылка на оригинальный объект сотрудника для операции удаления
    /// </summary>
    public EmployeBase? OriginalEmployee { get; set; }
}
