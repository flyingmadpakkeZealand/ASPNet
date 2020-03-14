using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibraryUWP
{
    public class Hotel
    {
        public int HotelNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Hotel()
        {

        }

        public Hotel(int hotelNo, string name, string address)
        {
            HotelNo = hotelNo;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"Hotel: {HotelNo}, {Name}, {Address}";
        }
    }
}
