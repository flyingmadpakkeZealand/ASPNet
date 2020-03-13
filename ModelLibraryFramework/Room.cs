using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibraryFramework
{
    public class Room
    {
        public int RoomNo { get; set; }
        public int HotelNo { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }

        public Room()
        {
            
        }

        public Room(int roomNo, int hotelNo, string type, double price)
        {
            RoomNo = roomNo;
            HotelNo = hotelNo;
            Type = type;
            Price = price;
        }

        public override string ToString()
        {
            return $"Room: {RoomNo} at Hotel: {HotelNo}, Type: {Type}, Cost: {Price}";
        }
    }
}
