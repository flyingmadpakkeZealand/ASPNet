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
    public class HotelsController : ApiController
    {
        private static ManageGenericWithLambda<Hotel> manager = DataBases.Access<Hotel>(BaseNames.BaseHotel, "Hotel");

        public static Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Hotel_No", id);
            return lookupDictionary;
        }
        // GET: api/Hotels
        public IEnumerable<Hotel> Get()
        {
            return manager.Get(Fillables.FillHotel);
        }

        // GET: api/Hotels/5
        public Hotel Get(int id)
        {
            return manager.GetOne(Fillables.FillHotel, PrimaryKeys(id));
        }

        // POST: api/Hotels
        public bool Post([FromBody]Hotel hotel)
        {
            return manager.Post(() => Extractables.ExtractHotel(hotel));
        }

        // PUT: api/Hotels/5
        public bool Put(int id, [FromBody]Hotel hotel)
        {
            return manager.Put(() => Extractables.ExtractHotel(hotel), PrimaryKeys(id));
        }

        // DELETE: api/Hotels/5
        public bool Delete(int id)
        {
            return manager.Delete(PrimaryKeys(id));
        }
    }
}
