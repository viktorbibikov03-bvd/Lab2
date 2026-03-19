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
        /// Метод для выполнения действия с повторением при ошибке
        /// </summary>
        /// <param name="action">Действие, которое нужно выполнить</param>
        private static void ActionHandler(Action action)
        {
            while (true)
            {
                try
                {
                    action();
                    return;
                }
                catch (IncorrectArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Повторите ввод");
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
            const int minEmployeeType = 1;
            const int maxEmployeeType = 3;

            int result = 0;

            ActionHandler(() =>
            {
                Console.WriteLine("Выберите тип выплаты:\n" +
                    "1 - почасовая оплата\n" +
                    "2 - оплата по окладу и ставке\n" +
                    "3 - выход из калькулятора заработной платы");

                string inputString = Console.ReadLine();

                if (int.TryParse(inputString, out int inputInt) &&
                    inputInt >= minEmployeeType &&
                    inputInt <= maxEmployeeType)
                {
                    result = inputInt;
                }
                else if (!int.TryParse(inputString, out int _))
                {
                    throw new IncorrectArgumentException("Введите ЧИСЛО!");
                }
                else
                {
                    throw new IncorrectArgumentException($"Число должно " +
                        $"быть в диапозоне от {minEmployeeType} " +
                        $"до {maxEmployeeType}");
                }
            });

            return result;
        }

        /// <summary>
        /// Метод для создания сотрудника с почасовой оплатой
        /// </summary>
        /// <returns>Сотрудник с информацией о почасовой оплате</returns>
        private static WageEmployee CreateWageEmployee()
        {
            var employee = new WageEmployee();

            InputEmployee(employee);

            ActionHandler(() =>
            {
                Console.Write("Введите оплату за 1 час: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double wage))
                {
                    employee.Wage = wage;
                }
                else
                {
                    throw new IncorrectArgumentException
                        ("Необходимо ввести число!");
                }
            });

            ActionHandler(() =>
            {
                Console.Write("Введите количество отработанных часов: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double hourCount))
                {
                    employee.HourCount = hourCount;
                }
                else
                {
                    throw new IncorrectArgumentException
                        ("Необходимо ввести число!");
                }
            });

            return employee;
        }

        /// <summary>
        /// Метод для ввода данных о сотруднике
        /// </summary>
        /// <param name="employee">Сотрудник фирмы</param>
        private static void InputEmployee(EmployeBase employee)
        {
            InputFirstName(employee);
            InputLastName(employee);
            InputGender(employee);
            InputAge(employee);
            employee.Profession = ChooseProfession();
        }

        /// <summary>
        /// Метод для ввода имени сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        private static void InputFirstName(EmployeBase employee)
        {
            ActionHandler(() =>
            {
                Console.Write("Введите имя: ");
                string input = Console.ReadLine();

                if (!CheckNameOrSurname(input))
                {
                    throw new IncorrectArgumentException
                        ("Имя должно состоять из букв одного алфавита!");
                }

                employee.FirstName = input;
            });
        }

        /// <summary>
        /// Метод для ввода фамилии сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        private static void InputLastName(EmployeBase employee)
        {
            ActionHandler(() =>
            {
                Console.Write("Введите фамилию: ");
                string input = Console.ReadLine();

                if (!CheckNameAndSurname(input))
                {
                    throw new IncorrectArgumentException
                        ("Фамилия должна состоять из букв одного алфавита!");
                }

                employee.LastName = input;
            });
        }

        /// <summary>
        /// Метод для ввода пола сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        private static void InputGender(EmployeBase employee)
        {
            ActionHandler(() =>
            {
                Console.Write("Введите пол сотрудника: " +
                    "1 - мужской, 2 - женский: ");
                string numberOfGender = Console.ReadLine();

                switch (numberOfGender)
                {
                    case "1":
                    {
                        employee.Gender = Gender.Male;
                        break;
                    }
                    case "2":
                    {
                        employee.Gender = Gender.Female;
                        break;
                    }
                    default:
                    {
                        //TODO: отступы +
                        throw new IncorrectArgumentException
                            ("Необходимо ввести число 1 или 2!");
                    }
                }
            });
        }

        /// <summary>
        /// Метод для ввода возраста сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        private static void InputAge(EmployeBase employee)
        {
            ActionHandler(() =>
            {
                Console.Write("Введите возраст сотрудника: ");

                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    employee.Age = age;
                }
                else
                {
                    throw new IncorrectArgumentException("При вводе " +
                        "возраста необходимо использовать только числа");
                }
            });
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

            ActionHandler(() =>
            {
                Console.Write("Введите оклад: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double salary))
                {
                    employee.Salary = salary;
                }
                else
                {
                    throw new IncorrectArgumentException
                        ("Необходимо ввести число!");
                }
            });

            ActionHandler(() =>
            {
                Console.Write("Введите ставку премии (%): ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double commission))
                {
                    employee.Commission = commission;
                }
                else
                {
                    throw new IncorrectArgumentException
                        ("Необходимо ввести число!");
                }
            });

            return employee;
        }

        /// <summary>
        /// Выбор профессии для расчета заработной платы
        /// </summary>
        /// <returns>Выбранная профессия</returns>
        private static string ChooseProfession()
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

            string? result = null;

            ActionHandler(() =>
            {
                Console.WriteLine("Введите номер профессии:");

                if (int.TryParse(Console.ReadLine(), out int choiceKey)
                    && professions.ContainsKey(choiceKey))
                {
                    result = professions[choiceKey];
                }
                else
                {
                    throw new IncorrectArgumentException
                        ("Неверный выбор! Повторите ввод!");
                }
            });

            return result;
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
    }
}