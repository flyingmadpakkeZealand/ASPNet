using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int HotelNo { get; set; }
        public Guest Guest { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Room Room { get; set; }

        public Booking()
        {

        }

        public Booking(int bookingId, Guest guest, int hotelNo, DateTime dateFrom, DateTime dateTo, Room room)
        {
            BookingId = bookingId;
            Guest = guest;
            HotelNo = hotelNo;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Room = room;
        }

        public override string ToString()
        {
            return $"Booking: {BookingId}, Hotel: {HotelNo}, Room: {Room.RoomNo}, booked by {Guest.GuestNo}:{Guest.Name} from {DateFrom} to {DateTo}";
        }
    }
}
