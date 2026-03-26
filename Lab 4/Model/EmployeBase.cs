namespace Model
{
    /// <summary>
    /// Базовый класс сотрудника
    /// </summary>
    public abstract class EmployeBase : IEmployeable
    {
        /// <summary>
        /// Имя работника
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Фамилия работника
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        private int _age;

        /// <summary>
        /// Профессия работника
        /// </summary>
        private string _profession;

        /// <summary>
        /// Максимальное количество вводимых символов
        /// </summary>
        private const int MaxLength = 30;

        /// <summary>
        /// Минимальный возраст приема на работу
        /// </summary>
        private const int MinAge = 18;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public EmployeBase()
        {
            _firstName = string.Empty;
            _lastName = string.Empty;
            _profession = string.Empty;
        }

        /// <summary>
        /// Валидация строкового поля
        /// </summary>
        /// <param name="value">Значение для проверки</param>
        /// <param name="fieldName">Название поля</param>
        /// <exception cref="IncorrectArgumentException">
        /// Ошибка валидации</exception>
        protected void ValidateStringField
            (string value, string fieldName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new IncorrectArgumentException
                    ($"Поле \"{fieldName}\" не может быть пустым!");
            }

            if (value.Length > MaxLength)
            {
                throw new IncorrectArgumentException
                    ($"Длина поля \"{fieldName}\" не должна " +
                    $"превышать {MaxLength} символов");
            }
        }

        /// <summary>
        /// Приведение строки к правильному формату 
        /// (первая буква заглавная, остальные строчные)
        /// </summary>
        /// <param name="value">Строка для форматирования</param>
        /// <returns>Отформатированная строка</returns>
        private string FormatString(string value)
        {
            return System.Globalization.CultureInfo.CurrentCulture
                .TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>
        /// Проверка корректности ввода имени
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                ValidateStringField(value, "имя");
                _firstName = FormatString(value);
            }
        }

        /// <summary>
        /// Проверка корректности ввода фамилии
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                ValidateStringField(value, "фамилия");
                _lastName = FormatString(value);
            }
        }

        /// <summary>
        /// Проверка корректности ввода возраста
        /// </summary>
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new IncorrectArgumentException("Введите возраст!");
                }

                var retirementAge = new Dictionary<Gender, int>
                {
                    { Gender.Male, 65 },
                    { Gender.Female, 60 }
                };

                ValidateRange(value, "возраст", MinAge,
                    retirementAge[Gender]);

                _age = value;
            }
        }

        /// <summary>
        /// Свойство для пола человека
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Проверка корректности ввода профессии
        /// </summary>
        public string Profession
        {
            get
            {
                return _profession;
            }
            set
            {
                ValidateStringField(value, nameof(Profession).ToLower());
                _profession = FormatString(value);
            }
        }

        /// <summary>
        /// Абстрактный метод для расчета зарплаты на основе их должности
        /// </summary>
        /// <returns>Заработная плата, ₽</returns>
        public abstract double CalculateSalary();

        /// <summary>
        /// Метод для валидации при диапазонах
        /// </summary>
        /// <param name="value">Число, которое обрабатывается</param>
        /// <param name="fieldName">Название обрадатываемого значения</param>
        /// <param name="minValue">Минимальное число в диапозоне</param>
        /// <param name="maxValue">Максимальное число в диапозоне</param>
        /// <param name="customMessage">Сообщение исключения</param>
        /// <exception cref="IncorrectArgumentException">Ошибки при валидации
        /// </exception>
        protected void ValidateRange(double value, string fieldName,
            int minValue, int maxValue, string customMessage = null)
        {
            if (value < minValue || value > maxValue)
            {
                string message = customMessage ??
                    $"Значение поля \"{fieldName}\" должно быть в " +
                    $"диапазоне от {minValue} до {maxValue}";
                throw new IncorrectArgumentException(message);
            }
        }
    }
}