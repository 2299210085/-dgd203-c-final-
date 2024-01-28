using System;
using System.Collections.Generic;

public class Player
{
    private const int MaxHealth = 100;

    private const int DefaultMinDamage = 5;
    private const int DefaultMaxDamage = 15;

    public string Name { get; private set; }
    public int Health { get; private set; }

    public Inventory Inventory { get; private set; }

    public Player(string name, List<Item> inventoryItems)
    {
        Name = name;
        Health = MaxHealth;
        Inventory = new Inventory();

        foreach (Item item in inventoryItems)
        {
            Inventory.AddItem(item);
        }
    }

    public void TakeItem(Item item)
    {
        Inventory.AddItem(item);
    }

    public void DropItem(Item item)
    {
        // Placeholder for future implementation
    }

    public void PrintInventory()
    {
        foreach (Item item in Inventory.Items)
        {
            Console.WriteLine($"You have a {item}");
        }
    }

    public int Damage(int minDamage, int maxDamage)
    {
        Random damageRandom = new Random();
        return damageRandom.Next(minDamage, maxDamage + 1);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        Health = Math.Max(Health, 0);

        Console.WriteLine($"You take {amount} damage. You have {Health} health left");

        if (Health <= 0)
        {
            Console.WriteLine("YOU DIED");
        }
    }
}
