using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary;
//using ModelLibraryFramework;

namespace ASPNet.DbUtil
{
    public static class Extractables
    {
        public static Dictionary<string, object> ExtractBooking(Booking booking)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Hotel_No", booking.HotelNo);
            lookupDictionary.Add("Guest_No", booking.Guest.GuestNo);
            lookupDictionary.Add("Date_From", booking.DateFrom);
            lookupDictionary.Add("Date_To", booking.DateTo);
            lookupDictionary.Add("Room_No", booking.Room.RoomNo);
            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractGuest(Guest guest)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Guest_No", guest.GuestNo);
            lookupDictionary.Add("Name", guest.Name);
            lookupDictionary.Add("Address", guest.Address);
            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractRoom(Room room)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Room_No", room.RoomNo);
            lookupDictionary.Add("Hotel_No", room.HotelNo);
            lookupDictionary.Add("Types", room.Type);
            lookupDictionary.Add("Price", room.Price);
            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractHotel(Hotel hotel)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Hotel_No", hotel.HotelNo);
            lookupDictionary.Add("Name", hotel.Name);
            lookupDictionary.Add("Address", hotel.Address);
            return lookupDictionary;
        }
    }
}