using System;
using System.Text.RegularExpressions;

namespace Model
{
    using System.Xml.Linq;

    /// <summary>
    /// Информация о человеке
    /// </summary>
    public abstract class Person
    {
        /// <summary>
        /// Имя человека
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия человека
        /// </summary>
        private string _surname;

        /// <summary>
        /// Возраст человека
        /// </summary>
        private int _age;

        /// <summary>
        /// Пол человека
        /// </summary>
        private Gender _gender;

        /// <summary>
        /// Флаг для проверки имени и фамилии на идентичность алфавита
        /// </summary>
        private static int _flagLanguage;

        /// <summary>
        /// Метод для обращения к private полям
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <exception cref="Exception">Исключение</exception>
        protected Person(string name, string surname, int age, Gender gender)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
        }

        /// <summary>
        /// Конструктор класса по умолчанию
        /// </summary>
        protected Person() : this("Бибиков", "Матвей", 10, Gender.Male) { }

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
        /// Проверка корректности ввода имени
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"{nameof(Name)}" +
                       $" не может быть пустым!");
                }

                _name = System.Globalization.CultureInfo.CurrentCulture.
                    TextInfo.ToTitleCase(value.ToLower());
            }
        }

        /// <summary>
        /// Проверка корректности ввода фамилии
        /// </summary>
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"{nameof(Surname)}" +
                      $" не может быть пустым!");
                }

                _surname = System.Globalization.CultureInfo.CurrentCulture.
                    TextInfo.ToTitleCase(value.ToLower());
            }
        }

        /// <summary>
        /// Минимальный возраст человека
        /// </summary>
        public const int MinAge = 0;

        /// <summary>
        /// Максимальный возраст человека
        /// </summary>
        public const int MaxAge = 123;

        /// <summary>
        /// Проверка корректности ввода возраста
        /// </summary>
        public int Age
        {
            get { return _age; }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new Exception("Введите возраст!");
                }

                if (value < MinAge || value > MaxAge)
                {
                    throw new Exception($"{nameof(Age)} должен быть в дипазоне" +
                        $" от {MinAge} до {MaxAge}");
                }
                _age = value;
            }
        }

        /// <summary>
        /// Пол человека
        /// </summary>
        public Gender Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// Метод для вывода имени и фамилии человека
        /// </summary>
        /// <returns>Имя и фамилия человека</returns>
        public string GetPersonNameAndSurname()
        {
            return $"{Name} {Surname}";
        }

        //TODO: XML +
        /// <summary>
        /// Метод для вывода информации о человеке
        /// </summary>
        /// <returns>Базовая информация о человеке</returns>
        public string GetPersonInfo()
        {
            return $"{Name} {Surname}, возраст: {Age}, пол: {Gender}";
        }

        //TODO: abstract +
        /// <summary>
        /// Абстрактный метод для получения информации, который
        /// будет индивидуально реализован в дочерних классах
        /// </summary>
        /// <returns>ИНформация о человеке</returns>
        public abstract string GetInfo();

        //TODO: abstract +
        /// <summary>
        /// Абстрактный метод проверки возраста, который 
        /// будет индивидуально реализован в дочерних классах
        /// </summary>
        /// <param name="age">Возраст человека</param>
        protected abstract void CheckAge(int age);

        /// <summary>
        /// Метод для удаления последнего символа в слове
        /// </summary>
        /// <param name="word">Слово, 
        /// в котором нужно убрать последний символ</param>
        /// <returns>Слово, которое было подано на вход 
        /// без последнего символа</returns>
        protected static string RemoveLastSimvol(string word)
        {
            string correctWord = "";
            for (int i = 0; i < word.Length - 1; i++)
            {
                correctWord = correctWord + word[i];
            }
            return correctWord;
        }
    }
}