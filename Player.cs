using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }
        public void PickUpItem(string item)
        {
            inventory.Add(item);
            Console.WriteLine("You have added an iteam to you inventory\n");

        }
        public string InventoryContents()
        {
            return string.Join(", ", inventory);
        }

        public bool HasItem(string item)
        {
            return inventory.Contains(item);
            //hehehhehe
        }
    }
}