using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using ModelLibrary;

namespace ASPNet.DbUtil
{
    public static class ManageGeneric<T> where T : new()
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static IEnumerable<T> Get(string sqlSentence)
        {
            List<T> list = new List<T>();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                T item = ReadItem(reader);
                list.Add(item);
            }

            conn.Close();
            return list;
        }

        private static T ReadItem(SqlDataReader reader)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            

            T item = new T();
            
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Type propertyType = properties[i].PropertyType;
                if (!propertyType.Namespace.Contains("ModelLibrary"))
                {
                    properties[i].SetValue(item, reader.GetValue(i)); //This works under the assumption that the object T has its properties defined in the same order as in the database columns. 
                }
                else
                {
                    SqlConnection conn2 = new SqlConnection(ConnectionString);
                    conn2.Open();
                    SqlCommand objectCommand = new SqlCommand($"Select * from {propertyType.Name} where {reader.GetName(i)} = {reader.GetValue(i)}", conn2);
                    SqlDataReader subReader = objectCommand.ExecuteReader();
                    PropertyInfo[] propertiesOfSubObject = propertyType.GetProperties();
                    ConstructorInfo subObjectConstructor = propertyType.GetConstructor(Array.Empty<Type>());
                    object subObject = subObjectConstructor.Invoke(null);

                    while (subReader.Read())
                    {
                        for (int j = 0; j < propertiesOfSubObject.Length; j++)
                        {
                            propertiesOfSubObject[j].SetValue(subObject, subReader.GetValue(j));
                        }
                    }

                    properties[i].SetValue(item, subObject);
                }
            }

            return item;
        }

        public static IEnumerable<T> GetStrictNames(string sqlSentence)
        {
            List<T> list = new List<T>();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                T item = ReadItemStrictNames(reader);
                list.Add(item);
            }

            conn.Close();
            return list;
        }

        private static T ReadItemStrictNames(SqlDataReader reader)
        {
            Dictionary<string, PropertyInfo> properties = typeof(T).GetProperties().ToDictionary(info => info.Name);

            T item = new T();

            

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string fieldName = reader.GetName(i);
                if (properties.ContainsKey(fieldName))
                {
                    properties[fieldName].SetValue(item, reader.GetValue(i));
                }
            }
            
            return item;
        }

        public static T GetOne(string sqlSentence)
        {
            T item = default;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                item = ReadItem(reader);
            }

            conn.Close();
            return item;
        }

        public static T GetOneStrictNames(string sqlSentence)
        {
            T item = default;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                item = ReadItemStrictNames(reader);
            }

            conn.Close();
            return item;
        }

        public static bool Post(string sqlSentence)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected == 1;
        }


        public static bool Put(string sqlSentence)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected == 1;
        }


        public static bool Delete(string sqlSentence)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected == 1;
        }
    }
}