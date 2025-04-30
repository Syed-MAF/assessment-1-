using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Creature : IDamageable
    {
        public string Name { get; protected set; }

        public int Health { get; protected set; }

        /// This is the base class for all creatures in the game
        protected Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public abstract void Attack(Creature target);

        // / This method allows the creature to take damage
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} took {damage} damage. Health: {Health} hp");
        }
    }

    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}