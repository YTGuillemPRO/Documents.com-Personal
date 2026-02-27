using System;
using System.Collections.Generic;
using System.Threading;

class Cookie
{
    public string Name;
    public int Level;
    public int MaxHP;
    public int HP;
    public int Attack;

    public Cookie(string name)
    {
        Name = name;
        Level = 1;
        MaxHP = 30;
        HP = MaxHP;
        Attack = 5;
    }

    public void LevelUp()
    {
        Level++;
        MaxHP += 5;
        Attack += 2;
        HP = MaxHP;
    }
}

class Enemy
{
    public string Name;
    public int HP;
    public int Attack;

    public Enemy(string name, int hp, int atk)
    {
        Name = name;
        HP = hp;
        Attack = atk;
    }
}

class Kingdom
{
    public int Gold = 0;
    public int Cookies = 0;
    public int OvenLevel = 1;
    public Cookie Hero;

    public Kingdom()
    {
        Hero = new Cookie("Cookie Héroe");
    }

    public void ProduceResources()
    {
        int goldGain = OvenLevel * 2;
        int cookieGain = OvenLevel * 1;
        Gold += goldGain;
        Cookies += cookieGain;
        Console.WriteLine($"+{goldGain} oro, +{cookieGain} galletas generadas.");
    }

    public void ShowStatus()
    {
        Console.WriteLine("===== REINO =====");
        Console.WriteLine($"Oro: {Gold}");
        Console.WriteLine($"Galletas: {Cookies}");
        Console.WriteLine($"Horno nivel: {OvenLevel}");
        Console.WriteLine();
        Console.WriteLine("===== COOKIE HÉROE =====");
        Console.WriteLine($"Nombre: {Hero.Name}");
        Console.WriteLine($"Nivel: {Hero.Level}");
        Console.WriteLine($"HP: {Hero.HP}/{Hero.MaxHP}");
        Console.WriteLine($"Ataque: {Hero.Attack}");
        Console.WriteLine("========================");
    }
}

class Game
{
    static void Main()
    {
        Console.Title = "Mini Cookie Kingdom (muy simple)";
        Kingdom kingdom = new Kingdom();
        bool running = true;

        Console.WriteLine("Bienvenido a Mini Cookie Kingdom!");
        Console.WriteLine("No es Cookie Run Kingdom real, es un clon MUY simple.");
        Console.WriteLine();

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("Elige una opción:");
            Console.WriteLine("1) Ver reino");
            Console.WriteLine("2) Generar recursos");
            Console.WriteLine("3) Mejorar horno (cuesta 10 oro)");
            Console.WriteLine("4) Subir de nivel a la cookie (cuesta 5 galletas)");
            Console.WriteLine("5) Ir a batalla");
            Console.WriteLine("0) Salir");
            Console.Write("Opción: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    kingdom.ShowStatus();
                    break;
                case "2":
                    kingdom.ProduceResources();
                    break;
                case "3":
                    UpgradeOven(kingdom);
                    break;
                case "4":
                    LevelUpCookie(kingdom);
                    break;
                case "5":
                    Battle(kingdom);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Saliendo del juego...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    static void UpgradeOven(Kingdom k)
    {
        int cost = 10;
        if (k.Gold >= cost)
        {
            k.Gold -= cost;
            k.OvenLevel++;
            Console.WriteLine($"Horno mejorado a nivel {k.OvenLevel}!");
        }
        else
        {
            Console.WriteLine("No tienes suficiente oro.");
        }
    }

    static void LevelUpCookie(Kingdom k)
    {
        int cost = 5;
        if (k.Cookies >= cost)
        {
            k.Cookies -= cost;
            k.Hero.LevelUp();
            Console.WriteLine($"Tu cookie subió a nivel {k.Hero.Level}!");
        }
        else
        {
            Console.WriteLine("No tienes suficientes galletas.");
        }
    }

    static void Battle(Kingdom k)
    {
        Enemy enemy = new Enemy("Slime de Azúcar", 20 + k.Hero.Level * 5, 3 + k.Hero.Level);
        Console.WriteLine($"¡Aparece un {enemy.Name} con {enemy.HP} HP!");

        while (enemy.HP > 0 && k.Hero.HP > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Turno de tu cookie.");
            enemy.HP -= k.Hero.Attack;
            Console.WriteLine($"Tu cookie hace {k.Hero.Attack} de daño. HP enemigo: {Math.Max(enemy.HP, 0)}");

            if (enemy.HP <= 0)
            {
                Console.WriteLine("¡Has ganado la batalla!");
                int goldReward = 10;
                int cookieReward = 3;
                k.Gold += goldReward;
                k.Cookies += cookieReward;
                Console.WriteLine($"Recompensa: +{goldReward} oro, +{cookieReward} galletas.");
                k.Hero.HP = k.Hero.MaxHP;
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Turno del enemigo.");
            k.Hero.HP -= enemy.Attack;
            Console.WriteLine($"{enemy.Name} hace {enemy.Attack} de daño. HP de tu cookie: {Math.Max(k.Hero.HP, 0)}");

            if (k.Hero.HP <= 0)
            {
                Console.WriteLine("Tu cookie ha sido derrotada...");
                Console.WriteLine("Descansando (recuperando vida)...");
                Thread.Sleep(1000);
                k.Hero.HP = k.Hero.MaxHP;
                Console.WriteLine("Tu cookie se ha recuperado.");
                return;
            }

            Console.WriteLine("Pulsa ENTER para siguiente turno...");
            Console.ReadLine();
        }
    }
}
