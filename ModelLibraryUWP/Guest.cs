using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibraryUWP
{
    public class Guest
    {
        public int GuestNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Guest()
        {

        }

        public Guest(int guestNo, string name, string address)
        {
            GuestNo = guestNo;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"Guest: {GuestNo}, {Name} at {Address}";
        }
    }
}
