using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private GameMap gameMap;
        private Monster currentMonster;


        public Game()
        {

            // gets the user to input their name

            Console.WriteLine("Enter your name to get started: ");

            string name = Console.ReadLine();


            // checks if the user has entered a valid name that isn't empty

            while (string.IsNullOrWhiteSpace(name))
            {

                Console.WriteLine("Please enter a valid name");
                Console.WriteLine("Enter a name: ");
                name = Console.ReadLine();

            }

            Console.WriteLine("\nYou are in the dungeon now. " +
                "\nThere are items scattered around that will help you win the game by defeating the monsters." +
                "\nGood luck!!!");

            Console.WriteLine("\nStarting room: There is a wooden sword and a health potion.\n");

            // creates a new player object with the name the user has entered
            player = new Player(name, 100);

            // creates a new room object with the description of the room
            Room startRoom1 = new Room("You are in the starting room.");

            startRoom1.AddItem(new Weapon("Wooden Sword", 30));
            startRoom1.AddItem(new Potion("Health Potion", 30));


            Room goblinRoom = new Room("\n You are in the goblins lair!.\n");

            goblinRoom.AddMonster(new Goblin());

            Room dragonRoom = new Room("\nDragons den: The game boss is here!.\n");

            dragonRoom.AddMonster(new Dragon());

            gameMap = new GameMap(startRoom1);

            gameMap.Rooms.Add(goblinRoom);
            gameMap.Rooms.Add(dragonRoom);

        }



        public void Start()
        {

            // The game loop
            while (true)

            {

                if (gameMap.CurrentRoom.Monsters.Any())
                {
                    StartCombat();
                }
                else
                {
                    DisplayRoomOptions();
                }
            }

        }

        private void StartCombat()
        {
            bool inCombat = true;
            currentMonster = gameMap.CurrentRoom.Monsters.First();

            Console.WriteLine($"{currentMonster.Name} is attacking you!");

            while (inCombat)
            {
                Console.WriteLine("\n Select a number" +
                    "\n1. Attack" +
                    "\n2. Use an item" +
                    "\n3. Flee\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        player.Attack(currentMonster);

                        if (currentMonster.Health <= 0)
                        {
                            Console.WriteLine($"You have defeated {currentMonster.Name}");

                            if (currentMonster is Goblin)

                            {
                                Console.WriteLine("The goblin has droped a Super Potion and an Iron Sword");

                                gameMap.CurrentRoom.Items.Add(new Potion("Super Potion", 50));
                                gameMap.CurrentRoom.Items.Add(new Weapon("Iron Sword", 75));

                            }

                            gameMap.CurrentRoom.Monsters.Remove(currentMonster);
                            inCombat = false; // Exit combat loop
                        }

                        else
                        {
                            currentMonster.Attack(player);
                            CheckPlayerHealth();
                        }
                        break;
                

                    case "2":
                        
                        player.ViewInventory();

                        Console.WriteLine("Enter the name of the item to use:");

                        string itemName = Console.ReadLine();

                        if (player.HasItem(itemName))
                        {
                            player.UseItem(itemName);
                        }
                        else
                        {
                            Console.WriteLine("You don't have that item.");
                        }

                        currentMonster.Attack(player);
                        CheckPlayerHealth();

                        break;


                    case "3":
                        Console.WriteLine("You fled the battle!");
                        inCombat = false; 

                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        
        
        private void DisplayRoomOptions()

        {
            Console.WriteLine(gameMap.CurrentRoom.Description);
            Console.WriteLine("\nSelect a number" +
                "\n1. Search room" +
                "\n2. Move to the next room" +
                "\n3. Inventory" +
                "\n4. Quit\n");
            string choice = Console.ReadLine();

            switch (choice)
            {

                case "1":
                    Console.WriteLine("\nYou found the following items:\n");
                    foreach (var item in gameMap.CurrentRoom.Items)
                    {
                        Console.WriteLine($"- {item.Name}");
                        item.Collect(player);
                    }
                    gameMap.CurrentRoom.Items.Clear();
                    break;

                case "2":

                    int currentIndex = gameMap.Rooms.IndexOf(gameMap.CurrentRoom);
                    if (currentIndex == gameMap.Rooms.Count - 1)
                    {
                        Console.WriteLine("You win!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        gameMap.CurrentRoom = gameMap.Rooms[currentIndex + 1];
                        Console.WriteLine($"You moved to the next room: {gameMap.CurrentRoom.Description}");
                    }
                    break;

                case "3":

                    player.ViewInventory();
                    break;

                case "4":
                    Console.WriteLine("Thanks for playing! Goodbye");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }


        private void CheckPlayerHealth()
        {
            if (player.Health <= 0)
            {
                Console.WriteLine("Game Over!");
                Environment.Exit(0);
            }
        }

    }
    
}