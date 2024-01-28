using System;
using System.Collections.Generic;
using System.Numerics;

public class Location
{
    #region VARIABLES

    public string Name { get; private set; } // Location name
    public Vector2 Coordinates { get; private set; } // Coordinates
    public LocationType Type { get; private set; } // Location type

    // CombatAlreadyHappened property is meaningful only for locations of Combat type.
    // Therefore, let's set it only for Combat type.
    private bool combatAlreadyHappened;
    public bool CombatAlreadyHappened
    {
        get => Type == LocationType.Combat && combatAlreadyHappened;
        private set => combatAlreadyHappened = value;
    }

    // ItemsOnLocation list should not be accessible from outside of the class.
    // Therefore, let's set the access modifier of the set to private.
    public List<Item> ItemsOnLocation { get; private set; }

    #endregion

    #region CONSTRUCTOR

    public Location(string locationName, LocationType type, Vector2 coordinates, List<Item> itemsOnLocation = null)
    {
        Name = locationName;
        Type = type;
        Coordinates = coordinates;

        ItemsOnLocation = itemsOnLocation ?? new List<Item>();

        CombatAlreadyHappened = Type == LocationType.Combat ? false : true;
    }

    #endregion

    #region METHODS

    public void RemoveItem(Item item)
    {
        if (ItemsOnLocation.Contains(item))
        {
            ItemsOnLocation.Remove(item);
        }
        else
        {
            Console.WriteLine($"{item} is not found in {Name}.");
        }
    }

    public void CombatHappened()
    {
        if (Type == LocationType.Combat)
        {
            CombatAlreadyHappened = true;
        }
    }

    #endregion
}

public enum LocationType
{
    City,
    Combat,
    Cave
}

public enum Item
{
    Sword,
    Shield,
    Potion
}
