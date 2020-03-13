using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASPNet.App_Start;
using ASPNet.DbUtil;
using DBUtility;
using ModelLibraryFramework;


namespace ASPNet.Controllers
{
    public class BookingsController : ApiController
    {
        private static ManageGenericWithLambda<Booking> manager =
            DataBases.Access<Booking>(BaseNames.BaseHotel, "Booking");

        public static Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Booking_id", id);
            return lookupDictionary;
        }
        // GET: api/Bookings
        public IEnumerable<Booking> Get()
        {
            return manager.Get(Fillables.FillBooking);
        }

        // GET: api/Bookings/5
        public Booking Get(int id)
        {
            return manager.GetOne(Fillables.FillBooking, PrimaryKeys(id));
        }

        // POST: api/Bookings
        public bool Post([FromBody]Booking booking) //Beware of cultural differences with dates
        {
            return manager.Post(() => Extractables.ExtractBooking(booking));
        }

        // PUT: api/Bookings/5
        public bool Put(int id, [FromBody]Booking booking)
        {
            return manager.Put(() => Extractables.ExtractBooking(booking), PrimaryKeys(id));
        }

        // DELETE: api/Bookings/5
        public bool Delete(int id)
        {
            return manager.Delete(PrimaryKeys(id));
        }
    }
}
