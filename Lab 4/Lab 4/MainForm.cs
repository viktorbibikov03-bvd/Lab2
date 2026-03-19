using Model;

namespace Lab4
{
    /// <summary>
    /// Главная форма приложения для управления списком сотрудников
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Список сотрудников
        /// </summary>
        private List<EmployeBase> _employees = new List<EmployeBase>();

        /// <summary>
        /// Текущий отображаемый список после фильтрации
        /// </summary>
        private List<EmployeBase> _currentDisplayList;

        /// <summary>
        /// Инициализирует главную форму
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeGridColumns();
            RefreshGrid();
        }

        /// <summary>
        /// Инициализирует столбцы в таблице сотрудников
        /// </summary>
        private void InitializeGridColumns()
        {
            DataGridViewWithEmployees.Columns.Clear();
            DataGridViewWithEmployees.AutoGenerateColumns = false;

            var columnDefinitions = new (string Header, string Property)[]
            {
                ("Имя", "FirstName"),
                ("Фамилия", "LastName"),
                ("Профессия", "Profession"),
                ("Пол", "Gender"),
                ("Возраст", "Age"),
                ("Тип оплаты", "EmployeeType"),
                ("Зарплата, ₽", "Salary")
            };

            foreach (var column in columnDefinitions)
            {
                DataGridViewWithEmployees.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = column.Header,
                        DataPropertyName = column.Property
                    });
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке "Добавить": 
        /// открывает форму добавления сотрудника
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void AddButton_Click(object sender, EventArgs eventArgs)
        {
            var addEmployeeForm = new AddEmployeeForm();
            if (addEmployeeForm.ShowDialog() == DialogResult.OK && 
                addEmployeeForm.CreatedEmployee != null)
            {
                _employees.Add(addEmployeeForm.CreatedEmployee);
                RefreshGrid();
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке "Удалить":
        /// удаляет выбранного сотрудника
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void ButtonForDelete_Click(
            object sender, EventArgs eventArgs)
        {
            if (DataGridViewWithEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для удаления!", "Инфо",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var employeesToRemove = new List<(EmployeBase Employee, 
                EmployeeDisplayData DisplayData)>();

            foreach (DataGridViewRow row in 
                DataGridViewWithEmployees.SelectedRows)
            {
                if (row.DataBoundItem is EmployeeDisplayData displayData)
                {
                    var employee = _employees.FirstOrDefault(employee =>
                        employee.FirstName == displayData.FirstName &&
                        employee.LastName == displayData.LastName &&
                        employee.Profession == displayData.Profession &&
                        employee.Age == displayData.Age);

                    if (employee != null)
                    {
                        employeesToRemove.Add((employee, displayData));
                    }
                }
            }

            if (employeesToRemove.Count == 0)
            {
                MessageBox.Show("Не удалось определить" +
                    " выбранных сотрудников!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string confirmationMessage = employeesToRemove.Count == 1
                ? $"Удалить сотрудника: " +
                    $"{employeesToRemove[0].DisplayData.LastName} " +
                    $"{employeesToRemove[0].DisplayData.FirstName}?"
                : $"Удалить выбранных сотрудников " +
                    $"({employeesToRemove.Count} шт.)?";

            if (MessageBox.Show(confirmationMessage, "Подтверждение",
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in employeesToRemove)
                {
                    _employees.Remove(item.Employee);
                }

                if (_currentDisplayList != null && 
                    _currentDisplayList != _employees)
                {
                    foreach (var item in employeesToRemove)
                    {
                        _currentDisplayList.Remove(item.Employee);
                    }
                }

                RefreshGrid(_currentDisplayList ?? _employees);
                if (employeesToRemove.Count == 1)
                {
                    MessageBox.Show(
                    $"Успешно удален сотрудник " +
                    $"\"{employeesToRemove[0].DisplayData.LastName} " +
                    $"{employeesToRemove[0].DisplayData.FirstName}\"",
                    "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    MessageBox.Show(
                       $"Сотрудников удалено: {employeesToRemove.Count} ",
                       "Информация",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Отработчик клика по кнопке "Поиск": открывает форму поиска
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void ButtonForSearch_Click(
            object sender, EventArgs eventArgs)
        {
            var searchForm = new SearchForm(_employees, (filtered) =>
            {
                _currentDisplayList = filtered;
                RefreshGrid(filtered);
            });
            searchForm.Show();
        }


        //TODO: нарушение инкапсуляции +
        /// <summary>
        /// Обновляет таблицу сотрудников данными из источника
        /// </summary>
        /// <param name="source">Источник данных для таблицы</param>
        private void RefreshGrid(IEnumerable<EmployeBase>? source = null)
        {
            var dataSource = (source ?? _employees).Select(
                employee => new EmployeeDisplayData
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Profession = employee.Profession,
                Gender = employee.Gender,
                Age = employee.Age,
                EmployeeType = GetEmployeeTypeRu(employee),
                Salary = Math.Round(employee.CalculateSalary(), 2),
                OriginalEmployee = employee
            }).ToList();

            DataGridViewWithEmployees.DataSource = null;
            DataGridViewWithEmployees.DataSource = dataSource;
        }

        /// <summary>
        /// Обработчик клика по кнопке "Открыть": загружает список из файла
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void OpenToolStripMenuItem_Click(
            object sender, EventArgs eventArgs)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "VIKTOR files " +
                "(*.viktor)|*.viktor|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Загрузить список сотрудников";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (_employees.Count > 0)
                    {
                        var result = MessageBox.Show(
                            "Текущий список сотрудников будет заменен. " +
                            "Продолжить?", "Подтверждение",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }

                    var loadedEmployees = EmployeeSerializer.Load(
                        openFileDialog.FileName);
                    _employees = loadedEmployees;
                    RefreshGrid();

                    MessageBox.Show(
                        $"Загружено {_employees.Count} сотрудников",
                        "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(
                        $"Ошибка при загрузке файла: {exception.Message}",
                        "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке "Сохранить как...": 
        /// сохраняет список в файл
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void SaveToolStripMenuItem_Click(
            object sender, EventArgs eventArgs)
        {
            if (_employees.Count == 0)
            {
                MessageBox.Show(
                    "Нет данных для сохранения", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "VIKTOR files " +
                "(*.viktor)|*.viktor|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Сохранить список сотрудников";
            saveFileDialog.DefaultExt = "viktor";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = $"employees_" +
                $"{DateTime.Now:dd.MM.yyyy_HH.mm.ss}.viktor";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    EmployeeSerializer.Save(_employees, 
                        saveFileDialog.FileName);

                    MessageBox.Show(
                        $"Список сотрудников успешно сохранен в файл:" +
                        $"\n{saveFileDialog.FileName}", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(
                        $"Ошибка при сохранении файла: {exception.Message}",
                        "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Метод для вывода русских типов оплаты
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns>Строка с типом оплаты на русском</returns>
        private string GetEmployeeTypeRu(EmployeBase employee)
        {
            return employee switch
            {
                SalaryEmployee => "Оклад + процент",
                WageEmployee => "Почасовая оплата",
                _ => employee.GetType().Name
            };
        }
    }
}