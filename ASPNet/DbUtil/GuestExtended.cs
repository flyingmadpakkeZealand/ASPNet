using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBUtility;
using ModelLibraryFramework;

namespace ASPNet.DbUtil
{
    public class GuestExtended : IFillable
    {
        public Guest Guest { get; set; }

        public GuestExtended()
        {
            TableName = "Guest";
            Guest = new Guest();
        }

        public GuestExtended(Guest guest, string tableName)
        {
            Guest = guest;
            TableName = tableName;
        }

        public string TableName { get; set; }
        public Dictionary<string, object> Extract()
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Guest_No", Guest.GuestNo);
            lookupDictionary.Add("Name", Guest.Name);
            lookupDictionary.Add("Address", Guest.Address);
            return lookupDictionary;
        }

        public void Fill(SqlDataReader reader)
        {
            Guest.GuestNo = reader.GetInt32(reader.GetOrdinal("Guest_No"));
            Guest.Name = reader.GetString(reader.GetOrdinal("Name"));
            Guest.Address = reader.GetString(reader.GetOrdinal("Address"));
        }
    }
}