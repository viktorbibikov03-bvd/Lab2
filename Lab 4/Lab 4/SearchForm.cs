using Model;
using System.Data;

namespace Lab4
{
    /// <summary>
    /// Форма для поиска сотрудников по критериям
    /// </summary>
    public partial class SearchForm : Form
    {
        /// <summary>
        /// Исходный список сотрудников для поиска
        /// </summary>
        private List<EmployeBase> _sourceEmployees;

        /// <summary>
        /// Ссылка на главную форму
        /// </summary>
        private MainForm _mainForm;

        /// <summary>
        /// Инициализирует форму поиска с исходным списком сотрудников
        /// </summary>
        /// <param name="source">Исходный список 
        /// сотрудников для поиска</param>
        /// <param name="parentForm">Ссылка на главную форму</param>
        public SearchForm(List<EmployeBase> source, MainForm parentForm)
        {
            _sourceEmployees = source;
            _mainForm = parentForm;
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик клика по кнопке "Найти": 
        /// выполняет поиск и обновляет таблицу главной формы
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void ButtonForFind_Click(object sender, EventArgs eventArgs)
        {
            var searchName = TextBoxForSearchName.Text?.Trim();
            var searchSurname = TextBoxForSearchSurname.Text?.Trim();
            var searchProfession = TextBoxForSearchProfession.Text?.Trim();
            const StringComparison stringComparison =
                StringComparison.OrdinalIgnoreCase;

            var filterEmployees = _sourceEmployees.Where(
                employee =>
                    (string.IsNullOrEmpty(searchName)
                    || employee.FirstName.IndexOf(
                        searchName, stringComparison) >= 0)
                    && (string.IsNullOrEmpty(searchSurname)
                    || employee.LastName.IndexOf(
                        searchSurname, stringComparison) >= 0)
                    && (string.IsNullOrEmpty(searchProfession)
                    || employee.Profession.IndexOf(
                        searchProfession, stringComparison) >= 0)).ToList();

            _mainForm.RefreshGrid(filterEmployees);
        }

        /// <summary>
        /// Обработчик клика по кнопке "Сброс": очищает поля и 
        /// показывает полный список
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void ButtonForReset_Click(object sender, EventArgs eventArgs)
        {
            TextBoxForSearchName.Clear();
            TextBoxForSearchSurname.Clear();
            TextBoxForSearchProfession.Clear();
            _mainForm.RefreshGrid(_sourceEmployees);
        }

        /// <summary>
        /// Обработчик клика по кнопке "Отмена": закрывает форму
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="eventArgs">Аргументы события</param>
        private void ButtonForCancel_Click
            (object sender, EventArgs eventArgs)
        {
            Close();
        }
    }
}