using System;
using System.Globalization;
using System.Text;

namespace Model
{
    /// <summary>
    /// Класс, описывающий ребенка
    /// </summary>
    public class Child : Person
    {
        /// <summary>
        /// Отец ребенка
        /// </summary>
        private Adult _father;

        /// <summary>
        /// Мать ребенка
        /// </summary>
        private Adult _mother;

        /// <summary>
        /// Школа, в которой учится ребенок
        /// </summary>
        private string _school;

        /// <summary>
        /// Минимальный возраст ребенка
        /// </summary>
        private const int MinAge = 0;

        /// <summary>
        /// Максимальный возвраст ребенка
        /// </summary>
        private const int MaxAge = 17;

        /// <summary>
        /// Аксессор для отца ребенка
        /// </summary>
        public Adult Father
        {
            get
            {
                return _father;
            }
            set
            {
                CheckParentGender(value, Gender.Female);
                _father = value;
            }
        }

        /// <summary>
        /// Аксессор для матери ребенка
        /// </summary>
        public Adult Mother
        {
            get
            {
                return _mother;
            }
            set
            {
                CheckParentGender(value, Gender.Male);
                _mother = value;
            }
        }

        /// <summary>
        /// Аксессор для школы, в которой учится ребенок
        /// </summary>
        public string School
        {
            get
            {
                return _school;
            }
            set
            {
                _school = value;
            }
        }

        /// <summary>
        /// Метод для обращения к private полям
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="surname">Фамилия человека</param>
        /// <param name="age">Возраст человека</param>
        /// <param name="gender">Пол человека</param>
        /// <param name="father">Отец ребенка</param>
        /// <param name="mother">Мать ребенка</param>
        /// <param name="school">Школа, в которой учится ребенок</param>
        public Child(string name, string surname, int age, Gender gender,
            Adult father, Adult mother, string school) :
            base(name, surname, age, gender)
        {
            Father = father;
            Mother = mother;
            School = school;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Child() : this("Неизвестно", "Неизвестно", 10, Gender.Female,
            null, null, null) { }

        /// <summary>
        /// Проверка пола родителя
        /// </summary>
        /// <param name="parent">Родитель</param>
        /// <param name="gender">Пол, противоположный тому, 
        /// что должен иметь родитель</param>
        /// <exception cref="Exception">Родители должны иметь 
        /// биологически правильный пол</exception>
        private static void CheckParentGender(Adult parent, Gender gender)
        {
            if (parent != null && parent.Gender == gender)
            {
                throw new Exception($"Пол родителя должен быть другой!");
            }
        }

        /// <summary>
        /// Проверка возраста ребенка
        /// </summary>
        /// <param name="age">Возраст</param>
        /// <exception cref="Exception">Возраст должен соостветствовать 
        /// возрасту ребенка</exception>
        protected override void CheckAge(int age)
        {
            if ((age < MinAge) || (age > MaxAge))
            {
                throw new Exception($"Возраст ребенка должен быть" +
                    $" в пределах от {MinAge} до {MaxAge}");
            }
        }

        /// <summary>
        /// Метод для вывода информации о ребенке
        /// </summary>
        /// <returns>Информация о ребенке</returns>
        public override string GetInfo()
        {
            var fatherExistence = "Отец отсутствует";
            var motherExistence = "Мать отсутствует";

            if (Father != null)
            {
                fatherExistence = $"Отец - {Father.GetPersonNameAndSurname()}";
            }

            if (Mother != null)
            {
                motherExistence = $"Мать - {Mother.GetPersonNameAndSurname()}";
            }

            var schoolStatus = "Не ходит в школу";
            if (!string.IsNullOrEmpty(School))
            {
                schoolStatus = $"Школа: {School}";
            }

            return $"{GetPersonInfo()};\n {fatherExistence};\n " +
                $"{motherExistence};\n {schoolStatus}\n";
        }

        /// <summary>
        /// Метод, создающий родителя для ребенка
        /// </summary>
        /// <returns>Родитель или ничего</returns>
        /// <exception cref="ArgumentException">Ребенок может иметь
        /// 1 или 2 родителей</exception>
        public static Adult GetRandomParent(Gender gender)
        { 
            var random = new Random();
            var parentExistence = random.Next(1, 3);
            if (parentExistence == 1)
            {
                return null;
            }
            else
            {
                return Adult.GetRandomPerson(gender);
            }
        }

        /// <summary>
        /// Метод для создания случайного ребенка
        /// </summary>
        /// <returns>Данные о случайном ребенке</returns>
        public static Child GetRandomPerson()
        {
            string[] nameMaleList = { "Александр", "Дмитрий" };
            string[] nameFemaleList = { "Анастасия", "Екатерина" };
            string[] surnameMaleList = { "Иванов", "Петров" };
            string[] surnameFemaleList = { "Иванова", "Петрова" };
            Gender[] genderList = { Gender.Male, Gender.Female };
            string[] schoolList = { "БСОШ №1", "БСОШ №2", "Лицей при ТПУ"};

            Random random = new();

            int genderIndex = random.Next(genderList.Length);

            Gender randomPersonGender = genderList[genderIndex];

            string namePerson = genderIndex == 0
                ? nameMaleList[random.Next(nameMaleList.Length)]
                : nameFemaleList[random.Next(nameFemaleList.Length)];

            string surnamePerson = null;

            int age = random.Next(MinAge, MaxAge + 1);

            Adult parentFather = GetRandomParent(Gender.Male);

            Adult parentMother = GetRandomParent(Gender.Female);

            if ((parentMother == null) && (parentFather == null))
            {
                surnamePerson = genderIndex == 0
                    ? surnameMaleList[random.Next(surnameMaleList.Length)]
                    : surnameFemaleList[random.Next(surnameFemaleList.Length)];
            }
            else if ((parentFather == null) && (parentMother != null))
            {
                surnamePerson = genderIndex == 0
                    ? RemoveLastSimvol(parentMother.Surname)
                    : parentMother.Surname;
            }
            else if ((parentFather != null) && (parentMother == null))
            {
                surnamePerson = genderIndex == 0
                    ? parentFather.Surname
                    : parentFather.Surname + "а";
            }
            else 
            {
                surnamePerson = genderIndex == 0
                    ? parentFather.Surname
                    : parentMother.Surname;
            }

            var schoolStatus = random.Next(1, 3);

            string tmpStudent = ((schoolStatus == 1) && (age > 6))
                ? tmpStudent = schoolList[random.Next(schoolList.Length)]
                : tmpStudent = "Отсутствует";

            return new Child(namePerson, surnamePerson, age, randomPersonGender,
                parentFather, parentMother, tmpStudent);
        }

        /// <summary>
        /// Метод для определения ребенка
        /// </summary>
        /// <returns>Сообщение, характеризующее ребенка</returns>
        public string GetGame()
        {
            Random random = new();

            string[] games = { "Minecraft", "Roblox", "CS:GO 2" };

            var game = games[random.Next(games.Length)];

            return $"Человеку нравится играть в {game}, он ребенок";
        }
    }
}