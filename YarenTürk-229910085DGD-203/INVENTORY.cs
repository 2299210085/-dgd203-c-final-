using System;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Console.WriteLine($"{item} added to the inventory.");
        }
        else
        {
            Console.WriteLine($"{item} already exists in the inventory.");
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Console.WriteLine($"{item} removed from the inventory.");
        }
        else
        {
            Console.WriteLine($"{item} does not exist in the inventory.");
        }
    }

    public void ListItems()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            Console.WriteLine("Inventory:");
            foreach (Item item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}

public enum Item
{
    Charm,
    Rune,
    Coin
}

class Program
{
    static void Main(string[] args)
    {
        // Example usage
        Inventory inventory = new Inventory();

        // Add items
        inventory.AddItem(Item.Charm);
        inventory.AddItem(Item.Rune);
        inventory.AddItem(Item.Coin);

        // Try adding the same item again
        inventory.AddItem(Item.Charm);

        // Remove an item
        inventory.RemoveItem(Item.Rune);

        // Try removing a non-existing item
        inventory.RemoveItem(Item.Coin);

        // List items in the inventory
        inventory.ListItems();
    }
}

