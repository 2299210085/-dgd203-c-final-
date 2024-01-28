using System;
using System.Numerics;
using System.Collections.Generic;

public class Map
{
    private Game _game;

    private Vector2 _playerPosition;SSS

    private int[] _widthBounds;
    private int[] _heightBounds;

    private Location[] _locations;


    public Map(Game game, int width, int height)
    {
        _game = game;

        // Set width boundaries
        int widthBoundary = (width - 1) / 2;

        _widthBounds = new int[2];
        _widthBounds[0] = -widthBoundary;
        _widthBounds[1] = widthBoundary;

        // Set height boundaries
        int heightBoundary = (height - 1) / 2;

        _heightBounds = new int[2];
        _heightBounds[0] = -heightBoundary;
        _heightBounds[1] = heightBoundary;

        // Set starting coordinates
        _playerPosition = new Vector2(0, 0);

        GenerateLocations();

        // Display result message
        Console.WriteLine($"Created map with size {width}x{height}");
    }

    #region Coordinates

    public Vector2 GetPlayerPosition()
    {
        return _playerPosition;
    }

    public void SetPlayerPosition(Vector2 newPosition)
    {
        _playerPosition = newPosition;
    }

    #endregion

    #region Movement

    public void MovePlayer(int x, int y)
    {
        int targetX = (int)_playerPosition.X + x;
        int targetY = (int)_playerPosition.Y + y;

        if (!CanMoveTo(targetX, targetY))
        {
            Console.WriteLine("You can't go that way");
            return;
        }

        _playerPosition.X = targetX;
        _playerPosition.Y = targetY;

        CheckForLocation(_playerPosition);
    }

    private bool CanMoveTo(int x, int y)
    {
        return !(x < _widthBounds[0] || x > _widthBounds[1] || y < _heightBounds[0] || y > _heightBounds[1]);
    }

    #endregion

    #region Locations

    private void GenerateLocations()
    {
        _locations = new Location[6];

        // Define locations...
    }

    public void CheckForLocation(Vector2 coordinates)
    {
        Console.WriteLine($"You are now at {_playerPosition.X},{_playerPosition.Y}");

        if (IsOnLocation(coordinates, out Location foundLocation))
        {
            if (foundLocation.Type == LocationType.Combat)
            {
                if (foundLocation.CombatAlreadyHappened) return;

                Console.WriteLine("Prepare to fight!");
                Combat combat = new Combat(_game, foundLocation);

                combat.StartCombat();
            }
            else
            {
                Console.WriteLine($"You are in {foundLocation.Name} {foundLocation.Type}");

                if (HasItem(foundLocation))
                {
                    Console.WriteLine($"There is a {foundLocation.ItemsOnLocation[0]} here");
                }
            }
        }
    }

    private bool IsOnLocation(Vector2 coords, out Location foundLocation)
    {
        foreach (Location location in _locations)
        {
            if (location.Coordinates == coords)
            {
                foundLocation = location;
                return true;
            }
        }

        foundLocation = null;
        return false;
    }

    private bool HasItem(Location location)
    {
        return location.ItemsOnLocation.Count != 0;
    }

    public void TakeItemFromLocation(Player player, Vector2 coordinates)
    {
        if (IsOnLocation(coordinates, out Location location))
        {
            if (HasItem(location))
            {
                Item itemOnLocation = location.ItemsOnLocation[0];

                player.TakeItem(itemOnLocation);
                location.RemoveItem(itemOnLocation);

                Console.WriteLine($"You took the {itemOnLocation}");

                return;
            }
        }

        Console.WriteLine("There is nothing to take here!");
    }

    public void RemoveItemFromLocations(Item item)
    {
        foreach (Location location in _locations)
        {
            if (location.ItemsOnLocation.Contains(item))
            {
                location.RemoveItem(item);
            }
        }
    }

    #endregion
}
