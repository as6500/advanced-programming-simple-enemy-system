
using System;
using System.Collections.Generic;

class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public Enemy(string name, int health, int damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }

    // attack method
    public virtual void Attack(Player player)
    {
        Console.WriteLine($"{Name} attacks the player for {Damage} damage!");
        player.Health -= Damage;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
            Health = 0;

        Console.WriteLine($"{Name} takes {damage} damage!");
    }

    public bool IsDefeated()
    {
        return Health <= 0;
    }
}
class Goblin : Enemy
{
    public Goblin() : base("Goblin", 30, 5)
    {
    }

    // fast attack
    public override void Attack(Player player)
    {
        int damage = Damage + 3;

        Console.WriteLine("The Goblin performs a quick attack!");
        Console.WriteLine($"The Goblin deals {damage} damage!");

        player.Health -= damage;
    }
}

// orc
class Orc : Enemy
{
    public Orc() : base("Orc", 60, 12)
    {
    }

    // powerful attack
    public override void Attack(Player player)
    {
        int damage = Damage + 5;

        Console.WriteLine("The Orc performs a powerful attack!");
        Console.WriteLine($"The Orc deals {damage} damage!");

        player.Health -= damage;
    }
}

//player
class Player
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public Player(string name)
    {
        Name = name;
        Health = 100;
        Damage = 15;
    }

    public void Attack(Enemy enemy)
    {
        Console.WriteLine($"\n{Name} attacks the {enemy.Name}!");
        enemy.TakeDamage(Damage);

        if (enemy.IsDefeated())
        {
            Console.WriteLine($"\n*** {enemy.Name} has been defeated! ***");
        }
    }

    public void ShowStats()
    {
        Console.WriteLine($"\nPlayer: {Name}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine($"Damage: {Damage}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== TEXT RPG GAME ===");

        // creates player
        Player player = new Player("Hero");

        // creates enemies
        List<Enemy> enemies = new List<Enemy>();

        enemies.Add(new Goblin());
        enemies.Add(new Orc());
        enemies.Add(new Goblin());

        Console.WriteLine("\nEnemies have appeared!");

        // game loop
        foreach (Enemy enemy in enemies)
        {
            Console.WriteLine("\n----------------------");
            Console.WriteLine($"A {enemy.Name} appears!");
            Console.WriteLine($"Enemy Health: {enemy.Health}");

            // battle
            while (!enemy.IsDefeated() && player.Health > 0)
            {
                player.ShowStats();

                Console.WriteLine($"\n{enemy.Name} Health: {enemy.Health}");
                Console.WriteLine("\n1. Attack");
                Console.WriteLine("2. Run away");

                Console.Write("Choose an action: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    player.Attack(enemy);

                    // enemy attacks if still alive
                    if (!enemy.IsDefeated())
                    {
                        enemy.Attack(player);
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("You ran away!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine("\nYou have been defeated!");
                    return;
                }
            }
        }

        Console.WriteLine("\n=== GAME OVER ===");
        Console.WriteLine("Y1ou survived all the enemies!");
        player.ShowStats();
    }
}
