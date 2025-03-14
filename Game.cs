using System;
using System.Diagnostics.Eventing.Reader;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        // this is to test the git push
        //this is second test
        //making more changes


        public Game()
        {
            // Initialize the game with one room and one player
            Console.WriteLine("Enter your name to get started: ");
            player = new Player(Console.ReadLine(), 100);
            currentRoom = new Room("\nYou have entered the dungeon" +
                "\nIn the dungeon there is no light and all you have brought with you is a torch" +
                "\nYou must remain quite whilst in the first room or else the monster will wake up" +
                "\nIf you listen closly you can even hear the monsters sleeping but once you enter the second room the monster will wake up" +
                "\nThis dungeon is made up of two rooms, the first room contains two items" +
                "\nThe second room has a monster inside" +
                "\nTo win the game you must defeat the monster"+
                "\nThere are a few items scattered around this cold and dark room\n");
            Console.WriteLine(currentRoom.GetDescription());

        }

        public void Start()
        {
            // Change the playing logic into true and populate the while loop

            bool playing = true;

            while (playing == true)
            {

                // Code your playing logic here
                Console.WriteLine("\nPlease select one of the following numbers to select that option: " +
                    "\n1. open treasure chest" +
                    "\n2. open a mysterious box" +
                    "\n3. go to the second room" +
                    "\n4. quit game\n");

                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        Console.WriteLine("This chest contains a sword");
                        Console.WriteLine("You have now equiped a sword");
                        player.PickUpItem("sword");
                        break;

                    case "2":
                        Console.WriteLine("There is nothing in this mysterious box");
                        break;

                    case "3":
                        Console.WriteLine("You have entered a large room where a monster has woken up" +
                            "\nYou have no choice but to fight the monster");

                        if (player.HasIteam("sword"))
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

                    case "4":
                        Console.WriteLine("\nSee you next time - Goodbye");
                        playing = false;
                        break;

                    default:
                        Console.WriteLine("\nPlease enter a number for one of the options");
                        break;

                }
            }
        }
    }
}