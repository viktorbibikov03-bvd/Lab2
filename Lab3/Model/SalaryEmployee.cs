using static System.Net.Mime.MediaTypeNames;

namespace Model
{
    /// <summary>
    /// Класс, реализующий метод оплаты по окладу
    /// </summary>
    public class SalaryEmployee : EmployeBase
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
                ValidateStringField(Convert.ToString(value), "оклад");

                ValidateRange(value, "оклад", 0, MaxSalary);

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
                ValidateStringField(Convert.ToString(value), "% по окладу");

                ValidateRange(value, "% по окладу", 
                    MinComission, MaxComission);

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