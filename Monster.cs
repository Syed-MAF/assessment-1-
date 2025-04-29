using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonExplorer
{
    public abstract class Monster : Creature
    {
        public int AttackPower { get; protected set; }

        protected Monster(string name, int health, int attackPower) : base(name, health)
        {
            AttackPower = attackPower;
        }   

        public override void Attack(Creature target)
        {
            Console.WriteLine($"Your attacks do {AttackPower} damage!");
            target.TakeDamage(AttackPower);
        }
    }

    public class Goblin : Monster
    {
        public Goblin() : base("Goblin", 50, 15) { }
    }

    public class Dragon : Monster
    {
        public Dragon() : base("Dragon", 200, 40) { }
    }
}