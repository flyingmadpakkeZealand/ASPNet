using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNet.App_Start;
using DBUtility;

[assembly: PreApplicationStartMethod(typeof(SetupDataBase), "Setup")]

namespace ASPNet.App_Start
{
    enum BaseNames
    {
        BaseHotel
    }
    public class SetupDataBase
    {

        //public static void Setup()
        //{
        //    DataBases.RegisterDataBase("BaseHotel", 
        //        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); 

        //    DataBases.RegisterDataBase(BaseNames.BaseHotel, 
        //        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}

        public static void Setup()
        {
            DataBases.RegisterDataBase("BaseHotel", 
                @"Data Source=magnusserverdb.database.windows.net;Initial Catalog=MagnusDataBase;User ID=MagnusAdmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            DataBases.RegisterDataBase(BaseNames.BaseHotel, 
                @"Data Source=magnusserverdb.database.windows.net;Initial Catalog=MagnusDataBase;User ID=MagnusAdmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}