using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNet.App_Start;
using ASPNet.DbUtil;
using DBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;


namespace ASPNetUnitTesting
{
    [TestClass]
    public class IOHotelManagerTest
    {
        #region Setup
        #region SetupDatabase
        enum BaseNames
        {
            BaseHotel
        }

        public static void SetupDatabase()
        {
            DataBases.RegisterDataBase("BaseHotel",
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            DataBases.RegisterDataBase(BaseNames.BaseHotel,
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            SetupDatabase();
        } 
        #endregion

        private ManageGenericWithLambda<Hotel> manager;
        private Hotel testHotel;

        private void SetupSimple()
        {
            manager = DataBases.Access<Hotel>(BaseNames.BaseHotel, "Hotel");
            testHotel = new Hotel(0, "Zero", "Street");
        }

        private void Setup()
        {
            SetupSimple();

            try
            {
                actualData = manager.GetOne(Fillables.FillHotel, PrimaryKeys(0));
                manager.Delete(PrimaryKeys(0));
            }
            catch (Exception e)
            {
            }
        }

        private Hotel actualData;
        private void Clean()
        {
            try
            {
                manager.Delete(PrimaryKeys(0));
                if (actualData.Name != string.Empty || actualData.Address != string.Empty) //Only put data back in if it is worthwhile data.
                {
                    manager.Post(Extractables.ExtractHotel(actualData));
                }
            }
            catch (Exception e)
            {
            }
        }

        public Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Hotel_No", id);
            return lookupDictionary;
        }
        #endregion

        //Note: setup and clean cannot fail.
        [TestMethod]
        public void ManagerGetAll_GetAllCompletes_ReturnsListHotel()
        {
            SetupSimple();
            List<Hotel> testHotelslList = null;

            try
            {
                testHotelslList = manager.Get(Fillables.FillHotel).ToList();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
            Assert.IsNotNull(testHotelslList);
            Assert.AreEqual(typeof(List<Hotel>), testHotelslList.GetType());
        }

        [TestMethod]
        public void ManagerGetOne_GetOneCompletes()
        {
            SetupSimple();
            testHotel = null;

            try
            {
                testHotel = manager.GetOne(Fillables.FillHotel, PrimaryKeys(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
        }

        [TestMethod]
        public void ManagerPost_PostCompletes()
        {
            Setup();

            try
            {
                manager.Post(Extractables.ExtractHotel(testHotel));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }


            Clean();
        }

        [TestMethod]
        public void ManagerPostAndGetOne_DataIsPostedAndDataIsRead()
        {
            Setup();

            Hotel actual = null;
            try
            {
                manager.Post(Extractables.ExtractHotel(testHotel));
                actual = manager.GetOne(Fillables.FillHotel, PrimaryKeys(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
            Assert.IsNotNull(actual);
            Assert.AreEqual(testHotel.HotelNo, actual.HotelNo);
            Assert.AreEqual(testHotel.Name, actual.Name);
            Assert.AreEqual(testHotel.Address, actual.Address);
        }

        [TestMethod]
        public void ManagerDelete_DeleteCompletes()
        {
            Setup();

            try
            {
                manager.Delete(PrimaryKeys(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
        }

        [TestMethod]
        public void ManagerDelete_WhenNoRecordIsFound_ReturnsFalse()
        {
            Setup();

            bool expected = false;
            bool actual = true;
            try
            {
                actual = manager.Delete(PrimaryKeys(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManagerDeleteAndPostAndGetOne_DataIsDeletedFromDatabase()
        {
            Setup();
            
            Hotel deletedHotel = new Hotel();
            try
            {
                manager.Post(Extractables.ExtractHotel(testHotel));
                manager.Delete(PrimaryKeys(0));
                deletedHotel = manager.GetOne(Fillables.FillHotel, PrimaryKeys(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
            Assert.IsNull(deletedHotel);
        }

        [TestMethod]
        public void ManagerPost_DuplicatePost()
        {
            Setup();

            bool expected = false;
            bool actual = true;

            try
            {
                manager.Post(Extractables.ExtractHotel(testHotel));
                actual = manager.Post(Extractables.ExtractHotel(testHotel));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManagerPut_PutCompletes()
        {
            Setup();

            Hotel editedHotel = new Hotel(0, "OtherHotel", "OtherStreet");
            try
            {
                manager.Put(Extractables.ExtractHotel(editedHotel), PrimaryKeys(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
        }

        [TestMethod]
        public void ManagerPut_WhenNoRecordIsFound_ReturnsFalse()
        {
            Setup();

            bool expected = false;
            bool actual = true;
            Hotel editedHotel = new Hotel(0, "OtherHotel", "OtherStreet");
            try
            {
                actual = manager.Put(Extractables.ExtractHotel(editedHotel), PrimaryKeys(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Clean();
            Assert.AreEqual(expected, actual);
        }
    }
}
