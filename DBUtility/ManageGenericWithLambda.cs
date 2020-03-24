using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public class ManageGenericWithLambda<T> where T : new()
    {
        private string _connectionString = null;
        private string _tableName = null;

        public ManageGenericWithLambda(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        public IEnumerable<T> Get(Action<T, SqlDataReader> fillMethod)
        {
            List<T> list = new List<T>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand($"Select * From {_tableName}", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    T item = ReadItem(fillMethod, reader);
                    list.Add(item);
                }
            }

            return list;
        }

        private T ReadItem(Action<T, SqlDataReader> fillMethod, SqlDataReader reader)
        {
            T item = new T();
            fillMethod(item, reader);
            return item;
        }

        public T GetOne(Action<T, SqlDataReader> fillMethod, Dictionary<string, object> lookupDictionary)
        {
            T item = default;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd =
                    new SqlCommand($"Select * From {_tableName} where {WhereClause(lookupDictionary.Keys)}", conn);
                cmd.Parameters.AddRange(ConstructParametersWhereClause(lookupDictionary));
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    item = ReadItem(fillMethod, reader);
                }
            }

            return item;
        }

        public bool Post(Dictionary<string, object> extractPairs)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Dictionary<string, object> extractPairs = extractFunc();

                SqlCommand cmd = new SqlCommand($"Insert Into {_tableName}({CommaSeparatedKeys(extractPairs.Keys)}) Values({CommaSeparatedKeys(extractPairs.Keys, "@")})", conn);
                cmd.Parameters.AddRange(ConstructParameters(extractPairs));

                int rowsAffected = 0;
                try
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    return false;
                }


                return rowsAffected == 1;
            }
        }

        public bool Put(Dictionary<string, object> extractPairs, Dictionary<string, object> lookupDictionary)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Dictionary<string, object> extractPairs = extractFunc();

                SqlCommand cmd = new SqlCommand($"Update {_tableName} set {Clause(extractPairs.Keys)} where {WhereClause(lookupDictionary.Keys)}", conn);
                cmd.Parameters.AddRange(ConstructParameters(extractPairs));
                cmd.Parameters.AddRange(ConstructParametersWhereClause(lookupDictionary));

                int rowsAffected = 0;
                try
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    return false;
                }


                return rowsAffected == 1;
            }
        }

        public bool Delete(Dictionary<string, object> lookupDictionary)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand($"Delete from {_tableName} where {WhereClause(lookupDictionary.Keys)}", conn);
                cmd.Parameters.AddRange(ConstructParametersWhereClause(lookupDictionary));

                int rowsAffected = cmd.ExecuteNonQuery();


                return rowsAffected == 1;
            }
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

        private string Clause(IEnumerable<string> keys)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in keys)
            {
                builder.Append($"{key}=@{key},");
            }

            string output = builder.ToString();
            return output.Remove(output.Length - 1);
        }

        private string WhereClause(IEnumerable<string> keys)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in keys)
            {
                builder.Append($"{key}=@W{key} and ");
            }

            string output = builder.ToString();
            return output.Remove(output.Length - 4);
        }

        private SqlParameter[] ConstructParametersWhereClause(Dictionary<string, object> lookupPairs)
        {
            SqlParameter[] parameters = new SqlParameter[lookupPairs.Count];
            int counter = 0;
            foreach (string key in lookupPairs.Keys)
            {
                parameters[counter++] = new SqlParameter("@W" + key, lookupPairs[key]);
            }

            return parameters;
        }
    }
}
