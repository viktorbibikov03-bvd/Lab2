namespace Lab4
{
    /// <summary>
    /// Точка входа в приложение
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Точка входа в приложение: инициализирует конфигурацию
        /// и запускает главную форму.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
