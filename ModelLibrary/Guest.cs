using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Guest : IFillable
    {
        public int Guest_No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Guest()
        {
            TableName = "Guest";
        }

        public Guest(int guestNo, string name, string address)
        {
            Guest_No = guestNo;
            Name = name;
            Address = address;
        }

        public string TableName { get; set; }
        public void Fill(object[] dataObjects)
        {
            Guest_No = (int) dataObjects[0];
            Name = dataObjects[1] as string;
            Address = dataObjects[2] as string;
        }
    }
}
