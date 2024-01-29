using System;
using System.IO;
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

        private void GetPlayerName()
        {
            Console.WriteLine("Welcome to the sea that has most malwareous droids!");
            Console.WriteLine("Just Checking for preocedures? Of course he will not come back but,");
            Console.WriteLine("Are you Onur? If not, please provide your ClassID");
            _playerName = Console.ReadLine();

            if (_playerName.Equals("Onur", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\r\n█▀█ █▀▄▀█ █▀▀ █   ▀█▀ █░█ █▀▀   █▀▀ █░█ █▀█ █▀█ █▀ █▀▀ █▄░█   █▀█ █▄░█ █▀▀ █\r\n█▄█ █░▀░█ █▄█ ▄   ░█░ █▀█ ██▄   █▄▄ █▀█ █▄█ █▄█ ▄█ ██▄ █░▀█   █▄█ █░▀█ ██▄ ▄");
                Console.WriteLine("\r\n▀█▀ █░█ █▀▀   █▀█ █▄░█ █▀▀   █░█░█ █░█ █▀█   █▄▀ █▄░█ █▀█ █░█░█ █▀   █░█ █▀█ █░█░█   ▀█▀ █▀█   █░█ █▀ █▀▀\r\n░█░ █▀█ ██▄   █▄█ █░▀█ ██▄   ▀▄▀▄▀ █▀█ █▄█   █░█ █░▀█ █▄█ ▀▄▀▄▀ ▄█   █▀█ █▄█ ▀▄▀▄▀   ░█░ █▄█   █▄█ ▄█ ██▄\r\n\r\n▄█ █▀█ ▀█ ░ ▄█ █▄▄ ░ ▄█ ░ ▄█\r\n░█ ▀▀█ █▄ ▄ ░█ █▄█ ▄ ░█ ▄ ░█");
                Console.WriteLine("\r\n█▀▀ ▄▀█ █▀▄▀█ █▀▀   █ █▄░█ █▀▀ █▀█\r\n█▄█ █▀█ █░▀░█ ██▄   █ █░▀█ █▀░ █▄█");
                Console.WriteLine("Your journey started in a vehicle called Zhip that floats on water and contains supplies. " +
                   "\r\nYou are an experienced item owner swapper who wants to retire. " +
                   "\r\nYou heard that the place called Neural Plaza is the perfect place for retired sealers, " +
                   "\r\nSo you decided to go on one last journey.");
            }
            else if (_playerName == "")
            {
                Console.WriteLine("AUTOFILLING THE ClassID:Corum7387381Si");
                _playerName = "Corum7387381Si";
                Console.WriteLine("\r\n█▀▀ ▄▀█ █▀▄▀█ █▀▀   █ █▄░█ █▀▀ █▀█\r\n█▄█ █▀█ █░▀░█ ██▄   █ █░▀█ █▀░ █▄█");
                Console.WriteLine("Your journey started in a vehicle called Zhip that floats on water and contains supplies. " +
                   "\r\nYou are an experienced item owner swapper who wants to retire. " +
                   "\r\nYou heard that the place called Neural Plaza is the perfect place for retired sealers, " +
                   "\r\nSo you decided to go on one last journey.");
            }
            else
            {
                Console.WriteLine($"UserID saved: {_playerName}");
                Console.WriteLine("\r\n█▀▀ ▄▀█ █▀▄▀█ █▀▀   █ █▄░█ █▀▀ █▀█\r\n█▄█ █▀█ █░▀░█ ██▄   █ █░▀█ █▀░ █▄█");
                Console.WriteLine($"If you're lost for words, just say help. It will help you remember");
                Console.WriteLine("Your journey started in a vehicle called Zhip that floats on water and contains supplies. " +
                     "\r\nYou are an experienced item owner swapper who wants to retire. " +
                     "\r\nYou heard that the place called Neural Plaza is the perfect place for retired sealers, " +
                     "\r\nSo you decided to go on one last journey.");
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
            if (_playerInput == "" || _playerInput == null)
            {
                Console.WriteLine("I guess you forgot what to say. A help will be usefull!");
                return;
            }

            switch (_playerInput)
            {
                case "W":
                    _gameMap.MovePlayer(0, 1);
                    break;
                case "S":
                    _gameMap.MovePlayer(0, -1);
                    break;
                case "D":
                    _gameMap.MovePlayer(1, 0);
                    break;
                case "A":
                    _gameMap.MovePlayer(-1, 0);
                    break;
                case "settle":
                    EndGame();
                    Console.WriteLine($"\r\n██╗░░░██╗░█████╗░██╗░░░██╗  ░█████╗░██████╗░███████╗\r\n╚██╗░██╔╝██╔══██╗██║░░░██║  ██╔══██╗██╔══██╗██╔════╝\r\n░╚████╔╝░██║░░██║██║░░░██║  ███████║██████╔╝█████╗░░\r\n░░╚██╔╝░░██║░░██║██║░░░██║  ██╔══██║██╔══██╗██╔══╝░░\r\n░░░██║░░░╚█████╔╝╚██████╔╝  ██║░░██║██║░░██║███████╗\r\n░░░╚═╝░░░░╚════╝░░╚═════╝░  ╚═╝░░╚═╝╚═╝░░╚═╝╚══════╝\r\n\r\n░██████╗███████╗████████╗████████╗██╗░░░░░███████╗██████╗░██╗  ░██████╗░░█████╗░███╗░░░███╗███████╗\r\n██╔════╝██╔════╝╚══██╔══╝╚══██╔══╝██║░░░░░██╔════╝██╔══██╗██║  ██╔════╝░██╔══██╗████╗░████║██╔════╝\r\n╚█████╗░█████╗░░░░░██║░░░░░░██║░░░██║░░░░░█████╗░░██║░░██║██║  ██║░░██╗░███████║██╔████╔██║█████╗░░\r\n░╚═══██╗██╔══╝░░░░░██║░░░░░░██║░░░██║░░░░░██╔══╝░░██║░░██║╚═╝  ██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░\r\n██████╔╝███████╗░░░██║░░░░░░██║░░░███████╗███████╗██████╔╝██╗  ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗\r\n╚═════╝░╚══════╝░░░╚═╝░░░░░░╚═╝░░░╚══════╝╚══════╝╚═════╝░╚═╝  ░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝\r\n\r\n███████╗███╗░░██╗██████╗░███████╗██████╗░\r\n██╔════╝████╗░██║██╔══██╗██╔════╝██╔══██╗\r\n█████╗░░██╔██╗██║██║░░██║█████╗░░██║░░██║\r\n██╔══╝░░██║╚████║██║░░██║██╔══╝░░██║░░██║\r\n███████╗██║░╚███║██████╔╝███████╗██████╔╝\r\n╚══════╝╚═╝░░╚══╝╚═════╝░╚══════╝╚═════╝░\r\n\r\n██████╗░███████╗░█████╗░░█████╗░███████╗███████╗██╗░░░██╗██╗░░░░░██╗░░░░░██╗░░░██╗  ░░██╗██████╗░\r\n██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝██║░░░██║██║░░░░░██║░░░░░╚██╗░██╔╝  ░██╔╝╚════██╗\r\n██████╔╝█████╗░░███████║██║░░╚═╝█████╗░░█████╗░░██║░░░██║██║░░░░░██║░░░░░░╚████╔╝░  ██╔╝░░█████╔╝\r\n██╔═══╝░██╔══╝░░██╔══██║██║░░██╗██╔══╝░░██╔══╝░░██║░░░██║██║░░░░░██║░░░░░░░╚██╔╝░░  ╚██╗░░╚═══██╗\r\n██║░░░░░███████╗██║░░██║╚█████╔╝███████╗██║░░░░░╚██████╔╝███████╗███████╗░░░██║░░░  ░╚██╗██████╔╝\r\n╚═╝░░░░░╚══════╝╚═╝░░╚═╝░╚════╝░╚══════╝╚═╝░░░░░░╚═════╝░╚══════╝╚══════╝░░░╚═╝░░░  ░░╚═╝╚═════╝░");
                    break;
                case "save":
                    SaveGame();
                    Console.WriteLine("Game saved");
                    break;
                case "load":
                    LoadGame();
                    Console.WriteLine("Game loaded");
                    break;
                case "help":
                    Console.WriteLine(HelpMessage());
                    break;
                case "where":
                    _gameMap.CheckForLocation(_gameMap.GetCoordinates());
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "who":
                    Console.WriteLine($"Your UserID:{Player.Name}, Don't forget to save it if you want o keep it.");
                    break;
                case "open":
                    _gameMap.TakeItem(Player, _gameMap.GetCoordinates());
                    break;
                case "inventory":
                    Player.CheckInventory();
                    break;
                default:
                    Console.WriteLine("Command not recognized. Please type 'help' for a list of available commands");
                    break;
            }
        }

        private void CheckPlayerPulse()
        {
            if (Player.Health <= 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            Console.WriteLine($"\r\n█▀▀ █▀█ █▀█ █▀█ █▀█   █▀▀ █▀█ █▄░█ █▀ █▀█ █░░ █▀▀ ▀\r\n██▄ █▀▄ █▀▄ █▄█ █▀▄   █▄▄ █▄█ █░▀█ ▄█ █▄█ █▄▄ ██▄ ▄");
            Console.WriteLine($"OperatingSystem/Users/{Player.Name}, GameEnded set player mood == wants to play again directory: Class/Students/id:225040067TahaAlperenDogan's game!");
            Console.WriteLine($"\r\n█▀▀ ▀▄▀ █ ▀█▀\r\n██▄ █░█ █ ░█░");
            _gameRunning = false;
        }

        #endregion

        #region Save Management

        private void LoadGame()
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

            for (int i = 0; i < itemStrings.Count; i++)
            {
                if (Enum.TryParse(itemStrings[i], out Item result))
                {
                    Item item = result;
                    _loadedItems.Add(item);
                    _gameMap.RemoveItemFromLocation(item);
                }
            }

            _gameMap.SetCoordinates(coordArray);

        }

        private void SaveGame()
        {
            // Player Coordinates
            string xCoord = _gameMap.GetCoordinates()[0].ToString();
            string yCoord = _gameMap.GetCoordinates()[1].ToString();
            string playerCoords = $"{xCoord},{yCoord}";

            // Player inventory
            List<Item> items = Player.Inventory.Items;
            string playerItems = "";
            for (int i = 0; i < items.Count; i++)
            {
                playerItems += items[i].ToString();
                
                if(i != items.Count -1)
                {
                    playerItems += ",";
                }
            }

            string saveContent = $"{_playerName}{Environment.NewLine}{playerCoords}{Environment.NewLine}{playerItems}";

            string path = SaveFilePath();

            File.WriteAllText(path, saveContent);
        }

        private string SaveFilePath()
        {
            // Get the save file path
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string path = projectDirectory + @"\save.txt";

            return path;
        }

        #endregion

        #region Miscellaneous

        private string HelpMessage()
        {
            return @"
█▄█ █▀█ █░█   █▀█ █▀▀ █▀▄▀█ █▀▀ █▀▄▀█ █▄▄ █▀▀ █▀█ █▀▀ █▀▄
░█░ █▄█ █▄█   █▀▄ ██▄ █░▀░█ ██▄ █░▀░█ █▄█ ██▄ █▀▄ ██▄ █▄▀:
W: go north
S: go south
A: go west
D: go east
load: Load saved game
save: save current game
inventory: view your inventory
open: open the chest
who: view the player information
where: view current location
clear: clear the screen";

        }

        #endregion

        #endregion
    }
}