using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Room
    {
        public int Room_No { get; set; }
        public int Hotel_No { get; set; }
        public string Types { get; set; }
        public double Price { get; set; }

        public Room()
        {
            
        }

        public Room(int roomNo, int hotelNo, string types, double price)
        {
            Room_No = roomNo;
            Hotel_No = hotelNo;
            Types = types;
            Price = price;
        }
    }
}
