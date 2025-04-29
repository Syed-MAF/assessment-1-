using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {
        public string Description { get; }

        public List<Monster> Monsters { get; } = new List<Monster>();

        public List<Item> Items { get; } = new List<Item>();

        public Room(string description)
        
        { 
            Description = description;
        }

        public void AddMonster(Monster monster)
        
        {
            Monsters.Add(monster);
        }

        public void AddItem(Item item)

        {
            Items.Add(item);
        }
        
    }
}


















