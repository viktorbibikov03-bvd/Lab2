using Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Lab4
{
    /// <summary>
    /// Форма для добавления нового сотрудника
    /// </summary>
    public partial class AddEmployeeForm : Form
    {
        /// <summary>
        /// Регулярное выражение для проверки 
        /// имени и фамиии на русский алфавит
        /// </summary>
        private Regex _checkingRussian =
            new Regex(@"^[А-Яа-яёЁ]+(\-[А-Яа-яёЁ]+)?$");

        /// <summary>
        /// Регулярное выражение для проверки 
        /// имени и фамиии на английский алфавит
        /// </summary>
        private Regex _checkingEnglish =
            new Regex(@"^[A-Za-z]+(\-[A-Za-z]+)?$");

        /// <summary>
        /// Поле для флага, которое установлено по умолчанию
        /// </summary>
        private FlagLanguage Flag = FlagLanguage.Another;

        /// <summary>
        /// Созданный сотрудник или null, если он не создан
        /// </summary>
        public EmployeBase? CreatedEmployee { get; private set; }

        /// <summary>
        /// Инициализация компонентов формы и настройки событий
        /// </summary>
        public AddEmployeeForm()
        {
            InitializeComponent();
            LoadComboBoxes();
            UpdateParameterLabels();
            RadioButtonWage.CheckedChanged += (sender, evenArgs) =>
                UpdateParameterLabels();
            RadioButtonSalary.CheckedChanged += (sender, evenArgs) =>
                UpdateParameterLabels();

            #if !DEBUG
            ButtonForGeneration.Visible = false;
            #endif
        }

        /// <summary>
        /// Загрузка вариантов во всплывающем окне 
        /// для профессии и пола сотрудника
        /// </summary>
        private void LoadComboBoxes()
        {
            ComboBoxForProfession.Items.AddRange(new object[] {
                "Начальник отдела",
                "Офисный работник",
                "Водитель",
                "Уборщик мусора"
            });

            ComboBoxForGender.Items.AddRange(new object[] {
                Gender.Male,
                Gender.Female
            });
        }

        /// <summary>
        /// Обновление лейблов и их видимости в зависимости 
        /// от выбора типа оплаты
        /// </summary>
        private void UpdateParameterLabels()
        {
            if (RadioButtonWage.Checked)
            {
                LabelForParameter1.Text = "Оплата за 1 час, ₽:";
                LabelForParameter2.Text = "Отработанные часы:";
                LabelForParameter1.Visible = true;
                LabelForParameter2.Visible = true;
                TextBoxForParameter1.Visible = true;
                TextBoxForParameter2.Visible = true;
            }
            else if (RadioButtonSalary.Checked)
            {
                LabelForParameter1.Text = "Оклад, ₽:";
                LabelForParameter2.Text = "Ставка, %:";
                LabelForParameter1.Visible = true;
                LabelForParameter2.Visible = true;
                TextBoxForParameter1.Visible = true;
                TextBoxForParameter2.Visible = true;
            }
            else
            {
                LabelForParameter1.Visible = false;
                LabelForParameter2.Visible = false;
                TextBoxForParameter1.Visible = false;
                TextBoxForParameter2.Visible = false;
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке "Отмена": 
        /// закрывает форму без сохранения
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргумент события</param>
        private void ButtonCancel_Click(object sender, EventArgs eventArgs)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Обработчик клика по кнопке "ОК": 
        /// валидация данных и создание сотрудника
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргумент события</param>
        private void ButtonOk_Click(object sender, EventArgs eventArgs)
        {
            try
            {
                EmployeBase employee;

                if (RadioButtonSalary.Checked)
                {
                    var salaryEmployee = new SalaryEmployee();
                    salaryEmployee.Salary = ParseToDouble(
                        TextBoxForParameter1.Text, "Оклад");
                    salaryEmployee.Commission = ParseToDouble(
                        TextBoxForParameter2.Text, "Ставка");
                    employee = salaryEmployee;
                }
                else
                {
                    var wageEmployee = new WageEmployee();
                    wageEmployee.Wage = ParseToDouble(
                        TextBoxForParameter1.Text, "Оплата за 1 час");
                    wageEmployee.HourCount = ParseToDouble(
                        TextBoxForParameter2.Text, "Отработанные часы");
                    employee = wageEmployee;
                }

                string firstNameString = TextBoxForName.Text;
                if (CheckNameOrSurname(firstNameString))
                {
                    employee.FirstName = firstNameString;
                }
                else
                {
                    throw new IncorrectArgumentException("Имя должно " +
                        "состоять из символов одного алфавита!");
                }

                string lastNameString = TextBoxForSurname.Text;
                if (!CheckNameAndSurname(lastNameString))
                {
                    throw new IncorrectArgumentException("Фамилия должна " +
                        "состоять из символов одного алфавита!");
                }
                else
                {
                    employee.LastName = lastNameString;
                }

                employee.Profession = ComboBoxForProfession.SelectedItem?.
                    ToString() ?? string.Empty;

                string genderString = ComboBoxForGender.SelectedItem?.
                    ToString() ?? string.Empty;
                if (!string.IsNullOrEmpty(genderString) && Enum.TryParse(
                    typeof(Gender), genderString, out object? gender))
                {
                    employee.Gender = (Gender)gender;
                }
                else
                {
                    employee.Gender = Gender.Male;
                    throw new IncorrectArgumentException(
                        "Необходимо выбрать пол сотрудника " +
                        "в выпадающем окне!");
                }

                string ageString = TextBoxForAge.Text;
                if (string.IsNullOrEmpty(ageString) ||
                !int.TryParse(
                    ageString, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out var value))
                {
                    throw new IncorrectArgumentException(
                        $"Возраст должен быть числом!");
                }
                else
                {
                    employee.Age = value;
                }

                CreatedEmployee = employee;
                DialogResult = DialogResult.OK;
            }
            catch (IncorrectArgumentException exception)
            {
                MessageBox.Show(exception.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Парсит строку в double с валидацией
        /// </summary>
        /// <param name="input">Строка для парсинга</param>
        /// <param name="field">Название поля для сообщения об ошибке</param>
        /// <returns>Парсированное значение</returns>
        /// <exception cref="IncorrectArgumentException">Если строка не 
        /// число или отрицательное</exception>
        private double ParseToDouble(string input, string field)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new IncorrectArgumentException(
                    $"Параметр \"{field}\" не может быть пустым!");
            }

            string normalizedInput = input.Replace('.', ',');

            if (!double.TryParse(
                normalizedInput,
                NumberStyles.Any,
                CultureInfo.CurrentCulture,
                out var value))
            {
                throw new IncorrectArgumentException(
                    $"Параметр \"{field}\" должно быть числом!");
            }

            if (value < 0)
            {
                throw new IncorrectArgumentException(
                    $"Параметр \"{field}\" не может быть отрицательным!");
            }

            return value;
        }

        /// <summary>
        /// Проверка имени и фамилии на корректность
        /// </summary>
        /// <param name="nameOrSurname">Имя или фамилия сотрудника</param>
        /// <returns>true - Данные корректны, false - некорерктны</returns>
        public bool CheckNameOrSurname(string nameOrSurname)
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
        public bool CheckNameAndSurname(string nameAndSurname)
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
        /// Обработчик клика по кнопке "Создать случайного сотрудника": 
        /// заполняет поля случайными данными
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void ButtonForGeneration_Click(
            object sender, EventArgs eventArgs)
        {
            try
            {
                var random = new Random();

                var namesRu = new Dictionary<Gender, string[]>
                {
                    { Gender.Male, new[] { "Иван", "Петр", "Виктор" } },
                    { Gender.Female, new[] {"Анна", "Мария", "Надежда" } }
                };

                var surnamesRu = new Dictionary<Gender, string[]>
                {
                    { Gender.Male, new[] { "Иванов", "Петров", "Морозов" } },
                    { Gender.Female, new[] { "Иванова", "Петрова" } }
                };

                string[] professionList =
                {
                    "Начальник отдела",
                    "Офисный работник",
                    "Водитель",
                    "Уборщик мусора"
                };

                string profession = professionList[random.Next(
                    professionList.Length)];

                Gender gender = random.Next(2) == 0 
                    ? Gender.Male 
                    : Gender.Female;

                TextBoxForName.Text = namesRu[gender][random.Next(
                    namesRu[gender].Length)];
                TextBoxForSurname.Text = surnamesRu[gender][random.Next(
                    surnamesRu[gender].Length)];
                ComboBoxForProfession.SelectedItem = profession;
                ComboBoxForGender.SelectedItem = gender;

                int maxAge = gender == Gender.Male 
                    ? 65 
                    : 60;

                int age = random.Next(18, maxAge + 1);

                TextBoxForAge.Text = age.ToString();

                bool useSalary;
                useSalary = random.Next(2) == 0;

                if (useSalary)
                {
                    RadioButtonSalary.Checked = true;

                    double salary = random.Next(0, 200001);

                    salary = Math.Round(salary + random.NextDouble(), 2);

                    TextBoxForParameter1.Text = salary.ToString("F2").
                        Replace(',', '.');

                    double commission = random.Next(0, 101);

                    commission += Math.Round(random.NextDouble(), 2);

                    TextBoxForParameter2.Text = commission.ToString("F2").
                        Replace(',', '.');
                }
                else
                {
                    RadioButtonWage.Checked = true;

                    double hourlyRate = random.Next(0, 2001);

                    hourlyRate = Math.Round(hourlyRate + 
                        random.NextDouble(), 2);

                    TextBoxForParameter1.Text = hourlyRate.ToString("F2").
                        Replace(',', '.');

                    double hours = random.Next(0, 116);

                    hours = Math.Round(hours + random.NextDouble(), 2);

                    TextBoxForParameter2.Text = hours.ToString("F2").
                        Replace(',', '.');
                }

                UpdateParameterLabels();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка при генерации данных: " +
                    $"{exception.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}