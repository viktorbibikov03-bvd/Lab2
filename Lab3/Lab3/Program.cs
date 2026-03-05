using System.Text.RegularExpressions;

namespace Model 
{
    /// <summary>
    /// Основной класс программы для расчета зарплат работников фирмы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Регулярное выражение для проверки 
        /// имени и фамиии на русский алфавит
        /// </summary>
        private static Regex _checkingRussian = 
            new Regex(@"^[А-Яа-яёЁ]+(\-[А-Яа-яёЁ]+)?$");

        /// <summary>
        /// Регулярное выражение для проверки 
        /// имени и фамиии на английский алфавит
        /// </summary>
        private static Regex _checkingEnglish = 
            new Regex(@"^[A-Za-z]+(\-[A-Za-z]+)?$");

        /// <summary>
        /// Поле для флага, которое установлено по умолчанию
        /// </summary>
        private static FlagLanguage Flag = FlagLanguage.Another;

        /// <summary>
        /// Точка входа в программу
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Расчет заработной платы сотрудников фирмы");

            var employeeList = new List<IEmployeable>();

            while (true)
            {
                IEmployeable employee;

                switch (GetSalaryType())
                {
                    case 1:
                    {
                        employee = CreateWageEmployee();
                        employeeList.Add(employee);
                        OutputSalary(employee);
                        break;
                    }
                    case 2:
                    {
                        employee = CreateSalaryEmployee();
                        employeeList.Add(employee);
                        OutputSalary(employee);
                        break;
                    }
                    case 3:
                    {
                        return;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Проверка имени и фамилии на корректность
        /// </summary>
        /// <param name="nameOrSurname">Имя или фамилия сотрудника</param>
        /// <returns>true - Данные корректны, false - некорерктны</returns>
        private static bool CheckNameOrSurname(string nameOrSurname)
        {
            if (_checkingRussian.IsMatch(nameOrSurname))
            {
                Flag = FlagLanguage.Russian;
            }

            if (_checkingEnglish.IsMatch(nameOrSurname))
            { 
                Flag = FlagLanguage.English;
            }

            return _checkingRussian.IsMatch(nameOrSurname) || 
                _checkingEnglish.IsMatch(nameOrSurname);
        }

        /// <summary>
        /// Проверка имени и фамилии на идентичность языка
        /// </summary>
        /// <param name="nameAndSurname">Имя и фамилия пользователя</param>
        /// <returns>true - Данные корректны, false - некорерктны</returns>
        /// <exception cref="IncorrectArgumentException">Имя и фамилия должны
        /// быть написаны символами одного языка!</exception>
        private static bool CheckNameAndSurname(string nameAndSurname)
        {
            if (((_checkingRussian.IsMatch(nameAndSurname)) &&
                (Flag != FlagLanguage.Russian)) ||
                ((_checkingEnglish.IsMatch(nameAndSurname)) &&
                (Flag != FlagLanguage.English)))
            {
                throw new IncorrectArgumentException("Имя и фамилия " +
                    "должны быть на одном языке!");
            }

            return _checkingEnglish.IsMatch(nameAndSurname) || 
                _checkingRussian.IsMatch(nameAndSurname);
        }

        /// <summary>
        /// Метод для обработки пользовательского ввода 
        /// при выборе типа оплаты
        /// </summary>
        /// <returns>Код типа оплаты в зависимости 
        /// от введенного числа</returns>
        public static int GetSalaryType()
        {
            const int MinEmployeeType = 1;
            const int MaxEmployeeType = 3;
            while (true)
            {
                Console.WriteLine("Выберите тип выплаты:\n" +
                    "1 - почасовая оплата\n2 - оплата по окладу и ставке\n" +
                    "3 - выход из калькулятора заработной платы");

                string inputString = Console.ReadLine();

                if (int.TryParse(inputString, out int inputInt) &&
                    inputInt >= MinEmployeeType &&
                    inputInt <= MaxEmployeeType)
                {
                    return inputInt;
                }

                if (!int.TryParse(inputString, out int _))
                {
                    Console.WriteLine("Введите ЧИСЛО!");
                }
                else
                {
                    Console.WriteLine($"Число должно быть в диапозоне " +
                        $"от {MinEmployeeType} до {MaxEmployeeType}");
                }
            }
        }

        /// <summary>
        /// Метод для создания сотрудника с почасовой оплатой
        /// </summary>
        /// <returns>Сотрудник с информацией о почасовой оплате</returns>
        private static WageEmployee CreateWageEmployee()
        { 
            var employee = new WageEmployee();

            InputEmployee(employee);

            ValidationOfSpecialInput("Оплата за 1 час",
                input => employee.Wage = Convert.ToDouble(input));

            ValidationOfSpecialInput("Количество отработанных часов", 
                input => employee.HourCount = Convert.ToDouble(input));

            return employee;
        }
        
        /// <summary>
        /// Метод для ввода данных о сотруднике
        /// </summary>
        /// <param name="employee">Сотрудник фирмы</param>
        private static void InputEmployee(EmployeBase employee)
        {
            InputNameOrSurname("Имя", inputName => 
            employee.FirstName = inputName);
            InputNameOrSurname("Фамилия", inputSurname => 
            employee.LastName = inputSurname);
            InputGender("пол сотрудника", inputGender => 
            employee.Gender = inputGender);
            InputAge("возраст", inputAge => employee.Age = inputAge);

            employee.Profession = ChoosenProfession();
        }

        /// <summary>
        /// Метод для создания сотрудника с оплатой по окладу и ставке
        /// </summary>
        /// <returns>Сотрудник, оплата которого осуществляется
        /// по окладу и ставке</returns>
        private static SalaryEmployee CreateSalaryEmployee()
        {
            var employee = new SalaryEmployee();

            InputEmployee(employee);

            ValidationOfSpecialInput("Оклад", 
                input => employee.Salary = Convert.ToDouble(input));

            ValidationOfSpecialInput("Ставка премии (%)", 
                input => employee.Commission = Convert.ToDouble(input));

            return employee;
        }

        //TODO: duplication
        /// <summary>
        /// Метод для ввода имени или фамилии с валидацией
        /// </summary>
        /// <param name="fieldName">Слово "Имя" или "Фамилия"</param>
        /// <param name="action">Поле, которому будет присвоено 
        /// значение в результате валидации</param>
        /// <exception cref="IncorrectArgumentException">Сообщение 
        /// об исключениях</exception>
        private static void InputNameOrSurname(string fieldName, 
            Action<string> action)
        {
            while (true)
            {
                Console.Write($"Введите {fieldName.ToLower()}: ");
                string input = Console.ReadLine();

                try
                {
                    if (fieldName == "Имя")
                    {
                        if (!CheckNameOrSurname(input))
                        {
                            throw new IncorrectArgumentException
                                ($"{fieldName} должно состоять из букв " +
                                $"одного алфавита!");
                        }
                    }
                    else
                    {
                        if (!CheckNameAndSurname(input))
                        {
                            throw new IncorrectArgumentException
                                ($"{fieldName} должна состоять из букв " +
                                $"одного алфавита!");
                        }
                    }

                    action(input);
                    return;
                }
                catch (IncorrectArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Повторите ввод");
                }
            }
        }

        /// <summary>
        /// Выбор профессии для расчета заработной платы
        /// </summary>
        /// <returns>Выбранная профессия</returns>
        private static string ChoosenProfession()
        {
            var professions = new Dictionary<int, string>
            {
                {1, "Начальник отдела"},
                {2, "Офисный работник"},
                {3, "Водитель"},
                {4, "Уборщик мусора"}
            };

            Console.WriteLine("Выберите должность для рассчета зарплаты:");

            foreach (var profession in professions)
            {
                Console.WriteLine($"{profession.Key} - {profession.Value}");
            }

            while (true)
            {
                Console.WriteLine("Введите номер профессии:");

                if (int.TryParse(Console.ReadLine(), out int choiceKey)
                    && professions.ContainsKey(choiceKey))
                {
                    return professions[choiceKey];
                }
                else 
                {
                    Console.WriteLine("Неверный выбор! Повторите ввод!");
                }
            }
        }

        //TODO: duplication
        /// <summary>
        /// Метод для ввода значений с последующей валидацией
        /// </summary>
        /// <param name="specialInput">Информация о том, 
        /// что необходимо ввести пользователю</param>
        /// <param name="action">Поле, которому будет присвоено 
        /// значение в результате валидации</param>
        private static void ValidationOfSpecialInput
            (string specialInput, Action<string> action)
        {
            while (true)
            {
                Console.Write($"Введите {specialInput.ToLower()}: ");
                string input = Console.ReadLine();

                try
                {
                    if (double.TryParse(input, out double inputInt))
                    {
                        action(input);
                        return;
                    }
                    else
                    { 
                        throw new IncorrectArgumentException
                            ("Необходимо ввести число!");
                    }
                }
                catch (IncorrectArgumentException exceptionMessage)
                {
                    Console.WriteLine(exceptionMessage.Message);
                    Console.WriteLine("Повторите ввод");
                }
            }
        }

        /// <summary>
        /// Вывод сообщения о сотруднике и его заработной плате
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        private static void OutputSalary(IEmployeable employee)
        {
            Console.WriteLine($"\nЗарплата {employee.FirstName} " +
                $"{employee.LastName} в возрасте {employee.Age} лет," +
                $" имеющего профессию \"{employee.Profession}\", " +
                $"составляет {Math.Round(employee.CalculateSalary(), 1)} Р");
        }

        //TODO: duplication
        /// <summary>
        /// Метод для ввода пола сотрудника
        /// </summary>
        /// <param name="fieldGender">Информация о том, 
        /// что необходимо ввести пользователю</param>
        /// <param name="action">Поле, которому будет присвоено 
        /// значение в результате валидации</param>
        /// <exception cref="IncorrectArgumentException">Сообщение
        /// об исключениях</exception>
        private static void InputGender(string fieldGender, 
            Action<Gender> action)
        {
            Console.Write($"Введите {fieldGender}: " +
                $"1 - мужской, 2 - женский: ");

            while (true)
            {
                try
                {
                    string numberOfGender = Console.ReadLine();

                    switch (numberOfGender)
                    {
                        case "1":
                        {
                            action(Gender.Male);
                            break;
                        }
                        case "2":
                        {
                            action(Gender.Female);
                            break;
                        }
                        default:
                        {
                            throw new IncorrectArgumentException
                                    ("Необходимо ввести число 1 или 2!");
                        }
                    }

                    return;
                }
                catch (IncorrectArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Повторите ввод");
                }
            }
        }

        //TODO: duplication
        /// <summary>
        /// Метод для ввода возраста сотрудника
        /// </summary>
        /// <param name="fieldAge">Информация о том, 
        /// что необходимо ввести пользователю</param>
        /// <param name="action">Поле, которому будет присвоено 
        /// значение в результате валидации</param>
        private static void InputAge(string fieldAge, Action<int> action)
        { 
            while(true)
            { 
                Console.Write($"Введите {fieldAge} сотрудника:");

                try
                {
                    if (int.TryParse(Console.ReadLine(), out int age))
                    {
                        action(age);
                    }
                    else
                    {
                        throw new IncorrectArgumentException("При вводе " +
                            "возраста необходимо использовать только числа");
                    }

                    return;
                }
                catch (IncorrectArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Повторите ввод");
                }
            }
        }
    }
}