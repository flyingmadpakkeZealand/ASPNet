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

        public static void Setup()
        {
            DataBases.RegisterDataBase("BaseHotel", 
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); 

            DataBases.RegisterDataBase(BaseNames.BaseHotel, 
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}