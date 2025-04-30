using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        internal Inventory inventory = new Inventory();
        public Weapon EquippedWeapon { get; private set; }


        public Player(string name, int health): base(name, health)
        {
            // This gives the player a name and health
        }

        // / This method allows the player to pick up an item and add it to their inventory
        public void PickUpItem(Item item)
        {
            inventory.AddItem(item);
            Console.WriteLine($"You have picked up {item.Name}.");

            if (item is Weapon newWeapon)
            {
                if (EquippedWeapon == null)
                {
                    EquippedWeapon = newWeapon;
                    Console.WriteLine($"Attack: {newWeapon.AttackPower}");
                }
                else if (newWeapon.AttackPower > EquippedWeapon.AttackPower)
                {

                    EquippedWeapon = newWeapon;
                    Console.WriteLine($"Upgraded from {EquippedWeapon.Name} to {newWeapon.Name}!");
                }
            }

        }

        public bool HasItem(string itemName)
        {
            return inventory.Items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        // / This method allows the player to use an item from their inventory and removes it if you used the potion
        public void UseItem(string itemName)
        {
            Item item = inventory.FindItem(itemName);
            if (item != null )
            {
                item.Use(this);
                if (item is Potion)
                {
                    inventory.RemoveItem(item);
                }
            }
            else 
            {
                Console.WriteLine("This iteam has not been found in you inventory.");
            }
            
        }

        // / This method allows the player to view their inventory and see what items they have
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

        // / This method allows the player to attack the monster using their equipped weapon
        public override void Attack(Creature target)
        {
            Weapon weapon = inventory.GetWeapons().FirstOrDefault();

            if (weapon != null)
            {
                Console.WriteLine($"{Name} attacked  with {EquippedWeapon.Name}!" );

                target.TakeDamage(EquippedWeapon.AttackPower);
            }
            else Console.WriteLine("You have no weapon!");
        }

        //  This method allows the player to heal themselves using the potions
        public void Heal(int amount)
        {
            Health += amount;
            Console.WriteLine($"You healed {amount} HP!");
        }
    }
}
