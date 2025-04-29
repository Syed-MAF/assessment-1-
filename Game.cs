using System;
using System.Diagnostics.Eventing.Reader;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
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
                Console.WriteLine("Enter a valid name: ");
                name = Console.ReadLine();
                
            }

            Console.WriteLine("\nYou are in the dungeon now. " +
                "\nThere are items scattered around that will help you win the game by defeating the monsters." +
                "\nGood luck!!!");
            Console.WriteLine("\nPress any key to start the game...");


            // creates a new player object with the name the user has entered
            player = new Player(name, 100);

            // creates a new room object with the description of the room
            Room startRoom1 = new Room("\nStarting room: You can see a wooden sword and a health potion.\n");

            startRoom1.AddItem(new Weapon("Wooden Sword", 10));
            startRoom1.AddItem(new Potion("Health Potion", 30));


            Room goblinRoom = new Room("\nGoblins lair: You can see a goblin blocking your path!.\n");

            goblinRoom.AddMonster(new Goblin());





        }

        public void Start()
        {
            // Change the playing logic into true and populate the while loop

            bool playing = true;

            // The game loop
            while (playing == true)
            {

                // Code your playing logic here
                Console.WriteLine("\nPlease select one of the following numbers to select that option: " +
                    "\n1. Open treasure chest" +
                    "\n2. Open a mysterious box" +
                    "\n3. Go to the second room" +
                    "\n4. View inventory" +
                    "\n5. View room description" +
                    "\n6. Quit game\n");

                string answer = Console.ReadLine();

                // Switch statement to handle the user input
                switch (answer)
                {
                    // Case 1: Open the treasure chest
                    case "1":
                        Console.WriteLine("This chest contains a sword");
                        Console.WriteLine("You have now equiped a sword");
                        player.PickUpItem("sword");
                        break;

                    // Case 2: Open the mysterious box
                    case "2":
                        Console.WriteLine("There is nothing in this mysterious box");
                        break;

                    // Case 3: Go to the second room
                    case "3":
                        Console.WriteLine("You have entered a large room where a monster has woken up" +
                            "\nYou have no choice but to fight the monster");

                        if (player.HasItem("sword"))
                        {
                            Console.WriteLine("\nYou have begun the battle with the monster...");
                            Console.WriteLine("\nCongratulations!!! You have beaten the monster and won the game.\n");
                            playing = false;

                        }
                        else
                        {
                            Console.WriteLine("\nYou have begun the battle with the monster...");
                            Console.WriteLine("\nYou were defenceless and were killed in battle.\n");
                            playing = false;

                        }
                        break;

                    // Case 4: View inventory

                    case "4":
                        if (player.InventoryContents() == "")
                        {
                            Console.WriteLine("\nYour inventory is empty." +
                                "\nOpen the mysterious box or the chest to collect an item.");
                        }
                        else
                            Console.WriteLine("\nYour inventory contains: " + player.InventoryContents());

                        break;

                    // Case 5: View room description

                    case "5":
                        Console.WriteLine("\nHere is the room description: ");
                        Console.WriteLine(currentRoom.GetDescription());
                        break;

                    // Case 6: Quit game

                    case "6":
                        Console.WriteLine("\nSee you next time - Goodbye");
                        playing = false;
                        break;

                    // Default case: If the user enters a number that is not one of the options or a random string
                    default:
                        Console.WriteLine("\nPlease enter a number for one of the options");
                        break;

                }
            }
        }
    }
}