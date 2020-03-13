using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibraryFramework;

namespace ConsumeRest.Consumers
{
    public static class ConsumeHotel
    {
        private const string URI = "http://localhost:56897/api/Hotels";
        public static List<Hotel> GetAllHotels()
        {
            List<Hotel> hotels = new List<Hotel>();
            return hotels;
        }
    }
}
