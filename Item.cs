using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonExplorer
{
    /// This is the base class for all items in the game
    public interface ICollectable
    {
        void Collect(Player player);
    }

    public class Item : ICollectable
    {
        public string Name { get; protected set; }

        public abstract void Use(Player player);

        public void Collect(Player player)
        
        {
            player.PickUpItem(this);
        } 
    }

    /// This is the base class for all weapons in the game
    public class Weapon : Item
    {
        public int AttackPower { get; }
        public Weapon(string name, int attackPower)
        {
            Name = name;
            AttackPower = attackPower;
        }

        public override void Use(Player player)
        {
            Console.WriteLine($"Equipped {Name}");
            Console.WriteLine($"Attack: {AttackPower}");
        }
            
    }

    /// This is the base class for all potions in the game
    public class Potion : Item
    {
        public int HealAmount { get; }
        public Potion(string name, int healAmount)
        {
            Name = name;
            HealAmount = healAmount;
        }

        public override void Use(Player player) 
        { 
            player.Heal(HealAmount);
            Console.WriteLine($"{Name} used! Healed {HealAmount} HP.");
        } 
    }
}