using Model;

/// <summary>
/// Вспомогательный класс для отображения данных в таблице
/// </summary>
public class EmployeeDisplayData
{
    //TODO: XML
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Profession { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public string EmployeeType { get; set; } = string.Empty;
    public double Salary { get; set; }

    /// <summary>
    /// Ссылка на оригинальный объект сотрудника для операции удаления
    /// </summary>
    public EmployeBase? OriginalEmployee { get; set; }
}
