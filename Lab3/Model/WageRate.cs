namespace Model
{
    /// <summary>
    /// Класс, реализующий почасовой метод оплаты
    /// </summary>
    /// //TODO: rename
    public class WageRate : EmployeBase
    {
        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        private double _hourCount;

        /// <summary>
        /// Заработная плата за 1 отработанный час
        /// </summary>
        private double _wage;

        /// <summary>
        /// Максимальное число часов, отработанных в месяц согласно ТК РФ
        /// </summary>
        private const int MaxHoursInMonth = 115;

        /// <summary>
        /// Максимальная зарплата, которую может выплатить фирма
        /// за отработанный час
        /// </summary>
        private const int MaxWageForHour = 2000;

        /// <summary>
        /// Свойство для числа отработанных часов
        /// </summary>
        public double HourCount
        {
            get
            {
                return _hourCount;
            }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                { 
                    throw new IncorrectArgumentException
                        ($"Поле {nameof(HourCount)} должно быть заполнено!");
                }

                if (value < 0 || value > MaxHoursInMonth)
                {
                    throw new IncorrectArgumentException($"Количество " +
                        $"отработанных часов должно быть в пределах " +
                        $"от 0 до {MaxHoursInMonth} согласно ТК РФ");
                }

                _hourCount = value;
            }
        }

        /// <summary>
        /// Свойство для заработной платы за 1 отработанный час
        /// </summary>
        public double Wage
        {
            get
            {
                return _wage;
            }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new IncorrectArgumentException
                        ($"Поле {nameof(Wage)} должно быть заполнено!");
                }

                if (value <= 0 || value > MaxWageForHour)
                {
                    throw new IncorrectArgumentException($"Заниматься " +
                        $"расточительством запрещено!\nСумма, " +
                        $"которую фирма может платить сотруднику в час," +
                        $"лежит в пределах от 0 до {MaxWageForHour}");
                }

                _wage = value;
            }
        }

        /// <summary>
        /// ереопределенный метод из интерфейса базового класса 
        /// для рассчета почасовой оплаты
        /// </summary>
        /// <returns>Заработная плата по отработанным часам, ₽</returns>
        public override double CalculateSalary()
        {
            return Wage * HourCount;
        }
    }
}