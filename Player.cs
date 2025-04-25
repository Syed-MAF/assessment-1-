using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        // Create a player class with the following properties:
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();


        public Player(string name, int health) 
        {
            this.Name = name;
            Name = name;
            Health = health;
            // Initialize the player with a name and health
        }
        public void PickUpItem(string item)
        {
            // Add the item to the player's inventory
            inventory.Add(item);
            Console.WriteLine("You have added an iteam to you inventory\n");
            // Print a message to the console that the player has picked up the item

        }
        public string InventoryContents()
        {
            // Return a string with all the items in the player's inventory
            return string.Join(", ", inventory);
        }

        public bool HasItem(string item)
        {
            // Return true if the player has the item in their inventory
            return inventory.Contains(item);
            // Return false if the player does not have the item in their inventory
        }
    }
}