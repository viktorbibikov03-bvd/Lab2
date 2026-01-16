using System;

namespace Model
{
    /// <summary>
    /// Класс, описывающий взрослого человека
    /// </summary>
    public class Adult : Person
    {
        /// <summary>
        /// Информация о паспорте (номер)
        /// </summary>
        private int _passportDetails;

        /// <summary>
        /// Место работы
        /// </summary>
        private string _jobName;

        /// <summary>
        /// Информация о супруге
        /// </summary>
        private Adult _partner;

        /// <summary>
        /// Минимальный возраст взрослого человека
        /// </summary>
        private const int MinAge = 18;

        /// <summary>
        /// Максимальный возраст взрослого человека
        /// </summary>
        private const int MaxAge = 123;

        /// <summary>
        /// Минимально возможный номер паспорта
        /// </summary>
        private const int PassportLowNumber = 000101;

        /// <summary>
        /// Максимально возможный номер паспорта
        /// </summary>
        private const int PassportHighNumber = 999999;

        /// <summary>
        /// Аксессор для паспортных данных
        /// </summary>
        public int PassportDetails
        {
            get
            {
                return _passportDetails;
            }
            set
            {
                CheckPassportDetails(value);
                _passportDetails = value;
            }
        }

        /// <summary>
        /// Аксессор для места работы
        /// </summary>
        public string JobName
        {
            get
            {
                return _jobName;
            }
            set
            {
                _jobName = value;
            }
        }

        /// <summary>
        /// Аксессор для информации о супруге
        /// </summary>
        public Adult Partner
        {
            get
            {
                return _partner;
            }
            set
            {
                _partner = value;
            }
        }

        /// <summary>
        /// Метод для обращения к private полям
        /// для наследованного класса
        /// </summary>
        /// <param name="name">Имя человека</param>
        /// <param name="surname">Фамилия человека</param>
        /// <param name="age">Возраст человека</param>
        /// <param name="gender">Пол человека</param>
        /// <param name="passportDetails">Информация о паспорте</param>
        /// <param name="partner">Информация о супруге</param>
        /// <param name="jobName">Место работы</param>
        public Adult(string name, string surname, int age,
            Gender gender, int passportDetails, Adult partner,
            string jobName) : base(name, surname, age, gender)
        {
            PassportDetails = passportDetails;
            JobName = jobName;
            Partner = partner;
        }

        /// <summary>
        /// Создание конструктора для взрослого человека
        /// </summary>
        public Adult() : this("Неизвестно", "Неизвестно",
            18, Gender.Female, 000101, null, null)
        { }

        /// <summary>
        /// Метод для проверки корректности номера паспорта
        /// </summary>
        /// <param name="passportNumber">Номер паспорта</param>
        /// <exception cref="Exception">Сообщение об ошибке</exception>
        private static void CheckPassportDetails(int passportNumber)
        {
            if ((passportNumber < PassportLowNumber) ||
                (passportNumber > PassportHighNumber))
            {
                throw new Exception($"Номер паспорта должен быть в пределах" +
                    $" от {PassportLowNumber} до {PassportHighNumber}");
            }
        }

        /// <summary>
        /// Метод для вывода информации о взрослом человеке
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            var marriedStatus = "Не состоит в браке";
            if (Partner != null)
            {
                marriedStatus = $"Состоит в браке с: " +
                    $"{Partner.Name} {Partner.Surname}";
            }

            var jobStatus = "Безработный";
            if (!string.IsNullOrEmpty(JobName))
            {
                jobStatus = $"Место работы: {JobName}";
            }

            return $"{GetPersonInfo()};\n Номер паспорта: {PassportDetails}; " +
                $"\n {marriedStatus}; \n {jobStatus}. \n";
        }

        /// <summary>
        /// Проверка человека на взрослость
        /// </summary>
        /// <param name="age">Возраст человека</param>
        /// <exception cref="Exception">Возраст должен быть 
        /// в определнном диапозоне</exception>
        protected override void CheckAge(int age)
        {
            if ((age < MinAge) || (age > MaxAge))
            {
                throw new Exception($"Возраст взрослого человека " +
                    $"от {MinAge} до {MaxAge}");
            }
        }

        /// <summary>
        /// Метод для создания случайного взрослого человека
        /// </summary>
        /// <returns>Данные о случайном взрослом человеке</returns>
        public static Adult GetRandomPerson(Gender gender = Gender.Unknown)
        {
            string[] nameMaleList = { "Александр", "Дмитрий" };
            string[] nameFemaleList = { "Анастасия", "Екатерина" };
            string[] surnameMaleList = { "Иванов", "Петров" };
            string[] surnameFemaleList = { "Иванова", "Петрова" };
            string[] jobList = { "СО ЕЭС", "Ростелеком", "Сбербанк" };
            string[] spouseStatusList = { "Состоит в браке", " Не состоит в браке" };

            Random random = new();

            if (gender == Gender.Unknown)
            {
                Gender[] genderList = { Gender.Male, Gender.Female };
                int genderIndex = random.Next(genderList.Length);
                gender = genderIndex == 0
                    ? Gender.Male
                    : Gender.Female;
            }

            string namePerson = gender == Gender.Male
                ? nameMaleList[random.Next(nameMaleList.Length)]
                : nameFemaleList[random.Next(nameFemaleList.Length)];

            string surnamePerson = gender == Gender.Male
                ? surnameMaleList[random.Next(surnameMaleList.Length)]
                : surnameFemaleList[random.Next(surnameFemaleList.Length)];

            int age = random.Next(MinAge, MaxAge + 1);

            int passportNumber = random.
                            Next(PassportLowNumber, PassportHighNumber + 1);

            Adult tmpSpouse = null;
            var spouseStatus = spouseStatusList[random.
                Next(spouseStatusList.Length)];
            if (spouseStatus == "Состоит в браке")
            {
                tmpSpouse = new Adult();

                tmpSpouse.Name = gender == Gender.Male
                    ? nameFemaleList[random.Next(nameFemaleList.Length)]
                    : nameMaleList[random.Next(nameMaleList.Length)];

                tmpSpouse.Surname = surnamePerson;

                tmpSpouse.Surname = gender == Gender.Female
                    ? RemoveLastSimvol(tmpSpouse.Surname)
                    : tmpSpouse.Surname + "а";
            }
            else
            {
                tmpSpouse = null;
            }

            var employerStatus = random.Next(1, 3);

            string tmpEmployer = employerStatus == 1
                ? tmpEmployer = jobList[random.Next(jobList.Length)]
                : tmpEmployer = "Безработный";

            return new Adult(namePerson, surnamePerson, age, gender,
                passportNumber, tmpSpouse, tmpEmployer);
        }

        /// <summary>
        /// Метод для определения взрослого человека
        /// </summary>
        /// <returns>Сообщение, характеризующее взрослого человека</returns>
        public string GetCar()
        { 
            Random random = new();

            string[] carBrand = { "Audi", "Porsche", "Volkswagen" };

            var driverCar = carBrand[random.Next(carBrand.Length)];

            return $"Этот человек ездит на машине марки {driverCar}," +
                $" он взрослый";
        }
    }
}