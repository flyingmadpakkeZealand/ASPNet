using System;
using System.Collections.Generic;
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
    public class GuestsController : ApiController
    {
        private static ManageGenericWithLambda<Guest> manager = DataBases.Access<Guest>(BaseNames.BaseHotel, "Guest");

        public static Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Guest_No", id);
            return lookupDictionary;
        }
        // GET: api/Guests
        public IEnumerable<Guest> Get()
        {
            return manager.Get(Fillables.FillGuest);
        }

        // GET: api/Guests/5
        public Guest Get(int id)
        {
            
            return manager.GetOne(Fillables.FillGuest, PrimaryKeys(id));
        }

        // POST: api/Guests
        public bool Post([FromBody]Guest guest)
        {
            return manager.Post(() => Extractables.ExtractGuest(guest));
        }

        // PUT: api/Guests/5
        public bool Put(int id, [FromBody]Guest guest)
        {
            return manager.Put(() => Extractables.ExtractGuest(guest), PrimaryKeys(id));
        }

        // DELETE: api/Guests/5
        public bool Delete(int id)
        {
            return manager.Delete(PrimaryKeys(id));
        }
    }
}
