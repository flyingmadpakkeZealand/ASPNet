using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public static class DataBases
    {
        private static Dictionary<string, string> _dataBaseDictionary = new Dictionary<string, string>();
        private static Dictionary<Enum, string> _enumDataBaseDictionary = new Dictionary<Enum, string>();

        public static void RegisterDataBase(string dataBaseName, string connectionString)
        {
            _dataBaseDictionary.Add(dataBaseName, connectionString);
        }

        public static void RegisterDataBase(Enum dataBaseName, string connectionString)
        {
            _enumDataBaseDictionary.Add(dataBaseName, connectionString);
        }

        //Support for IFillable is not finished, therefore access through it has been commented out.

        //public static ManageGeneric<T> Access<T>(string dataBase) where T : IFillable, new()
        //{
        //    if (_dataBaseDictionary.ContainsKey(dataBase))
        //    {
        //        string connectionString = _dataBaseDictionary[dataBase];
        //        ManageGeneric<T> dataAccess = new ManageGeneric<T>(connectionString);

        //        return dataAccess;
        //    }

        //    throw new ArgumentException(dataBase + " is not a registered Data Base");
        //}

        //public static ManageGeneric<T> Access<T>(Enum dataBase) where T : IFillable, new()
        //{
        //    if (_enumDataBaseDictionary.ContainsKey(dataBase))
        //    {
        //        string connectionString = _enumDataBaseDictionary[dataBase];
        //        ManageGeneric<T> dataAccess = new ManageGeneric<T>(connectionString);

        //        return dataAccess;
        //    }

        //    throw new ArgumentException(dataBase + " is not a registered Data Base");
        //}

        //Lambda support is finished - though not 100% tested.

        public static ManageGenericWithLambda<T> Access<T>(string dataBase, string table) where T : new()
        {
            if (_dataBaseDictionary.ContainsKey(dataBase))
            {
                string connectionString = _dataBaseDictionary[dataBase];
                ManageGenericWithLambda<T> dataAccess = new ManageGenericWithLambda<T>(connectionString, table);

                return dataAccess;
            }

            throw new ArgumentException(dataBase + " is not a registered Data Base");
        }

        public static ManageGenericWithLambda<T> Access<T>(Enum dataBase, string table) where T : new()
        {
            if (_enumDataBaseDictionary.ContainsKey(dataBase))
            {
                string connectionString = _enumDataBaseDictionary[dataBase];
                ManageGenericWithLambda<T> dataAccess = new ManageGenericWithLambda<T>(connectionString, table);

                return dataAccess;
            }

            throw new ArgumentException(dataBase + " is not a registered Data Base");
        }
    }
}
