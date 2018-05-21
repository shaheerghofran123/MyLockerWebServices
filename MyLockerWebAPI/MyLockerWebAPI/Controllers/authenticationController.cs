using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyLockerWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class authenticationController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Shaheer";
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]UserCreds userCreds)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DABEER-DEVELOPE; Initial Catalog=MyLocker; User Id=sa;password=admin;");
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM users WHERE password_hash=sys.fn_varbintohexstr(HASHBYTES('MD5','" + userCreds.password + "')) AND username='" + userCreds.username + "'", sqlConnection);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand.CommandText, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            sqlAdapter.Fill(dataTable);
            UserResponseBody user = new UserResponseBody();
            user.id = (Guid)dataTable.Rows[0]["id"];
            var token = Guid.NewGuid();
            user.token = token;
            user.expiry = DateTime.Now.AddDays(7);
            SqlCommand sqlCommand2 = new SqlCommand("INSERT INTO user_tokens(id,user_id,token,expiry,created,modified) VALUES('" +
                Guid.NewGuid()+"','"+user.id+"','"+token+"','"+DateTime.Now.AddDays(7)+"','"+DateTime.Now+"','"+DateTime.Now+"')", sqlConnection);
            sqlCommand2.ExecuteNonQuery();

            //SqlCommand sqlCommand3 = new SqlCommand("UPDATE user SET last_login='"+DateTime.Now+"'", sqlConnection);
            //sqlCommand3.ExecuteNonQuery();
            sqlConnection.Close();
            return new JsonResult(user);
        }
        public class UserCreds
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class UserResponseBody
        {
            public Guid id { get; set; }
            public Guid token { get; set; }
            public DateTime expiry { get; set; }
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
