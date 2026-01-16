using Model;
using System;
using System.Xml.Linq;

namespace Lab2
{
    /// <summary>
    /// Класс, в котором выполняется основная часть программы
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception">Исключения</exception>
        static void Main(string[] args)
        {
            Console.WriteLine("Создадим лист и добавим туда 7 человек");
            Console.WriteLine();
            var listPeople = new PersonList();
            Random random = new();

            for (int i = 0; i <= 7; i++)
            { 
                Person randomPerson = random.Next(2) == 0
                    ? Adult.GetRandomPerson()
                    : Child.GetRandomPerson();
                listPeople.AddPerson(randomPerson);
            }

            Wait();

            Console.WriteLine("Вывод всех людей из списка: ");
            Console.WriteLine();
            PrintList(listPeople);

            Wait();

            //TODO: polymorphism

            Console.WriteLine("Тип четвертого человека из списка:");
            Console.WriteLine();
            var person = listPeople.GetPersonInIndex(3);

            switch (person)
            {
                case Adult personAdult:
                    Console.WriteLine(personAdult.GetCar());
                    break;
                case Child personChild:
                    Console.WriteLine(personChild.GetGame());
                    break;
                default:
                    break;
            }

            Wait();

        }

        /// <summary>
        /// Метод для вывода списка людей
        /// </summary>
        /// <param name="personList">Список людей</param>
        /// <exception cref="Exception">Список пуст!</exception>
        private static void PrintList(PersonList personList)
        {
            if (personList == null)
            {
                throw new Exception("Список людей пуст!");
            }

            if (personList.Count != 0)
            {
                for (int i = 0; i < personList.Count; i++)
                {
                    var personOfIndex = personList.GetPersonInIndex(i);
                    Console.WriteLine(personOfIndex.GetInfo());
                }
            }
            else
            {
                Console.WriteLine("Список пуст!");   
            }
        }

        /// <summary>
        ///Метод для ожидания действий пользователя 
        /// </summary>
        private static void Wait()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}