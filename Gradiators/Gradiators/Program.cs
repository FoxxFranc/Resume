using System;
using System.Collections;
using System.Collections.Generic;

/*
5 бойцов, пользователь выбирает 2 бойцов и они сражаются друг с другом до смерти. У каждого бойца могут быть свои статы.
Каждый игрок должен иметь особую способность для атаки, которая свойственна только его классу!
 */

namespace Gradiators
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;
            Console.WriteLine("Приветсвую путник! Ты пришел на арену.");
            while (isWorking)
            {
                Console.WriteLine("1 - остаться, esc - выйти");
                switch (Console.ReadLine())
                {
                    case "1":
                        Arena arena = new Arena();
                        arena.Сhoice();
                        arena.Fight();
                        break;
                    case "esc":
                        isWorking = false;
                        Console.WriteLine("Ждем вас снова!");
                        break;
                    default:
                        Console.WriteLine("Что это за слово? Ты тоже варвар? РЕБЯТА, А НУ ДАВАЙ СЮДА. ЗДЕСЬ НОВЫЙ РАБ НАШЕЛСЯ!");
                        isWorking = false;
                        break;
                }
            }
        }
    }

    class Arena
    {
        private List<Gladiator> _allGladiators = new List<Gladiator>(5);
        private Gladiator _leftGladiator = null;
        private Gladiator _rightGladiator = null;

        public Arena()
        {
        }

        public void ShowInfo()
        {

            int id = 0;
            foreach (var gladiator in _allGladiators)
            {
                id++;
                Console.Write($"Гладиатор {id} - ");
                gladiator.ShowInfo();
            }
        }

        public void Сhoice()
        {
            Create();
            ShowInfo();
            string id;
            Console.WriteLine("Выберите левого гладиатора");
            id = Console.ReadLine();
            _leftGladiator = FindGladiator(id);
            ShowInfo();
            Console.WriteLine("Выберите правого гладиатора");
            id = Console.ReadLine();
            _rightGladiator = FindGladiator(id);
            Console.WriteLine($"Итак! Сегодня у нас дерутся {_leftGladiator.Name} и {_rightGladiator.Name}");
            Console.ReadKey();
        }

        public void Fight()
        {
            while (_leftGladiator.Health > 0 && _rightGladiator.Health > 0)
            {
                _leftGladiator.ShowInfo();
                _rightGladiator.ShowInfo();
                _leftGladiator.TakeDammage(_rightGladiator.GetDammage());
                _rightGladiator.TakeDammage(_leftGladiator.GetDammage());
                Console.ReadKey();
            }
            if (_leftGladiator.Health == 0 && _rightGladiator.Health == 0)
            {
                Console.WriteLine("Ничья!");
            }
            else if (_leftGladiator.Health > 0)
            {
                Console.WriteLine($"Победил {_leftGladiator.Name}");
            }
            else
            {
                Console.WriteLine($"Победил {_rightGladiator.Name}");
            }
        }

        private void Create()
        {
            Gladiator _peasant = new Gladiator("Крестьянин", 50, 1, 500);
            Knight _knight = new Knight("Рыцарь", 50, 5, 1200);
            Barbarian _barbarian = new Barbarian("Варвар", 60, 2, 600, 1);
            Rogue _rogue = new Rogue("Разбойник", 75, 2, 500, 25);
            Archer _archer = new Archer("Лучник", 60, 1, 500);
            _allGladiators.AddRange(new Gladiator[] { _peasant, _knight, _rogue, _barbarian, _archer });
        }

        private Gladiator FindGladiator(string id)
        {
            Random random = new Random();
            Gladiator desiredGladiator;
            int number;
            if (int.TryParse(id, out number))
            {
                for (int i = 0; i < _allGladiators.Count; i++)
                {
                    if (number - 1 == i)
                    {
                        desiredGladiator = _allGladiators[i];
                        _allGladiators.Remove(_allGladiators[i]);
                        return desiredGladiator;
                    }
                }
            }
            Console.WriteLine("Гладиатор не найден. Выбран случайный гладиатор");
            return _allGladiators[random.Next(0, _allGladiators.Count - 1)];
        }
    }

    class Gladiator
    {
        public string Name { get; private set; }
        public int Dammage { get; protected set; }
        public int Armor { get; protected set; }
        public int Health { get; protected set; }

        public Gladiator(string name, int dammage, int armor, int health)
        {
            Name = name;
            Dammage = dammage;
            Armor = armor;
            Health = health;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"{Name}: урон - {Dammage}; броня - {Armor}, HP - {Health}");
        }

        public virtual void UseUltimateSkill()
        {
            Console.WriteLine("Размах виллами!");
            Dammage += 1;
        }

        public void TakeDammage(int dammage)
        {
            Health -= dammage - Armor;
        }

        public int GetDammage()
        {
            Random random = new Random();
            if (random.Next(1, 3) == 1)
            {
                UseUltimateSkill();
                return Dammage;
            }
            else
            {
                return Dammage;
            }
        }
    }

    class Knight : Gladiator
    {
        public Knight(string name, int dammage, int armor, int health) : base(name, dammage, armor, health)
        {
        }

        public override void UseUltimateSkill()
        {
            Console.WriteLine("Во славу света!");
            Armor += 1;
        }
    }

    class Barbarian : Gladiator
    {
        private int _attackSpeed;
        public Barbarian(string name, int dammage, int armor, int health, int attackSpeed) : base(name, dammage * attackSpeed, armor, health)
        {
            _attackSpeed = attackSpeed;
        }

        public override void UseUltimateSkill()
        {
            Console.WriteLine("Жажда крови!");
            _attackSpeed *= 2;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"{Name}: урон - {Dammage}; броня - {Armor}, HP - {Health}, скорость атаки - {_attackSpeed}");
        }
    }

    class Rogue : Gladiator
    {
        private int _ultimateEvasion;

        public Rogue(string name, int dammage, int armor, int health, int evasion) : base(name, dammage, armor, health)
        {
            _ultimateEvasion = evasion;
        }

        public override void UseUltimateSkill()
        {
            Random random = new Random();
            Console.Write("Хитрый трюк! -");
            if (random.Next(0, 100) <= _ultimateEvasion)
            {
                Console.WriteLine("Ускоренные удары");
                Dammage += 15;
            }
            else
            {
                Console.WriteLine("Использование подорожника");
                Health += 60;
            }
        }
    }

    class Archer : Gladiator
    {
        public Archer(string name, int dammage, int armor, int health) : base(name, dammage, armor, health)
        {
        }

        public override void UseUltimateSkill()
        {
            Console.WriteLine("Дополнительная стрела!");
            Dammage += 3;
        }
    }
}
