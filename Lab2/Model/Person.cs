using System;
using System.Text.RegularExpressions;

namespace Model
{
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
        /// Проверка корректности ввода имени
        /// </summary>
        public string Name
        {
            get 
            { 
                return _name; 
            }
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
            get 
            { 
                return _surname; 
            }
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
            get 
            { 
                return _age; 
            }
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
            get 
            { 
                return _gender; 
            }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// Метод для вывода информации о человеке
        /// </summary>
        /// <returns>Базовая информация о человеке</returns>
        public string GetPersonInfo()
        {
            return $"{Name} {Surname}, возраст: {Age}, пол: {Gender}";
        }

        /// <summary>
        /// Метод для вывода имени и фамилии человека
        /// </summary>
        /// <returns>Имя и фамилия человека</returns>
        public string GetPersonNameAndSurname()
        {
            return $"{Name} {Surname}";
        }

        /// <summary>
        /// Абстрактный метод для получения информации, который
        /// будет индивидуально реализован в дочерних классах
        /// </summary>
        /// <returns>Информация о человеке</returns>
        public abstract string GetInfo();

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