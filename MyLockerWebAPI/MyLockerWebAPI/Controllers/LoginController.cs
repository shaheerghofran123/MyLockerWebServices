using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyLockerWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get(string jsonString)
        {
            
            return new string[] { "as", "as" };
        }

        [HttpGet("{username}/{password}")]
        public JsonResult Get(string username, string password)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=tcp:mylockersqlserver.database.windows.net,1433;Initial Catalog=MyLocker;Persist Security Info=False;User ID=sqladmin;Password=Pass1984;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM users WHERE password_hash=sys.fn_varbintohexstr('MD5','"+password+"') && username='" + username+"'", sqlConnection);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand.CommandText,sqlConnection);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            sqlAdapter.Fill(dataTable);
            User user = new User();
            user.userName = (string)dataTable.Rows[0]["user_name"];
            user.firstName = (string)dataTable.Rows[0]["first_name"];
            user.lastName = (string)dataTable.Rows[0]["last_name"];
            user.emailId = (string)dataTable.Rows[0]["email_id"];
            user.subscription_Type_id = (string)dataTable.Rows[0]["subscription_type"];
            //user.addressLine1 = 
            return new JsonResult(user);
        }
        // POST api/values
        [HttpPost]
        public string Post(string username,string password, string last_name, string first_name,string email_id,string subscription_type,string account_level,string address_line_1,string address_line_2, string town,string province, string zip_code)
        {
            string conn = "Server=tcp:mylockersqlserver.database.windows.net,1433;Initial Catalog=MyLocker;Persist Security Info=False;User ID=sqladmin;Password=Pass1984;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            

            SqlConnection sqlConection = new SqlConnection(conn);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO users(username,last_name,first_name,email_id,subsription_type,password_hash,account_level,address_line_1,address_line_2,town,province,zip_code,created,modified) VALUES('" + username + "','" + last_name + "','" + first_name + "','" + email_id + "','" + subscription_type + "',sys.fn_varbintohexstr('MD5','"+password+"'),'" + account_level + "','" + address_line_1 + "','" + address_line_2 + "','" + town + "','" + province + "','" + zip_code + "','" + DateTime.Now + "','"+DateTime.Now+"')", sqlConection);
            sqlConection.Open();
            int res = sqlCommand.ExecuteNonQuery();
            if (res == 1)
            {
                sqlConection.Close();
                return "Custom 201";
            }
            else
            {
                sqlConection.Close();
                return "Error";
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}