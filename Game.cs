using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{

    /* This is the main class that runs the game
     * In this class the user can choose the options to be able to play the game 
     * When the user chooses an option the game runs the mothods from different classes to run the game
     */



    internal class Game
    {
        private Player player;
        private GameMap gameMap;
        private Monster currentMonster;

        // This is the constructor for the game class
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

            Console.WriteLine("\nYou are in the dark and gloomy dungeon now. " +
                "\nThere are items scattered around that will help you win the game by defeating the monsters." +
                "You must leave the dungeon by defeating the monster to win." +
                "\nGood luck!!!");

            Console.WriteLine("\nStarting room: There is a wooden sword and a health potion.\n");

            // creates a new player object with the name the user has entered
            player = new Player(name, 100);

            // creates a new room object with the description of the room
            Room startRoom1 = new Room("You are at the enterence to the mysterious dungeon.");

            startRoom1.AddItem(new Weapon("Wooden Sword", 20));
            startRoom1.AddItem(new Potion("Health Potion", 30));


            Room goblinRoom = new Room("\nYou are in the goblins lair! This is a cold and dark room filled with danger.\n");

            goblinRoom.AddMonster(new Goblin());

            Room dragonRoom = new Room("\nDragons den: The game boss is here!.\n");

            dragonRoom.AddMonster(new Dragon());

            // creates a new game map object with the starting room
            gameMap = new GameMap(startRoom1);

            // adds the rooms to the game map
            gameMap.Rooms.Add(goblinRoom);
            gameMap.Rooms.Add(dragonRoom);

        }

        // This is the main method that runs the game
        public void Start() 
        {
            try
            {

                while (true)
                {
                    DisplayRoomOptions();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred and the game crashed: {ex.Message}");
                Console.WriteLine("Please restart the game.");
            }
        }


        // This method displays the options for the user to choose from
        // The reason this is a method is to make it look less cluttererd because of the try-catch
        private void DisplayRoomOptions() 
        {

            Console.WriteLine(gameMap.CurrentRoom.Description);

            var options = new List<string>();

            // This hides the option to fight the monster if there arn't any monster in the room
            if (gameMap.CurrentRoom.Monsters.Any())
            {
                options.Add("\n1. Fight monster");
            }

            options.AddRange(new[]
            {
                "2. Search room",
                "3. Move to the next room",
                "4. Inventory",
                "5. Use an item",
                "6. Quit\n"
            });

            Console.WriteLine("\n" + string.Join("\n", options));
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1" when gameMap.CurrentRoom.Monsters.Any():
                    
                    try 
                    {
                        StartCombat();
                    }
                    
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred during combat: {ex.Message}");
                        Console.WriteLine("Please restart the game.");
                    }

                    break;

                case "2":

                    // Lets the user to search the room for items
                    try
                    {
                        Console.WriteLine("\nYou found the following items:\n");
                        foreach (var item in gameMap.CurrentRoom.Items)
                        {
                            Console.WriteLine($"- {item.Name}");
                            item.Collect(player);
                        }
                        gameMap.CurrentRoom.Items.Clear();
                         
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred whilst searching the room: {ex.Message}");
                        Console.WriteLine("Please restart the game.");
                    }
                    break;


                case "3":

                    try 
                    {
                        // This stops the user from just moving on without fighting the monster
                        if (gameMap.CurrentRoom.Monsters.Any())
                        {
                            Console.WriteLine("You can't move to the next room while there are monsters present!");
                            break;
                        }

                        else 
                        {
                            // Lets the user to move to the next room
                            // The game finishes if the user is in the last room
                            int currentIndex = gameMap.Rooms.IndexOf(gameMap.CurrentRoom);
                            if (currentIndex == gameMap.Rooms.Count - 1)
                            {
                                Console.WriteLine("\n Congratulations! You won!");
                                Environment.Exit(0);
                            }
                            else
                            {
                                gameMap.CurrentRoom = gameMap.Rooms[currentIndex + 1];
                                Console.WriteLine($"You moved to the next room: ");
                            }
                       
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while moving to the next room: {ex.Message}");
                        Console.WriteLine("Please restart the game.");
                    }
                    break;




                case "4":

                    try 
                    {
                        // This lets the user to view their inventory
                        player.ViewInventory();
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while viewing the inventory: {ex.Message}");
                        Console.WriteLine("Please restart the game.");
                    }
                    break;


                case "5":

                    try 
                    {
                        // This lets the user to use an item from the inventory by typing the name of it
                        player.ViewInventory();
                        Console.WriteLine("\nEnter the name of the item to use or type 'cancel':");
                        string itemName = Console.ReadLine();
                        if (itemName.ToLower() != "cancel")
                        {
                            player.UseItem(itemName);
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while using an item: {ex.Message}");
                        Console.WriteLine("Please restart the game.");
                    }
                    break;

                case "6":

                    try 
                    {
                        // Lets the player just leave if they want to quit playing
                        Console.WriteLine("Thanks for playing! Goodbye");
                        Environment.Exit(0);
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while quitting the game: {ex.Message}");
                        Console.WriteLine("Please restart the game.");
                    }
                    break;

                default:

                    // Makes sure the user selects a valid option and stops the game crashing
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }

            
            
        }

        // This method starts the combat with the monster
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
                    // This allows the player to attack
                    case "1":

                        player.Attack(currentMonster);

                        if (currentMonster.Health <= 0)
                        {
                            Console.WriteLine($"You have defeated {currentMonster.Name}");

                            if (currentMonster is Goblin)
                                // This gives extra items as a reward for beating the goblin

                            {
                                Console.WriteLine("The goblin has droped a Super Potion and an Iron Sword");

                                gameMap.CurrentRoom.Items.Add(new Potion("Super Potion", 50));
                                gameMap.CurrentRoom.Items.Add(new Weapon("Iron Sword", 70));

                            }

                            else if (currentMonster is Dragon)
                            {
                                // This exds the game after beating the dragon so that it doesnt just go back to the options
                                Console.WriteLine("\nCONGRATULATION !!! You have won by defeating the game boss !!!");
                                Environment.Exit(0);

                            }

                            gameMap.CurrentRoom.Monsters.Remove(currentMonster);
                            // This removes the monster after it dies

                            inCombat = false; 
                        }

                        else
                        {
                            currentMonster.Attack(player);
                            CheckPlayerHealth();
                        }
                        break;
                

                    case "2":

                        // This allows the player to use an item from the inventory
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

                        // Bascially just lets the player to run away from the monster and go back to options so that you can heal
                        Console.WriteLine("You fled the battle!");
                        inCombat = false; 

                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        private void CheckPlayerHealth()

        // This method just makes sure the player is still alive
        {
            if (player.Health <= 0)
            {
                Console.WriteLine("\nGame Over! Good luck next time. ");
                Environment.Exit(0);
            }
        }        
        
    }
    
}
