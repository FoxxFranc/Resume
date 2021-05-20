using System;
using System.Threading;
using System.Collections.Generic;

/*
Есть 2 взвода. 1 взвод страны один, 2 взвод страны два.
Каждый взвод внутри имеет солдат.
Нужно написать программу, которая будет моделировать бой этих взводов.
Каждый боец - это уникальная единица, он может иметь уникальные способности или же уникальные характеристики, такие как повышенная сила.
Побеждает та страна, во взводе которой остались выжившие бойцы.
 */

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;
            while (isWorking)
            {
                Platoon platoon1 = new Platoon(101);
                Platoon platoon2 = new Platoon(100);
                Console.WriteLine("Генерал, отдайте приказ! (Атака - начать бой! Побег - отступить!)");
                switch (Console.ReadLine())
                {
                    case "Атака":
                        while (platoon1.GetCount() > 0 && platoon2.GetCount() > 0)
                        {

                            Console.WriteLine("Наш взвод:");
                            platoon1.ShowInfo();
                            Console.WriteLine("\nВзвод врага:");
                            platoon2.ShowInfo();
                            Console.WriteLine();
                            platoon1.TakeDammage(platoon2);
                            platoon2.TakeDammage(platoon1);
                            Thread.Sleep(500);
                            Console.Clear();
                        }
                        if (platoon1.GetCount() == 0 && platoon2.GetCount() == 0)
                        {
                            Console.WriteLine("Ничья");
                        }
                        else if (platoon1.GetCount() == 0)
                        {
                            Console.WriteLine("Поражение");
                            Console.WriteLine("У врага осталось:");
                            platoon2.ShowInfo();
                        }
                        else
                        {
                            Console.WriteLine("Мы победили");
                            Console.WriteLine("У нас остались:");
                            platoon1.ShowInfo();
                        }

                        break;
                    case "Побег":
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Такого приказа нет в уставе!");
                        break;
                }
            }
        }
    }

    class Platoon
    {
        private List<Troop> _allTroops = new List<Troop>();
        private Dictionary<string, int> _troopsCount;
        private static Random random = new Random();

        public Platoon(int count)
        {
            _allTroops = new List<Troop>(count);
            _troopsCount = new Dictionary<string, int>();

            _troopsCount.Add("Swordmaster", 0);
            _troopsCount.Add("Archer", 0);
            _troopsCount.Add("Mage", 0);
            _troopsCount.Add("Warlock", 0);
            _troopsCount.Add("Berserker", 0);
            do
            {
                switch (random.Next(1, 6))
                {
                    case 1:
                        _allTroops.Add(new Troop());
                        _troopsCount["Swordmaster"]++;
                        break;
                    case 2:
                        _allTroops.Add(new Archer());
                        _troopsCount["Archer"]++;
                        break;
                    case 3:
                        _allTroops.Add(new Mage());
                        _troopsCount["Mage"]++;
                        break;
                    case 4:
                        _allTroops.Add(new Warlock());
                        _troopsCount["Warlock"]++;
                        break;
                    case 5:
                        _allTroops.Add(new Berserker());
                        _troopsCount["Berserker"]++;
                        break;
                }
            }
            while (_allTroops.Count < count);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Здоровье взвода - {GetTotalHealth()}");
            Console.WriteLine($"Количество мечников - {_troopsCount["Swordmaster"]}; Количество лучников - {_troopsCount["Archer"]}; Количество магов - {_troopsCount["Mage"]}; Количество чернокнижников - {_troopsCount["Warlock"]}; Количество берсеркеров {_troopsCount["Berserker"]}");
        }



        public int GetCount()
        {
            return _allTroops.Count;
        }

        public void TakeDammage(Platoon enemyPlatoon)
        {
            Random random = new Random();

            for (int i = 0; i < enemyPlatoon.GetCount(); i++)
            {
                int randomTroopId = random.Next(0, _allTroops.Count - 1);
                if (enemyPlatoon.GetCount() < 0)
                {

                    Troop enemyTroop = GetRandomTroop(enemyPlatoon);
                    if (enemyTroop != null)
                    {
                        _allTroops[randomTroopId].TakeDammage(enemyTroop.Dammage, enemyTroop.DammageType);
                    }

                }
                else
                {
                    _allTroops[randomTroopId].TakeDammage(enemyPlatoon.GetTroops()[enemyPlatoon.GetTroops().Count - 1].Dammage, enemyPlatoon.GetTroops()[enemyPlatoon.GetTroops().Count - 1].DammageType);
                }
            }
            RemoveDeadTroops();
        }

        public Troop GetRandomTroop(Platoon platoon)
        {
            Random random = new Random();
            int randomId = random.Next(0, platoon.GetCount() - 1);
            int count = platoon.GetTroops().Count;
            if (count > 0)
            {
                return platoon.GetTroops()[randomId];
            }
            return null;
        }

        private List<Troop> GetTroops()
        {
            return _allTroops;
        }

        private void RemoveDeadTroops()
        {
            for (int i = 0; i < _allTroops.Count; i++)
            {
                if (_allTroops[i].Health <= 0)
                {
                    _troopsCount[_allTroops[i].Name]--;
                    _allTroops.Remove(_allTroops[i]);
                    i--;
                }
            }
        }


        private int GetTotalHealth()
        {
            int totalHealth = 0;
            foreach (var troop in _allTroops)
            {
                totalHealth += troop.Health;
            }
            return totalHealth;
        }
    }

    class Troop
    {
        public string Name { get; protected set; }
        public int Dammage { get; protected set; }
        public int Armor { get; protected set; }
        public int Health { get; protected set; }
        public string DammageType { get; protected set; }
        public string ArmorType { get; protected set; }

        public Troop()
        {
            Random random = new Random();
            Name = "Swordmaster";
            Dammage = random.Next(20, 40);
            Armor = random.Next(5, 14);
            Health = random.Next(150, 250);
            DammageType = "Physical";
            ArmorType = "Physical";
        }

        public virtual void TakeDammage(int dammage, string dammageType)
        {
            if (dammageType == ArmorType)
            {
                Health -= (dammage - Armor * 2);
            }
            else
            {
                Health -= dammage - Armor;
            }
        }

        public virtual void UseUltimateWeapon()
        {
            Random random = new Random();
            if (random.Next(1, 5) == 1)
            {
                Dammage += 5;
            }
        }

        public int GetDammage()
        {
            Random random = new Random();
            if (random.Next(1, 4) == 1)
            {
                UseUltimateWeapon();
            }
            return Dammage;
        }
    }

    class Archer : Troop
    {
        public Archer()
        {
            Random random = new Random();
            Name = "Archer";
            Dammage = random.Next(30, 50);
            Armor = random.Next(2, 7);
            Health = random.Next(100, 200);
            DammageType = "Physical";
            ArmorType = "Physical";
        }

        public override void UseUltimateWeapon()
        {
            Random random = new Random();
            if (random.Next(1, 5) == 1)
            {
                Dammage += 10;
            }
        }
    }

    class Mage : Troop
    {
        public Mage()
        {
            Random random = new Random();
            Name = "Mage";
            Dammage = random.Next(50, 80);
            Armor = random.Next(1, 5);
            Health = random.Next(75, 150);
            DammageType = "Magic";
            ArmorType = "Magic";
        }

        public override void UseUltimateWeapon()
        {
            Random random = new Random();
            if (random.Next(1, 5) == 1)
            {
                Health += 30;
            }
        }
    }

    class Warlock : Troop
    {
        public Warlock()
        {
            Random random = new Random();
            Name = "Warlock";
            Dammage = random.Next(60, 90);
            Armor = random.Next(1, 5);
            Health = random.Next(75, 125);
            DammageType = "Magic";
            ArmorType = "Physical";
        }

        public override void UseUltimateWeapon()
        {
            Random random = new Random();
            if (random.Next(1, 5) == 1)
            {
                Dammage += 30;
                Health -= 2;
                if (Health <= 0)
                {
                    Health = 1;
                }
                Armor -= 1;
                if (Armor <= 0)
                {
                    Armor = 1;
                }
            }
        }
    }

    class Berserker : Troop
    {
        public Berserker()
        {
            Random random = new Random();
            Name = "Berserker";
            Dammage = random.Next(70, 120);
            Armor = random.Next(5, 10);
            Health = random.Next(100, 200);
            DammageType = "Chaos";
            ArmorType = "Physical";
        }

        public override void UseUltimateWeapon()
        {
            base.UseUltimateWeapon();
            Armor += 1;
        }
    }
}
