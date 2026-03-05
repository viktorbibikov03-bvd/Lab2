namespace Model
{
    /// <summary>
    /// Класс, реализующий почасовой метод оплаты
    /// </summary>
    public class WageEmployee : EmployeBase
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
                ValidateStringField(Convert.ToString(value), 
                    "число отработанных часов");

                ValidateRange(value, "число отработанных часов", 0, 
                    MaxHoursInMonth);


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
                ValidateStringField(Convert.ToString(value), 
                    "оплата за час");

                ValidateRange(value, "оплата за 1 час", 0, MaxWageForHour);

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