using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace DGD203_2
{
    public class Game
    {
        #region VARIABLES

        #region Game Constants

        private const int _defaultMapWidth = 5;
        private const int _defaultMapHeight = 5;

        #endregion

        #region Game Variables

        #region Player Variables

        public Player Player { get; private set; }

        private string _playerName;
        private List<Item> _loadedItems;

        #endregion

        #region World Variables

        private Location[] _locations;

        #endregion

        private bool _gameRunning;
        private Map _gameMap;
        private string _playerInput;

        #endregion

        #endregion

        #region METHODS

        #region Initialization

        public void StartGame(Game gameInstanceReference)
        {
            // Generate game environment
            CreateNewMap();

            // Load game
            LoadGame();

            // Deal with player generation
            CreatePlayer();

            InitializeGameConditions();

            _gameRunning = true;
            StartGameLoop();
        }

        private void CreateNewMap()
        {
            _gameMap = new Map(this, _defaultMapWidth, _defaultMapHeight);
        }

        private void CreatePlayer()
        {
            try
            {
                if (_playerName == null)
                {
                    GetPlayerName();
                }

                if (_loadedItems == null)
                {
                    _loadedItems = new List<Item>();
                }

                // _playerName may be null. It would be a good idea to put a check here.
                Player = new Player(_playerName, _loadedItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating player: {ex.Message}");
            }
        }

        private void GetPlayerName()
        {
            Console.WriteLine("Welcome to the most awesomest RPG game of all time!");
            Console.WriteLine("Would you be kind enough to provide us with your name?");
            _playerName = Console.ReadLine();

            if (_playerName == "Johnny")
            {
                Console.WriteLine($"Here comes {_playerName}!!");
            }
            else if (string.IsNullOrEmpty(_playerName))
            {
                Console.WriteLine("Player name not entered, giving the name John Doe");
                _playerName = "John Doe";
            }
            else
            {
                Console.WriteLine($"Pleased to meet you {_playerName}, we will have a great adventure together!");
            }
        }

        private void InitializeGameConditions()
        {
            _gameMap.CheckForLocation(_gameMap.GetCoordinates());
        }


        #endregion

        #region Game Loop

        private void StartGameLoop()
        {
            while (_gameRunning)
            {
                GetInput();
                ProcessInput();
                CheckPlayerPulse();
            }
        }

        private void GetInput()
        {
            _playerInput = Console.ReadLine();
        }

        private void ProcessInput()
        {
            if (string.IsNullOrEmpty(_playerInput))
            {
                Console.WriteLine("Give me a command!");
                return;
            }

            switch (_playerInput.ToUpper())
            {
                case "U":
                    _gameMap.MovePlayer(0, 1);
                    break;
                case "D":
                    _gameMap.MovePlayer(0, -1);
                    break;
                case "R":
                    _gameMap.MovePlayer(1, 0);
                    break;
                case "L":
                    _gameMap.MovePlayer(-1, 0);
                    break;
                case "EXIT":
                    EndGame();
                    break;
                case "SAVE":
                    SaveGame();
                    Console.WriteLine("Game saved");
                    break;
                case "LOAD":
                    LoadGame();
                    Console.WriteLine("Game loaded");
                    break;
                case "HELP":
                    Console.WriteLine(HelpMessage());
                    break;
                case "WHERE":
                    _gameMap.CheckForLocation(_gameMap.GetCoordinates());
                    break;
                case "CLEAR":
                    Console.Clear();
                    break;
                case "WHO":
                    Console.WriteLine($"You are {Player.Name}, the mighty hero of the Isles");
                    break;
                case "TAKE":
                    _gameMap.TakeItem(Player, _gameMap.GetCoordinates());
                    break;
                case "INVENTORY":
                    Player.CheckInventory();
                    break;
                default:
                    Console.WriteLine("Command not recognized. Please type 'help' for a list of available commands");
                    break;
            }
        }

        private void CheckPlayerPulse()
        {
            if (Player.Health <= 0 || Player.Inventory.Items.Contains(Item.None))
            {
                Console.WriteLine("You have perished! Game over.");
                EndGame();
            }
        }

        private void EndGame()
        {
            Console.WriteLine("Exiting the game. We hope you enjoyed our adventure!");
            _gameRunning = false;
        }

        #endregion

        #region Save Management

        private void LoadGame()
        {
            try
            {
                string path = SaveFilePath();

                if (!File.Exists(path)) return;

                // Reading the file contents
                string[] saveContent = File.ReadAllLines(path);

                // Set the player name
                _playerName = saveContent[0];

                // Set player coordinates
                List<int> coords = saveContent[1].Split(',').Select(int.Parse).ToList();
                Vector2 coordArray = new Vector2(coords[0], coords[1]);

                // Set player inventory
                _loadedItems = new List<Item>();

                List<string> itemStrings = saveContent[2].Split(',').ToList();

                foreach (var itemString in itemStrings)
                {
                    if (Enum.TryParse(itemString, out Item result))
                    {
                        Item item = result;
                        _loadedItems.Add(item);
                        _gameMap.RemoveItemFromLocation(item);
                    }
                }

                _gameMap.SetCoordinates(coordArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
            }
        }

        private void SaveGame()
        {
            try
            {
                // Player Coordinates
                string xCoord = _gameMap.GetCoordinates()[0].ToString();
                string yCoord = _gameMap.GetCoordinates()[1].ToString();
                string playerCoords = $"{xCoord},{yCoord}";

                // Player inventory
                List<Item> items = Player.Inventory.Items;
                string playerItems = string.Join(",", items);

                string saveContent = $"{_playerName}{Environment.NewLine}{playerCoords}{Environment.NewLine}{playerItems}";

                string path = SaveFilePath();

                File.WriteAllText(path, saveContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        private string SaveFilePath()
        {
            // Get the save file path
            string saveFileName = "save.txt";
            string saveDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveFileName);

