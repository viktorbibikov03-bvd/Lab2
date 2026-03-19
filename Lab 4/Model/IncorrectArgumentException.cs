namespace Model
{
    /// <summary>
    /// Класс для генерации исключений
    /// </summary>
    public class IncorrectArgumentException : Exception
    {
        /// <summary>
        /// Конструктор по умолчанию для обработки исключений
        /// </summary>
        /// <param name="message">Сообщение об исключении</param>
        public IncorrectArgumentException(string message) : base(message) { }
    }
}
