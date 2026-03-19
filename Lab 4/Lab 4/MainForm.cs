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
        private List<EmployeBase> employees = new List<EmployeBase>();

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
            using var addEmployeeForm = new AddEmployeeForm();
            if (addEmployeeForm.ShowDialog() == DialogResult.OK)
            {
                if (addEmployeeForm.CreatedEmployee != null)
                {
                    employees.Add(addEmployeeForm.CreatedEmployee);
                    RefreshGrid();
                }
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

            int index = DataGridViewWithEmployees.SelectedRows[0].Index;
            if (index >= 0 && index < employees.Count)
            {
                if (MessageBox.Show("Удалить выбранного сотрудника?",
                    "Подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    employees.RemoveAt(index);
                    RefreshGrid();
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
            var searchForm = new SearchForm(employees, this);
            searchForm.Show();
        }

        /// <summary>
        /// Обновляет таблицу сотрудников данными из источника
        /// </summary>
        /// <param name="sourse">Источник данных для таблицы</param>
        public void RefreshGrid(IEnumerable<EmployeBase>? sourse = null)
        {
            var dataSourse = (sourse ?? employees).Select(employee => new
            {
                employee.FirstName,
                employee.LastName,
                employee.Profession,
                employee.Gender,
                employee.Age,
                EmployeeType = employee.GetType().Name,
                Salary = Math.Round(employee.CalculateSalary(), 2)
            }).ToList();

            DataGridViewWithEmployees.DataSource = null;
            DataGridViewWithEmployees.DataSource = dataSourse;
        }

        /// <summary>
        /// Обработчик клика по кнопке "Открыть": загружает список из файла
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void OpenToolStripMenuItem_Click(
            object sender, EventArgs eventArgs)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "VIKTOR files " +
                "(*.viktor)|*.viktor|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Загрузить список сотрудников";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (employees.Count > 0)
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
                    employees = loadedEmployees;
                    RefreshGrid();

                    MessageBox.Show(
                        $"Загружено {employees.Count} сотрудников",
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
            if (employees.Count == 0)
            {
                MessageBox.Show(
                    "Нет данных для сохранения", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var saveFileDialog = new SaveFileDialog();
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
                    EmployeeSerializer.Save(employees, 
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
    }
}
