using System;
using System.Collections.Generic;
using System.Threading;

namespace FightClub
{

    internal class Fighter
    {
        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _health;

        public double health
        {
            get { return _health; }
            set { _health = value; }
        }

        private double _defense;

        public double defense
        {
            get { return _defense; }
            set { _defense = value; }
        }

        private double _damage;

        public double damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        private double _stamina;

        public double stamina
        {
            get { return _stamina; }
            set { _stamina = value; }
        }

        private double _dexterity;

        public double dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; }
        }

        private double _guile;

        public double guile
        {
            get { return _guile; }
            set { _guile = value; }
        }


        public double currentDefense;
        public double currentHealth;
        public double currentDamage;
        public Fighter(string name, double health, double defense, double damage, double stamina, double dexterity, double guile)
        {
            _name = name;
            _defense = defense;
            _damage = damage;

            _stamina = stamina;
            _dexterity = dexterity;
            _health = health;
            _guile = guile;

            currentDefense = defense;
            currentHealth = health + Math.Round(stamina * 5, 2);
            currentDamage = damage + Math.Round(dexterity * 3, 2);
            Math.Round(currentHealth, 2);
            Math.Round(currentDamage, 2);
        }

        public void ShowStats()
        {
            Console.WriteLine($"{name} \n\tHP: {currentHealth} + ({currentHealth - health}) | DEF: {defense} \n\t" +
                $"DMG: {currentDamage} + ({currentDamage - damage}) | STM: {stamina}\n\t" +
                $"DEX: {dexterity} | GL: {guile}");
        }

        public void ShowCurrentHealth()
        {
            //Console.Write($"{name} HP: {health} & DEF: {currentDefense}");
            Console.Write($"{name} HP: {currentHealth}");
        }

        public string GenerateHit()
        {
            Random rnd = new Random();
            List<string> hits = new List<string>()
            {
                "ударил головой",
                "пнул",
                "ультанул в",
                "закаставал китайский павербанк в",
                "морально унизил",
                "отправился в прошлое и подкрался из-за спины",
                "воспользовался техникой тысячилетие боли на",
                "направил ядовитое облако на",
                "уничтожил одним взглядом достоисство противника",
                "послал",
                "зарядил сковородкой"
            };

            return hits[rnd.Next(1, hits.Count)];
        }
        public void TakeDamage(Fighter enemy)
        {
            double realDamage;

            if (currentDefense >= 50)
            {
                realDamage = Math.Round(enemy.damage / 2, 2);
            }
            else
            {
                realDamage = Math.Round(enemy.damage - (enemy.damage * currentDefense * 0.0015), 2);
            }
            Math.Round(realDamage, 2);
            if (currentDefense > 0)
            {
                currentDefense -= Math.Round(defense * 0.3 + defense * enemy.guile * 0.025, 2);
                Math.Round(currentDefense, 2);
            }
            else if (currentDefense < 0)
            {
                currentDefense = 0;
            }

            currentHealth -= realDamage;
            Math.Round(currentHealth, 2);


            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
            if (enemy.currentHealth <= 0)
            {
                enemy.currentHealth = 0;
            }


            Console.WriteLine();
            Console.WriteLine($"{enemy.name} {GenerateHit()} {name} на {realDamage} урона");
            ShowCurrentHealth();
            Console.Write(" | ");
            enemy.ShowCurrentHealth();
            Console.WriteLine();

        }

        public void StartFight(Fighter enemy)
        {
            Console.Clear();
            Console.WriteLine($"{name} VS {enemy.name}\n");
            while (true)
            {
                Thread.Sleep(2000);
                enemy.TakeDamage(this);
                if (enemy.currentHealth <= 0)
                {
                    Console.WriteLine($"\n{name} выйграл!");

                    break;
                }
                else
                {
                    Thread.Sleep(2000);
                    this.TakeDamage(enemy);
                    if (currentHealth <= 0)
                    {
                        Console.WriteLine($"\n{enemy.name} выйграл!");
                        break;
                    }
                }
            }
            currentHealth = health + Math.Round(stamina * 5, 2); ;
            currentDefense = defense;
            currentDamage = damage + Math.Round(dexterity * 3, 2);
            enemy.currentHealth = enemy.health + Math.Round(enemy.stamina * 5, 2);
            enemy.currentDefense = enemy.defense;
            enemy.currentDamage = enemy.damage + Math.Round(enemy.dexterity * 3, 2);
        }
    }
}
