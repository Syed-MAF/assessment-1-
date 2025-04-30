using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // Needed for Debug.Assert

namespace DungeonExplorer
{
    public static class GameTests
    {
        public static void RunAllTests()
        {
            Console.WriteLine("Running game tests...");

            TestWeaponEquipping();
            TestCombatDamage();
            TestInventory();

            Console.WriteLine("All tests passed!");
        }

        static void TestWeaponEquipping()
        {
            // Create a test player
            Player player = new Player("Tester", 100);

            // Create test weapons
            Weapon weakSword = new Weapon("Wooden Sword", 20);
            Weapon strongSword = new Weapon("Iron Sword", 75);

            // Pick up weak sword first
            player.PickUpItem(weakSword);
            Debug.Assert(player.EquippedWeapon != null, "Player should have a weapon equipped");
            Debug.Assert(player.EquippedWeapon.Name == "Wooden Sword", "Should equip first weapon");

            // Pick up strong sword
            player.PickUpItem(strongSword);
            Debug.Assert(player.EquippedWeapon.Name == "Iron Sword", "Should equip stronger weapon automatically");

            Console.WriteLine("Weapon equipping test passed!");
        }

        static void TestCombatDamage()
        {
            Player player = new Player("Tester", 100);
            Monster goblin = new Goblin();
            Weapon sword = new Weapon("Test Sword", 25);

            player.PickUpItem(sword);
            int goblinStartHealth = goblin.Health;

            player.Attack(goblin);

            Debug.Assert(goblin.Health == goblinStartHealth - 25,
                "Goblin should take 25 damage from the sword");

            Console.WriteLine("Combat damage test passed!");
        }

        static void TestInventory()
        {
            Player player = new Player("Tester", 100);
            Potion potion = new Potion("Health Potion", 30);

            // Test adding item
            player.PickUpItem(potion);
            Debug.Assert(player.HasItem("Health Potion"), "Potion should be in inventory");

            // Test case-insensitive search
            Debug.Assert(player.HasItem("health potion"), "Should find item regardless of case");

            Console.WriteLine("Inventory test passed!");
        }
    }
}
