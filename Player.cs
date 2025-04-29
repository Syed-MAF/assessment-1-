using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        private Inventory inventory = new Inventory();

        public Player(string name, int health): base(name, health)
        {
            // This gives the player a name and health
        }

        public void PickUpItem(Item item)
        {
            inventory.AddItem(item);
            Console.WriteLine($"You have picked up {item.Name}.");
        }

        public bool HasItem(string itemName)
        {
            return inventory.Items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }


        public void UseItem(string itemName)
        {
            Item item = inventory.FindItem(itemName);
            if (item != null )
            {
                item.Use(this);
                inventory.RemoveItem(item);
            }
            else 
            {
                Console.WriteLine("This iteam has not been found in you inventory.");
            }
            
        }

        public void ViewInventory()
        {
            Console.WriteLine("Inventory:");

            if (inventory.Items.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.Go pick up an item.");
                return;
            }
            foreach (var item in inventory.Items)

                Console.WriteLine($"- {item.Name}");
        }

        public override void Attack(Creature target)
        {
            Weapon weapon = inventory.GetWeapons().FirstOrDefault();
            if (weapon != null)
            {
                Console.WriteLine($"{Name} attacked  with {weapon.Name}!" );

                target.TakeDamage(weapon.AttackPower);
            }
            else Console.WriteLine("You have no weapon!");
        }

        public void Heal(int amount)
        {
            Health += amount;
            Console.WriteLine($"You healed {amount} HP!");
        }
    }
}
