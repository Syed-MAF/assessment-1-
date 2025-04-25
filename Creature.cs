using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Creature : IDamageable
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }

        protected Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public abstract void Attack(Creature target);

        public void TakeDamage(int damage)
        {
            ;
            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"{Name} took {damage} damage. Remaining health: {Health}");
        }
    }

    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}

