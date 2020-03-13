using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Booking : IFillable
    {
        public int Booking_id { get; set; }
        public int Hotel_No { get; set; }
        public Guest Guest { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public int Room_No { get; set; }

        public Booking()
        {
            TableName = "Booking";
        }

        public Booking(int bookingId, Guest guest, int hotelNo, DateTime dateFrom, DateTime dateTo, int roomNo)
        {
            Booking_id = bookingId;
            Guest = guest;
            Hotel_No = hotelNo;
            Date_From = dateFrom;
            Date_To = dateTo;
            Room_No = roomNo;
        }

        public string TableName { get; set; }
        public void Fill(object[] dataObjects)
        {
            Booking_id = (int) dataObjects[0];
            Hotel_No = (int) dataObjects[1];
            //Do guset
            Date_From = (DateTime) dataObjects[3];
            Date_To = (DateTime) dataObjects[4];
            Room_No = (int) dataObjects[5];
        }
    }
}
