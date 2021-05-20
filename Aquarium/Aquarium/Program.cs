using System;
using System.Collections;
using System.Collections.Generic;

/*
Есть аквариум, в котором плавают рыбы. В этом аквариуме может быть максимум определенное кол-во рыб. Рыб можно добавить в аквариум или рыб можно достать из аквариума. 
 */

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;
            Aquarium aquarium = new Aquarium();
            while (isWorking)
            {
                Console.WriteLine("Вы следите за аквариумом.\n" +
                    "Чтобы добавить рыбку, введите 1;\n" +
                    "Чтобы вытащить рыбку, введите 2;\n" +
                    "Чтобы перестать следить за аквариумом, введите esc\n" +
                    "Напишите любое другое сообщение чтобы продолжить следить.");
                aquarium.ShowInfo();
                switch (Console.ReadLine())
                {
                    case "1":
                        aquarium.AddNewFish();
                        break;
                    case "2":
                        aquarium.PullOutFish();
                        break;
                    case "esc":
                        isWorking = false;
                        break;
                }
                Console.Clear();
                aquarium.RemoveDeadFishes();
                aquarium.GoNextDay();
            }
        }
    }

    class Fish
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int MaxAge { get; private set; }
        public bool IsDead { get; private set; }

        public Fish(string name)
        {
            Random random = new Random();
            Name = name;
            Age = random.Next(0, 10);
            MaxAge = random.Next(20, 40);
            IsDead = false;
        }

        public void ShowInfo()
        {
            if (MaxAge <= Age)
            {
                Console.WriteLine($"Рыбка {Name} умерла");
                IsDead = true;
            }
            else
            {
                IsDead = false;
                Console.WriteLine($"Рыбка {Name}; возраст: {Age} дней; осталось жить {MaxAge - Age} дней");
            }
        }
        public void ReduseAge()
        {
            Age += 1;
        }
    }

    class Aquarium
    {
        private List<Fish> _allFishes;
        private int _maxCount;

        public Aquarium(List<Fish> allFishes)
        {
            _allFishes = allFishes;
            _maxCount = 10;
        }

        public Aquarium()
        {
            _allFishes = new List<Fish>();
            _maxCount = 10;
        }

        public void ShowInfo()
        {
            if (_allFishes.Count == 0)
            {
                Console.WriteLine("Аквариум пуст");
            }
            else
            {
                foreach (var fish in _allFishes)
                {
                    fish.ShowInfo();
                }
            }
        }

        public void RemoveDeadFishes()
        {
            for (int i = 0; i < _allFishes.Count; i++)
            {
                if (_allFishes[i].IsDead)
                {
                    _allFishes.Remove(_allFishes[i]);
                    i--;
                }
            }
        }

        public void AddNewFish()
        {
            if (_allFishes.Count + 1 > _maxCount)
            {
                Console.Clear();
                Console.WriteLine("В аквариуме нет мест");
                Console.ReadKey();
            }
            else
            {
                string name;
                bool sameName = false;
                Console.WriteLine("Как зовут новую рыбку?");
                name = Console.ReadLine();
                foreach (var fish in _allFishes)
                {
                    if (name == fish.Name)
                    {
                        sameName = true;
                        break;
                    }
                }
                if (sameName)
                {
                    Console.WriteLine("Рыбка с таким именем уже есть");
                    Console.ReadKey();
                }
                else
                {
                    _allFishes.Add(new Fish(name));
                    Console.WriteLine($"Рыбка {_allFishes[_allFishes.Count - 1]} возрастом {_allFishes[_allFishes.Count - 1].Age} была добавленна в аквариум");
                }
            }
        }

        public void PullOutFish()
        {
            string name;
            Console.WriteLine("Какую рыбку вы хотите вытащить?");
            name = Console.ReadLine();
            foreach (var fish in _allFishes)
            {
                if (fish.Name == name)
                {
                    Console.WriteLine($"Вы достаете рыбку {fish.Name}");
                    _allFishes.Remove(fish);
                    break;
                }
            }
        }

        public void GoNextDay()
        {
            foreach (var fish in _allFishes)
            {
                fish.ReduseAge();
            }
        }
    }
}
