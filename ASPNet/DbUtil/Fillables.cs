using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ASPNet.App_Start;
using ASPNet.Controllers;
using DBUtility;
using ModelLibraryFramework;

namespace ASPNet.DbUtil
{
    public static class Fillables
    {
        public static void FillBooking(Booking booking, SqlDataReader reader)
        {
            booking.BookingId = reader.GetInt32(reader.GetOrdinal("Booking_id"));
            booking.HotelNo = reader.GetInt32(reader.GetOrdinal("Hotel_No"));

            int id = reader.GetInt32(reader.GetOrdinal("Guest_No"));
            booking.Guest = DataBases.Access<Guest>(BaseNames.BaseHotel, "Guest").GetOne(FillGuest, GuestsController.PrimaryKeys(id));

            booking.DateFrom = reader.GetDateTime(reader.GetOrdinal("Date_From"));
            booking.DateTo = reader.GetDateTime(reader.GetOrdinal("Date_To"));
            int roomNo = reader.GetInt32(reader.GetOrdinal("Room_No"));
            booking.Room = DataBases.Access<Room>(BaseNames.BaseHotel, "Room")
                .GetOne(FillRoom, RoomsController.PrimaryKeys(roomNo, booking.HotelNo));
        }

        public static void FillGuest(Guest guest, SqlDataReader reader)
        {
            guest.GuestNo = reader.GetInt32(reader.GetOrdinal("Guest_No"));
            guest.Name = reader.GetString(reader.GetOrdinal("Name"));
            guest.Address = reader.GetString(reader.GetOrdinal("Address"));
        }

        public static void FillRoom(Room room, SqlDataReader reader)
        {
            room.RoomNo = reader.GetInt32(reader.GetOrdinal("Room_No"));
            room.HotelNo = reader.GetInt32(reader.GetOrdinal("Hotel_No"));
            room.Type = reader.GetString(reader.GetOrdinal("Types"));
            room.Price = reader.GetDouble(reader.GetOrdinal("Price"));
        }

        public static void FillHotel(Hotel hotel, SqlDataReader reader)
        {
            hotel.HotelNo = reader.GetInt32(reader.GetOrdinal("Hotel_No"));
            hotel.Name = reader.GetString(reader.GetOrdinal("Name"));
            hotel.Address = reader.GetString(reader.GetOrdinal("Address"));
        }
    }
}