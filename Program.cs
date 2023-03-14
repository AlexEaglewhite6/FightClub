using System;
using System.Collections.Generic;
using System.Threading;

namespace FightClub
{
    internal class Program
    {
        public static Random rnd = new Random();
        public static List<Fighter> fighters = new List<Fighter>(6);
        public static string choose;
        static void Main(string[] args)
        {
            Init();
        MainState:
            Console.Clear();
            Console.WriteLine("Выберите режим:"
                              + "\n1 - администратор"
                              + "\n2 - пользователь");
            choose = Console.ReadLine();
            if (choose == "1")
            {
                AdminState();
            }
            else if (choose == "2")
            {
                UserState();
            }
            else
            {
                goto MainState;
            }
        }
        
        public static void Init() 
        {
            GenerateFighter("Oleg");
            GenerateFighter("Aleberto");
            GenerateFighter("MisyaSoupNinja");
            GenerateFighter("PodMonsterPasha");
            GenerateFighter("GoblinKirill");
        }

        #region Fighters
        public static void GenerateFighter(string name)
        {
            double health = Math.Round(rnd.Next(75, 125) + rnd.NextDouble() * 10, 2);
            double defense = Math.Round(rnd.Next(30, 60) + rnd.NextDouble() * 10, 2);
            double damage = Math.Round(rnd.Next(30, 60) + rnd.NextDouble(), 2);
            double stamina = Math.Round(rnd.NextDouble() * 10, 2);
            double dexterity = Math.Round(rnd.NextDouble() * 10, 2);
            double guile = Math.Round(rnd.NextDouble() * 8, 2);
            Fighter fighter = new Fighter(name, health, defense, damage, stamina, dexterity, guile);
            fighters.Add(fighter);
        }

        public static void CreateFighter()
        {
            Console.Clear();
            if (fighters.Count >= 6)
            {
                Console.WriteLine("Достигнут максимум бойцов!"
                                  + "\n\nЕсли Вы хотите создать нового бойца,"
                                  + "\n\nУдалите хотя бы одного старого!\n");
            }
            else
            {
                Console.WriteLine("Добро пожаловать в редактор создания!\n");
                Console.WriteLine("Как будут звать бойца?");
                string name = Console.ReadLine();
                Console.WriteLine("Какой у него запас здоровья?");
                double health = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Какое у него запас брони?");
                double defense = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Как больно он бьет?");
                double damage = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Его выносливость?");
                double stamina = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Насколько он ловкий?");
                double dexterity = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Насколько он хитрый? (Максимум 8)");
                double guile = Convert.ToDouble(Console.ReadLine());
            guileValue:
                if (guile > 8)
                {
                    Console.WriteLine("Неправильное значение!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto guileValue;
                }

                fighters.Add(new Fighter(name, health, defense, damage, stamina, dexterity, guile));

                Console.WriteLine($"\nПоздравляю, Вы создали {name}");

            }
        }

        public static void ShowFighers()
        {
            Console.Clear();
            Console.WriteLine("Список доступных бойцов:\n");
            foreach (var f in fighters)
            {
                Console.Write($"{fighters.IndexOf(f) + 1}\t"); f.ShowStats();
                Console.WriteLine();
            }
        }

        public static void ChooseFighters()
        {
            Console.Clear();
            ShowFighers();
            int f1, f2;
            Console.WriteLine("Выберите номера бойцов\n");
            Console.Write("Боец 1 : ");
            f1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Боец 2 : ");
            f2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            fighters[f1 - 1].StartFight(fighters[f2 - 1]);
        }

        public static void DeleteFighter()
        {
            Console.Clear();
            if (fighters.Count <= 0)
            {
                Console.WriteLine("Вы не можете удалить того, чего нет!"
                                  + "\nЕсли хочется кого-то удалить, сначала создайте его!");
            }
            else
            {
                ShowFighers();
                Console.Write("Выберите номер бойца, которого хотите удалить: ");
                choose = Console.ReadLine();
                Console.WriteLine($"Вы удалили {fighters[Convert.ToInt32(choose) - 1]}");
                fighters.RemoveAt(Convert.ToInt32(choose) - 1);
            }
        }
        #endregion

        #region States
        public static void ReturnToState()
        {
            Console.WriteLine("Нажмите любую клавишу чтобы выйти . . .");
            Console.ReadLine();
        }

        public static void AdminState()
        {
            Console.Clear();

            Console.WriteLine("Выберите действие:"
                              + "\n1 - Создать бойца"
                              + "\n2 - Удалить бойца"
                              + "\n3 - Посмотрееть список бойцов"
                              + "\n4 - Перейти в режим Пользователь"
                              + "\n5 - Выйти");

            choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    CreateFighter();
                    ReturnToState();
                    AdminState();
                    break;
                case "2":
                    DeleteFighter();
                    ReturnToState();
                    AdminState();
                    break;
                case "3":
                    ShowFighers();
                    ReturnToState();
                    AdminState();
                    break;
                case "4":
                    UserState();
                    break;
                case "5":
                    return;
                default:
                    AdminState();
                    break;
            }
        }

        public static void UserState()
        {
            Console.Clear();

            Console.WriteLine("Выберите действие:"
                              + "\n1 - Посмотреть список бойцов"
                              + "\n2 - Выбрать бойцов"
                              + "\n3 - Перейти в режим Админ"
                              + "\n4 - Выйти");

            choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    ShowFighers();
                    ReturnToState();
                    UserState();
                    break;
                case "2":
                    ChooseFighters();
                    ReturnToState();
                    UserState();
                    break;
                case "3":
                    AdminState();
                    break;
                case "4":
                    return;
                default:
                    UserState();
                    break;
            }
        }

        #endregion
    }
}
