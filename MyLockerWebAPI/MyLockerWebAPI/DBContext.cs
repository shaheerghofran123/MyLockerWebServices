using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MyLockerWebAPI
{
    public class DBContext
    {
        string connectionString;
        public DBContext()
        {
            connectionString = "Data Source=DABEER-DEVELOPE;Initial Catalog=APIDB;Persist Security Info=False;User ID=sa;Password=admin;";
            
        }

        public DataTable FetchUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM users WHERE user_name = '" + username + "' && master.dbo.fn_varbintohexstring(HASHBYTES('MD5','" + password + "'))=password", connection))
                {
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
