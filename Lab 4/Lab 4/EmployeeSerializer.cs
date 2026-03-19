using Model;
using System.Text.Json;

namespace Lab4
{
    /// <summary>
    /// Статический класс для сериализации и 
    /// десериализации списка сотрудников
    /// </summary>
    public static class EmployeeSerializer
    {
        /// <summary>
        /// Сохраняет список сотрудников в файл в формате JSON
        /// </summary>
        /// <param name="employees">Список сотрудников для сохранения</param>
        /// <param name="path">Путь к файлу для сохранения</param>
        public static void Save(
            IEnumerable<EmployeBase> employees, string path)
        {
            var list = new List<object>();
            foreach (var employee in employees)
            {
                list.Add(employee);
            }

            var json = JsonSerializer.Serialize(
                list, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Загружает список сотрудников из файла в формате JSON
        /// </summary>
        /// <param name="path">Пусть к файлу для загрузки</param>
        /// <returns>Список загруженных сотрудников</returns>
        public static List<EmployeBase> Load(string path)
        {
            var text = File.ReadAllText(path);
            var root = JsonDocument.Parse(text).RootElement;
            var result = new List<EmployeBase>();

            foreach (var element in root.EnumerateArray())
            {
                if (element.TryGetProperty(
                    nameof(WageEmployee.Wage), out _))
                {
                    var hourlyEmployee = JsonSerializer.
                        Deserialize<WageEmployee>(element.GetRawText())!;
                    result.Add(hourlyEmployee);
                    continue;
                }

                if (element.TryGetProperty(
                    nameof(SalaryEmployee.Commission), out _))
                {
                    var commissionEmployee = JsonSerializer.
                        Deserialize<SalaryEmployee>(element.GetRawText())!;
                    result.Add(commissionEmployee);
                    continue;
                }
            }
            return result;
        }
    }
}