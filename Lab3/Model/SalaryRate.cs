namespace Model
{
    /// <summary>
    /// Класс, реализующий метод оплаты по окладу
    /// </summary>
    public class SalaryRate : EmployeBase
    {
        /// <summary>
        /// Фиксированный оклад
        /// </summary>
        private double _salary;

        /// <summary>
        /// Процент комиссионных
        /// </summary>
        private double _commissionСharge;

        /// <summary>
        /// Минимальный процент комиссионных
        /// </summary>
        private const int MinComission = 0;

        /// <summary>
        /// Максимальный процент комиссионных
        /// </summary>
        private const int MaxComission = 100;

        /// <summary>
        /// Максимальная сумма оклада
        /// </summary>
        private const int MaxSalary = 200000;

        /// <summary>
        /// Свойство для поля оклада
        /// </summary>
        public double Salary
        { 
            get 
            { 
                return _salary; 
            }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new IncorrectArgumentException($"Поле " +
                        $"{nameof(Salary)} должно быть заполнено!");
                }

                if (value <= 0 || value > MaxSalary)
                {
                    throw new IncorrectArgumentException($"Оклад должен " +
                        $"быть больше 0, но не превышать сумму " +
                        $"максимального оклада: {MaxSalary}");
                }

                _salary = value;
            }
        }

        /// <summary>
        /// Свойство для поля процента комиссионных
        /// </summary>
        public double Commission
        {
            get
            {
                return _commissionСharge;
            }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new IncorrectArgumentException($"Поле " +
                        $"{nameof(Commission)} должно быть заполнено!");
                }

                if ((value < MinComission) || (value > MaxComission))
                {
                    throw new IncorrectArgumentException($"Процент " +
                        $"комиссионных должен лежать от {MinComission} " +
                        $"до {MaxComission}");
                }

                _commissionСharge = value;
            }
        }

        /// <summary>
        /// Переопределенный метод из интерфейса базового класса 
        /// для расчета заработной платы
        /// </summary>
        /// <returns>Заработная плата по окладу, ₽</returns>
        public override double CalculateSalary()
        {
            return Salary * (1 + Commission / 100);
        }
    }
}