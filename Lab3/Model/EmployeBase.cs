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
        /// Пол работника
        /// </summary>
        private Gender _gender;

        /// <summary>
        /// Минимальный возраст приема на работу
        /// </summary>
        private const int MinAge = 18;

        /// <summary>
        /// Максимальный возраст сотрудника мужского пола
        /// </summary>
        private const int MaxAgeMale = 65;

        /// <summary>
        /// Максимальный возраст сотрудника женского пола
        /// </summary>
        private const int MaxAgeFemale = 60;

        /// <summary>
        /// Максимальное количество вводимых символов
        /// </summary>
        private const int MaxLength = 30;

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
                if (string.IsNullOrEmpty(value))
                {
                    throw new IncorrectArgumentException($"Поле \"имя\" " +
                        $"не может быть пустым!");
                }

                if (value.Length > MaxLength)
                {
                    throw new IncorrectArgumentException($" Длина " +
                        $"имени не должна превышать {MaxLength}");
                }

                _firstName = System.Globalization.CultureInfo.CurrentCulture.
                    TextInfo.ToTitleCase(value.ToLower());
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new IncorrectArgumentException
                        ($"Поле \"фамилия\" не может быть пустым!");
                }

                if (value.Length > MaxLength)
                {
                    throw new IncorrectArgumentException($" Длина " +
                        $"фамилии не должна превышать {MaxLength}");
                }

                _lastName = System.Globalization.CultureInfo.CurrentCulture.
                    TextInfo.ToTitleCase(value.ToLower());
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

                if ((((value < MinAge) || (value > MaxAgeMale)) &&
                    (_gender == Gender.Male)) || (((value < MinAge) ||
                    (value > MaxAgeFemale)) && (_gender == Gender.Female)))
                {
                    throw new IncorrectArgumentException($"{nameof(Age)} " +
                        $"должен быть от {MinAge} до {MaxAgeFemale} " +
                        $"для женщин и до {MaxAgeMale} для мужчин!");
                }

                _age = value;
            }
        }

        /// <summary>
        /// Свойство для пола человека
        /// </summary>
        public Gender Gender 
        { 
            get 
            { 
                return _gender; 
            }
            set
            { 
                _gender = value; 
            }
        }

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
                if (string.IsNullOrEmpty(value))
                {
                    throw new IncorrectArgumentException
                        ($"{nameof(Profession)} не может быть пустым!");
                }

                if (value.Length > MaxLength)
                {
                    throw new IncorrectArgumentException($" Длина " +
                        $"{nameof(Profession)} не должна " +
                        $"превышать {MaxLength}");
                }

                _profession = System.Globalization.CultureInfo.
                    CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }

        /// <summary>
        /// Абстрактный метод для расчета зарплаты на основе их должности
        /// </summary>
        /// <returns>Заработная плата, ₽</returns>
        public abstract double CalculateSalary();
    }
}