using System;
using System.Collections.Generic;

/*
Реализовать базу данных игроков и методы для работы с ней.
У игрока может быть порядковый номер, ник, уровень, флаг – забанен ли он(флаг - bool).
Реализовать возможность добавления игрока, бана игрока по порядковому номеру, разбана игрока по порядковому номеру и удаление игрока.
 */

namespace UsersData
{
    class Program
    {
        static void Main(string[] args)
        {
            Database allPlayersData = new Database();
            bool isWorking = true;
            while (isWorking)
            {
                Console.WriteLine("Введите команду:\n" +
                    "1 - добавить игрока;\n" +
                    "2 - удалить игрока\n" +
                    "3 - вывести всех игроков на экран\n" +
                    "4 - забанить игрока по ID\n" +
                    "5 - разбанить игрока по ID\n" +
                    "esc - выход");
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        allPlayersData.AddPlayer();
                        break;
                    case "2":
                        allPlayersData.RemovePlayer();
                        break;
                    case "3":
                        allPlayersData.ShowAllPlayers();
                        break;
                    case "4":
                        allPlayersData.BunPlayer();
                        break;
                    case "5":
                        allPlayersData.UnbanPlayer();
                        break;
                    case "esc":
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }
                Console.WriteLine("\nНажмите на любую клавишу.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Player
    {
        private static int _id;

        public bool IsBanned { get; private set; }
        public int Id { get; private set; }
        public int Level { get; private set; }
        public string Nickname { get; private set; }

        public Player(string nickname, int level)
        {
            Id = _id++;
            Nickname = nickname;
            Level = level;
            IsBanned = false;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }

    class Database
    {
        private List<Player> _allPlayers = new List<Player>();

        public void ShowAllPlayers()
        {
            string banStatus;
            foreach (var player in _allPlayers)
            {
                if (player.IsBanned)
                {
                    banStatus = "Забанен";
                }
                else
                {
                    banStatus = "Не забанен";
                }
                Console.WriteLine($"ID - {player.Id}; Nickname - {player.Nickname}; Level - {player.Level}; Ban status - {banStatus}");
            }
        }

        public void AddPlayer()
        {
            Console.WriteLine("Введите ник игрока");
            string name = Console.ReadLine();
            Console.WriteLine("Введите уровень игрока");
            int level;
            if (int.TryParse(Console.ReadLine(), out level))
            {
                _allPlayers.Add(new Player(name, level));
            }
            else
            {
                Console.WriteLine("Введено не коректное значение");
            }
        }

        public void RemovePlayer()
        {
            Console.WriteLine("Введите ID игрока для удаления");
            int id = CheckCorrectId(Console.ReadLine());
            if (id != -1)
            {
                foreach (var player in _allPlayers)
                {
                    if (player.Id == id)
                    {
                        _allPlayers.Remove(player);
                        break;
                    }
                }
            }
        }

        public void BunPlayer()
        {
            Console.WriteLine("Введите ID игрока, которого вы хотите забанить");
            int id = CheckCorrectId(Console.ReadLine());
            if (id != -1)
            {
                foreach (var player in _allPlayers)
                {
                    if (player.Id == id)
                    {
                        player.Ban();
                    }
                }
            }
        }

        public void UnbanPlayer()
        {
            Console.WriteLine("Введите ID игрока, которого вы хотите разбанить");
            int id = CheckCorrectId(Console.ReadLine());
            if (id != -1)
            {
                foreach (var player in _allPlayers)
                {
                    if (player.Id == id)
                    {
                        player.Unban();
                    }
                }
            }
        }

        private int CheckCorrectId(string message)
        {
            int number;
            if (int.TryParse(message, out number))
            {
                foreach (var player in _allPlayers)
                {
                    if (number == player.Id)
                    {
                        return number;
                    }
                }
            }
            Console.WriteLine("ID не корректный");
            return -1;
        }
    }
}
