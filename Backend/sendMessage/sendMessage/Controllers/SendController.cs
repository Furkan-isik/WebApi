using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using sendMessage.Models;

namespace sendMessage.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SendController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SendMessage(MessageModel message)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
                using (SqlConnection baglanti=new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    using (SqlCommand cmd= new SqlCommand("INSERT INTO ContactMessage(Ad, Soyad, Email, [Check], Message) VALUES (@Ad, @Soyad, @Email, @Check, @Message)", baglanti))
                    {
                        cmd.Parameters.Add("@Ad", SqlDbType.NVarChar).Value = message.Ad;
                        cmd.Parameters.Add("@Soyad", SqlDbType.NVarChar).Value = message.Soyad;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = message.Email;
                        cmd.Parameters.Add("@Check", SqlDbType.Int).Value = message.Check;
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message.Message;
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok("Message saved successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }
    }
}
