namespace Model
{
    /// <summary>
    /// Интерфейс, представляющий сотрудника фирмы
    /// </summary>
    public interface IEmployeable
    {
        /// <summary>
        /// Имя работника
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Фамилия работника
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Возраст работника
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// Пол человека
        /// </summary>
        Gender Gender { get; set; }

        /// <summary>
        /// Профессия человека
        /// </summary>
        string Profession { get; set; }

        /// <summary>
        /// Метод для расчета зарплаты работника
        /// </summary>
        /// <returns>Заработная плата, ₽</returns>
        double CalculateSalary();
    }
}
