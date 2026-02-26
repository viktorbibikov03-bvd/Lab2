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

        //TODO: RSDN
        /// <summary>
        /// Поле для флага, которое установлено по умолчанию
        /// </summary>
        static FlagLanguage Flag = FlagLanguage.Another;

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

                switch (SalaryType())
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
                            //TODO: remove
                        break;
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
            if ((_checkingRussian.IsMatch(nameAndSurname) 
                    && Flag != FlagLanguage.Russian) 
                || (_checkingEnglish.IsMatch(nameAndSurname) 
                    && Flag != FlagLanguage.English))
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
        /// //TODO: rename
        public static int SalaryType()
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
        private static WageRate CreateWageEmployee()
        { 
            var employee = new WageRate();

            InputEmployee(employee);

            ValidationOfSpecialInput("Оплата за 1 час",
                input => employee.Wage = Convert.ToDouble(input),
                "Необходимо ввести число!");

            ValidationOfSpecialInput("Количество отработанных часов", 
                input => employee.HourCount = Convert.ToDouble(input),
                "Необходимо ввести число!");

            return employee;
        }
        
        /// <summary>
        /// Метод для ввода данных о сотруднике
        /// </summary>
        /// <param name="employee">Сотрудник фирмы</param>
        private static void InputEmployee(EmployeBase employee)
        {
            InputNameOrSurname(employee, "Имя");
            InputNameOrSurname(employee, "Фамилия");
            InputGender(employee);
            InputAge(employee);

            employee.Profession = ChoosenProfession();
        }

        /// <summary>
        /// Метод для создания сотрудника с оплатой по окладу и ставке
        /// </summary>
        /// <returns>Сотрудник, оплата которого осуществляется
        /// по окладу и ставке</returns>
        private static SalaryRate CreateSalaryEmployee()
        {
            var employee = new SalaryRate();

            InputEmployee(employee);

            ValidationOfSpecialInput("Оклад", 
                input => employee.Salary = Convert.ToDouble(input), 
                "Значение должно быть числом");

            ValidationOfSpecialInput("Ставка премии (%)", 
                input => employee.Commission = Convert.ToDouble(input), 
                "Значение должно быть числом");

            return employee;
        }

        /// <summary>
        /// Метод для ввода имени или фамилии с валидацией
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <param name="nameOrSurname">Слово "Имя" или "Фамилия"</param>
        /// <exception cref="IncorrectArgumentException">Сообщение об 
        /// исключениях</exception>
        private static void InputNameOrSurname
            (EmployeBase employee, string nameOrSurname)
        {
            while (true)
            {
                Console.WriteLine($"Введите " +
                    $"{nameOrSurname.ToLower()} сотрудника");
                
                string inputString = Console.ReadLine();

                try
                {

                    if (nameOrSurname == "Имя")
                    {
                        if (!CheckNameOrSurname(inputString))
                        {
                            throw new IncorrectArgumentException
                                ($"{nameOrSurname} должно состоять " +
                                $"из букв одного алфавита!");
                        }
                        else
                        {
                            employee.FirstName = inputString;
                        }
                    }
                    else
                    {
                        if (!CheckNameAndSurname(inputString))
                        {
                            throw new IncorrectArgumentException
                                ($"{nameOrSurname} должна быть из " +
                                $"символов того же алфавита, что и имя");
                        }
                        else
                        {
                            employee.LastName = inputString;
                        }
                    }

                    return;
                }
                catch (IncorrectArgumentException exception)
                {
                    Console.WriteLine($"{exception.Message}\n" +
                        $"Повторите ввод!");
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

        /// <summary>
        /// Метод для ввода значений с последующей валидацией
        /// </summary>
        /// <param name="specialInput">Информация о том, 
        /// что необходимо ввести пользователю</param>
        /// <param name="action">Поле, которому будет присвоено 
        /// значение в результате валидации</param>
        /// <param name="exception">Сообщение с исключением, 
        /// на которое будет осуществлена проверка</param>
        private static void ValidationOfSpecialInput
            (string specialInput, Action<string> action, string exception)
        {
            while (true)
            {
                Console.Write($"Введите {specialInput.ToLower()}: ");

                string input = Console.ReadLine();

                try
                {
                    action(input);
                    return;
                }
                catch(FormatException) 
                {
                    Console.WriteLine(exception);
                    Console.WriteLine("Повторите ввод");
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
            Console.WriteLine();
            Console.WriteLine($"Зарплата {employee.FirstName} " +
                $"{employee.LastName} в возрасте {employee.Age} лет," +
                $" имеющего профессию \"{employee.Profession}\", " +
                $"составляет {Math.Round(employee.CalculateSalary(), 1)} Р");
            Console.WriteLine();
        }

        /// <summary>
        /// Метод для ввода и валидации пола сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <exception cref="IncorrectArgumentException">Сообщение 
        /// о неверном вводе информации пользователем</exception>
        private static void InputGender(EmployeBase employee)
        {
            Console.Write("1 - мужской, 2 - женский: ");
            //TODO: duplication
            while (true)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), 
                        out int numberOfGender))
                    {
                        employee.Gender = numberOfGender == 1
                            ? Gender.Male
                            : numberOfGender == 2
                                ? Gender.Female
                                //TODO: duplication
                                : throw new IncorrectArgumentException
                                ("Введите числа 1 - мужской пол, " +
                                "2 - женский");
                    }
                    else
                    {
                        //TODO: duplication
                        throw new IncorrectArgumentException
                            ("Введите числа 1 - мужской пол, 2 - женский");
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

        /// <summary>
        /// Метод для ввода и валидации возраста сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        private static void InputAge(EmployeBase employee)
        { 
            //TODO: duplication
            while(true)
            { 
                Console.Write("Введите возраст сотрудника:");

                try
                {
                    if (int.TryParse(Console.ReadLine(), out int age))
                    {
                        employee.Age = age;
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