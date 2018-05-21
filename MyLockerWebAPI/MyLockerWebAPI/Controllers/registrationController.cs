using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyLockerWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Register")]
    public class registrationController : Controller
    {
        // GET: api/Register
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Register
        [HttpPost]
        public JsonResult Post([FromBody]UserToSave user)
        {
            string conn = "Server=tcp:mylockersqlserver.database.windows.net,1433;Initial Catalog=MyLocker;Persist Security Info=False;User ID=sqladmin;Password=Pass1984;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //string conn = "Data Source=DABEER-DEVELOPE; Initial Catalog=MyLocker; User Id=sa;password=admin;";

            SqlConnection sqlConnection = new SqlConnection(conn);
            Guid roleId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO user_role(id,name,description,created,modified) VALUES('" +roleId+
                user.roleName+ "','"+user.roleDescription+"','" + DateTime.Now + "','NULL')", sqlConnection);
            sqlCommand.ExecuteNonQuery();

            SqlCommand sqlCommand2 = new SqlCommand("INSERT INTO users(username,password_hash,dob,first_name,last_name," +
                "email,status,reset_token,reset_expiry,last_login,profile_pic_url,NIN,is_social_login,created,modified,id," +
                "role_id,guardian_id) VALUES('" + user.userName + "',sys.fn_varbintohexstr(HASHBYTES('MD5','" + user.password + "'))" +
                ",'" + user.dob + "','" + user.firstName + "','" + user.lastName + "','" + user.emailId + "','active','NULL','NULL'" +
                "'NULL','" + user.profilePicUrl + "','" + user.nationalInsNo + "','" + user.isSocialLogin + "','" + DateTime.Now + "','NULL','"
                + userId + "','" + roleId + "','NULL')", sqlConnection);
            sqlCommand2.ExecuteNonQuery();

            SqlCommand sqlCommand3 = new SqlCommand("INSERT INTO user_details(user_id,country,state,city,zip_code,address_line_1," +
                "address_line_2,employer_name,employer_status,education,marital_status,club_name,council_no,created,modified" +
                "VALUES('" +userId + "','"+user.country+"','"+user.state+"','"+user.town+"','"+user.zipCode+"','"+user.addressLine1+"','"+
                user.addressLine2 + "','" + user.employerName + "','" +user.employerStatus + "','" + user.education + "','"+user.MaritalStatus
                +"','"+user.clubName+"','"+user.countcilNo+"','"+DateTime.Now+"','NULL')", sqlConnection);
            sqlCommand3.ExecuteNonQuery();
            sqlConnection.Close();


            return new JsonResult(user);

        }
        public class UserToSave
        {
            public Guid id { get; set; }
            public string roleName { get; set; }
            public string MaritalStatus { get; set; }
            public string userName { get; set; }
            public string countcilNo { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailId { get; set; }
            public string password { get; set; }
            public string profilePicUrl { get; set; }
            public DateTime dob { get; set; }
            public string country { get; set; }
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string employerStatus { get; set; }
            public string state { get; set; }
            public string zipCode { get; set; }
           
            public string clubName { get; set; }
            public string employerName { get; set; }
            public string town { get; set; }
            public bool isSocialLogin { get; set; }
            public string nationalInsNo { get; set; }
            public string roleDescription { get; set; }
           
            public string education { get; set; }
            
            public bool isUnder13 { get; set; }
        }
        // PUT: api/Register/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
