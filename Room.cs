namespace DungeonExplorer
{
    public class Room
    {
        private string description;

        public Room(string description)

        // Create a room class with the following properties:
        {
            this.description = description;
        }

        public string GetDescription()

        // Create a method that returns the description of the room
        {
            return description;
        }
    }
}