namespace Model
{
    /// <summary>
    /// Флаг для проверки имени и фамилии на идентичность алфавита
    /// </summary>
    public enum FlagLanguage
    {
        /// <summary>
        /// Слово содержить только русские символы
        /// </summary>
        Russian,

        /// <summary>
        /// Слово содержит только английские символы
        /// </summary>
        English,

        /// <summary>
        /// Слово содержит неизвестные символы
        /// </summary>
        Another
    }
}