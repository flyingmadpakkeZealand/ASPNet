using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public class ManageGeneric<T> where T : IFillable, new()
    {
        private string _connectionString = null;

        public ManageGeneric(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> Get()
        {
            List<T> list = new List<T>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                T tableItem = new T(); ;

                SqlCommand cmd = new SqlCommand($"Select * From {tableItem.TableName}", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    T item = ReadItem(reader);
                    list.Add(item);
                }
            }
            return list;
        }

        private T ReadItem(SqlDataReader reader)
        {
            T item = new T();
            item.Fill(reader);
            return item;
        }

        public T GetOne(KeyValuePair<string, int> lookupPair)
        {
            T item = default;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                T tableItem = new T();

                SqlCommand cmd = new SqlCommand($"Select * From {tableItem.TableName} where {lookupPair.Key} = {lookupPair.Value}", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    item = ReadItem(reader);
                }
            }

            return item;
        }

        public bool Post(T item)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                Dictionary<string, object> lookupPairs = item.Extract();

                SqlCommand cmd = new SqlCommand($"Insert Into {item.TableName}({CommaSeparatedKeys(lookupPairs.Keys)}) Values({CommaSeparatedKeys(lookupPairs.Keys,"@")})", conn);
                cmd.Parameters.AddRange(ConstructParameters(lookupPairs));

                int rowsAffected = cmd.ExecuteNonQuery();

                
                return rowsAffected == 1;
            }
        }


        public bool Put(string sqlSentence)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected == 1;
        }


        public bool Delete(string sqlSentence)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlSentence, conn);
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected == 1;
        }

        private SqlParameter[] ConstructParameters(Dictionary<string, object> lookupPairs)
        {
            SqlParameter[] parameters = new SqlParameter[lookupPairs.Count];
            int counter = 0;
            foreach (string key in lookupPairs.Keys)
            {
                parameters[counter++] = new SqlParameter("@" + key, lookupPairs[key]);
            }

            return parameters;
        }

        private string CommaSeparatedKeys(IEnumerable<string> keys)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in keys)
            {
                builder.Append(key + ",");
            }

            string output = builder.ToString();
            return output.Remove(output.Length - 1);
        }

        private string CommaSeparatedKeys(IEnumerable<string> keys, string modifier)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in keys)
            {
                builder.Append(modifier + key + ",");
            }

            string output = builder.ToString();
            return output.Remove(output.Length - 1);
        }
    }
}
