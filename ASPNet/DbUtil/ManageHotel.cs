using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ModelLibraryFramework;


namespace ASPNet.DbUtil
{
    public static class ManageHotel
    {

        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * from Hotel";

        private const string GET_ONE = "Select * from Hotel where Hotel_No = @Id";

        private const string INSERT = "Insert into Hotel Values(@Id, @Name, @Address)";

        private const string UPDATE = "Update Hotel set Hotel_No = @HotelId, Name = @Name, Address = @Address where Hotel_No = @Id";

        private const string DELETE = "Delete from Hotel where Hotel_No = @Id";

        public static IEnumerable<Hotel> Get()
        {
            List<Hotel> list = new List<Hotel>();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ALL, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Hotel hotel = ReadHotel(reader);
                list.Add(hotel);
            }

            conn.Close();
            return list;
        }

        private static Hotel ReadHotel(SqlDataReader reader)
        {
            Hotel hotel = new Hotel();

            hotel.Id = reader.GetInt32(0);
            hotel.Name = reader.GetString(1);
            hotel.Address = reader.GetString(2);

            return hotel;
        }

        public static Hotel Get(int id)
        {
            Hotel hotel = null;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                hotel = ReadHotel(reader);
            }

            conn.Close();
            return hotel;
        }

        
        public static bool Post(Hotel hotel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(INSERT, conn);
            cmd.Parameters.AddRange(new SqlParameter[]{ new SqlParameter("@Id", hotel.Id), new SqlParameter("@Name", hotel.Name), new SqlParameter("@Address", hotel.Address)});
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected==1;
        }

        
        public static bool Put(int id, Hotel hotel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(UPDATE, conn);
            cmd.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@HotelId", hotel.Id), new SqlParameter("@Name", hotel.Name), new SqlParameter("@Address", hotel.Address), new SqlParameter("@Id", id) });
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected == 1;
        }

        
        public static bool Delete(int id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            return rowsAffected == 1;
        }
    }
}