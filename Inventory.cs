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

        public void AddItem(Item item) 
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item) 
        
        { 
            Items.Remove(item);
        }

        public Item FindItem(string name) 
        
        {
            return Items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
            
        public IEnumerable<Weapon> GetWeapons() 
        
        { 
            return Items.OfType<Weapon>();
        } 
        
    }
}
