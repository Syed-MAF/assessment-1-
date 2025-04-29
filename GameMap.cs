using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GameMap
    {
        public Room CurrentRoom { get; set; }

        public List<Room> Rooms { get; } = new List<Room>();

        public GameMap(Room startRoom)
        {
            CurrentRoom = startRoom;
            Rooms.Add(startRoom);
        }
    }
}
