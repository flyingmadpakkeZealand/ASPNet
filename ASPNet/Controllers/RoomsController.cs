using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASPNet.App_Start;
using ASPNet.DbUtil;
using DBUtility;
using ModelLibrary;
//using ModelLibraryFramework;

namespace ASPNet.Controllers
{
    public class RoomsController : ApiController
    {
        private static ManageGenericWithLambda<Room> manager = DataBases.Access<Room>(BaseNames.BaseHotel, "Room");

        public static Dictionary<string, object> PrimaryKeys(int roomNo, int hotelNo)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Room_No", roomNo);
            lookupDictionary.Add("Hotel_No", hotelNo);
            return lookupDictionary;
        }
        // GET: api/Rooms
        public IEnumerable<Room> Get()
        {
            return manager.Get(Fillables.FillRoom);
        }

        // GET: api/Rooms/5/3
        [Route("api/Rooms/{roomNo}/{hotelNo}")]
        public Room Get(int roomNo, int hotelNo)
        {
            return manager.GetOne(Fillables.FillRoom, PrimaryKeys(roomNo, hotelNo));
        }

        // POST: api/Rooms
        public bool Post([FromBody]Room room)
        {
            return manager.Post( Extractables.ExtractRoom(room));
        }

        // PUT: api/Rooms/5/3
        [Route("api/Rooms/{roomNo}/{hotelNo}")]
        public bool Put(int roomNo, int hotelNo, [FromBody]Room room)
        {
            return manager.Put( Extractables.ExtractRoom(room), PrimaryKeys(roomNo, hotelNo));
        }

        // DELETE: api/Rooms/5/3
        [Route("api/Rooms/{roomNo}/{hotelNo}")]
        public bool Delete(int roomNo, int hotelNo)
        {
            return manager.Delete(PrimaryKeys(roomNo, hotelNo));
        }
    }
}
