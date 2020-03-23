using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNet.App_Start;
using DBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace ASPNetUnitTesting
{
    [TestClass]
    public class IOHotelManagerTest
    {
        private static ManageGenericWithLambda<Hotel> manager = DataBases.Access<Hotel>(BaseNames.BaseHotel, "Hotel");

        public static Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Hotel_No", id);
            return lookupDictionary;
        }
    }
}
