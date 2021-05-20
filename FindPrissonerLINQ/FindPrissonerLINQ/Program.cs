using System;
using System.Collections.Generic;
using System.Linq;

/*
У нас есть список всех преступников.
В преступнике есть поля: ФИО, заключен ли он под стражу, рост, вес, национальность.
Вашей программой будут пользоваться детективы.
У детектива запрашиваются данные (рост, вес, национальность), и детективу выводятся все преступники, которые подходят под эти параметры, но уже заключенные под стражу выводиться не должны.
 */

namespace FindPrissonerLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Database data = new Database();
            bool isWorking = true;

            while (isWorking)
            {
                Console.Clear();
                Console.WriteLine("Введите команду:\n" +
                    "1 - показать всех преступников\n" +
                    "2 - показать всех разыскиваемых преступников по параметрам РОСТ ВЕС НАЦИОНАЛЬНОСТЬ.\n" +
                    "3 - показать всех не заключенных под стражу преступников\n" +
                    "esc - выход");
                switch (Console.ReadLine())
                {
                    case "1":
                        data.ShowAllCriminals();
                        break;
                    case "2":
                        Console.Write("Введите рост преступника - ");
                        int height = GetCorrectParametr(Console.ReadLine());
                        Console.Write("\nВведите вес преступника - ");
                        int weight = GetCorrectParametr(Console.ReadLine());
                        Console.Write("\nВведите национальность преступника - ");
                        string nation = Console.ReadLine();
                        if (height == -1 || weight == -1)
                        {
                            Console.WriteLine("Введены некоректные данные");
                        }
                        else if (height != -1 && weight != -1)
                        {
                            data.ShowWantedCriminals(height, weight, nation);
                        }

                        break;
                    case "3":
                        data.ShowAllWantedCriminals();
                        break;
                    case "esc":
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
                Console.ReadKey();
            }
        }

        static int GetCorrectParametr(string message)
        {
            int number;
            if (int.TryParse(message, out number) == false)
            {
                return -1;
            }
            else
            {
                return number;
            }
        }
    }

    class Database
    {
        private List<Criminal> _allCriminals;

        public Database()
        {
            _allCriminals = new List<Criminal>();
            _allCriminals.Add(new Criminal("Андрей", false, 180, 75, "Русский"));
            _allCriminals.Add(new Criminal("Максим", true, 175, 60, "Украинец"));
            _allCriminals.Add(new Criminal("Илья", false, 175, 60, "Украинец"));
        }

        public void ShowAllCriminals()
        {
            foreach (var criminal in _allCriminals)
            {
                criminal.ShowInfo();
            }
        }

        public void ShowAllWantedCriminals()
        {
            var wantedCriminals = _allCriminals.Where(_allCriminals => _allCriminals.InPrisoned == false);
            Console.WriteLine("Преступники не заключенные под стражу:");
            foreach (var criminal in wantedCriminals)
            {
                criminal.ShowInfo();
            }
        }

        public void ShowWantedCriminals(int height, int weight, string nation)
        {
            var wantedCriminals = _allCriminals.Where(_allCriminals => _allCriminals.InPrisoned == false && _allCriminals.Height == height && _allCriminals.Weight == weight && _allCriminals.Nation == nation);
            Console.WriteLine($"Разыскиваемые преступники по параметрам: рост - {height}, вес - {weight}; национальность {nation}");
            foreach (var criminal in wantedCriminals)
            {
                criminal.ShowInfo();
            }
        }
    }

    class Criminal
    {
        public string FullName { get; private set; }
        public bool InPrisoned { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public string Nation { get; private set; }

        public Criminal(string fullName, bool inPrisoned, int height, int weight, string nation)
        {
            FullName = fullName;
            InPrisoned = inPrisoned;
            Height = height;
            Weight = weight;
            Nation = nation;
        }

        public void ShowInfo()
        {
            string inPrisoned = "Нет";
            if (InPrisoned == true)
            {
                inPrisoned = "Да";
            }
            else
            {
                inPrisoned = "Нет";
            }
            Console.WriteLine($"{FullName}: Заключен под стражу - {inPrisoned}; Рост - {Height}; Вес - {Weight}; Национальность - {Nation}");
        }
    }
}
