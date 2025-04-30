using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Inventory
    {
        public List<Item> Items { get; } = new List<Item>();

        // Lets the player add an item to their inventory
        public void AddItem(Item item) 
        {
            Items.Add(item);
        }

        // Lets the player remove an item from their inventory
        public void RemoveItem(Item item) 
        
        { 
            Items.Remove(item);
        }

        // Lets the player find an item in their inventory
        public Item FindItem(string name) 
        
        {
            return Items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Lets the player get a list of all items in their inventory
        public IEnumerable<Weapon> GetWeapons() 
        
        { 
            return Items.OfType<Weapon>();
        } 
        
    }
}
