using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBUtility;
using ModelLibraryFramework;

namespace ASPNet.DbUtil
{
    public class BookingExtended : IFillable
    {
        public Booking Booking { get; set; }

        public BookingExtended()
        {
            Booking = new Booking();
            TableName = "Booking";
        }

        public BookingExtended(Booking booking, string tableName)
        {
            Booking = booking;
            TableName = tableName;
        }

        public string TableName { get; set; }
        public Dictionary<string, object> Extract()
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Hotel_No", Booking.HotelNo);
            lookupDictionary.Add("Guest_No", Booking.Guest.GuestNo);
            lookupDictionary.Add("Date_From", Booking.DateFrom);
            lookupDictionary.Add("Date_To", Booking.DateTo);
            lookupDictionary.Add("Room_No", Booking.RoomNo);
            return lookupDictionary;
        }

        public void Fill(SqlDataReader reader)
        {
            Booking.BookingId = reader.GetInt32(reader.GetOrdinal("Booking_id"));
            Booking.HotelNo = reader.GetInt32(reader.GetOrdinal("Hotel_No"));

            Booking.Guest = DataBases.Access<GuestExtended>("BaseHotel").GetOne(new KeyValuePair<string, int>("Guest_No", reader.GetInt32(reader.GetOrdinal("Guest_No")))).Guest;

            Booking.DateFrom = reader.GetDateTime(reader.GetOrdinal("Date_From"));
            Booking.DateTo = reader.GetDateTime(reader.GetOrdinal("Date_To"));
            Booking.RoomNo = reader.GetInt32(reader.GetOrdinal("Room_No"));
        }
    }
}