using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiKarbordAccount.Models;

namespace ApiKarbordAccount.Controllers
{
    public class AccountController : ApiController
    {
        Models.ModelAccount db = new ModelAccount();




        public static string UnEncript(string value)
        {
            int temp;
            if (value != "" && value != null)
            {
                string[] User = value.Split(',');
                int count = Int32.Parse(User[User.Length - 1]);
                char[] c = new char[count];
                for (int i = 0; i < User.Length - 1; i++)
                {
                    temp = Int32.Parse(User[i]) / 2;
                    c[i] = (Char)temp;
                }
                return new string(c);
            }
            else
                return null;
        }

        // GET: api/Account
        [Route("api/Account/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_Account(string userName, string password)
        {

            //userName = UnEncript(userName);
            // password = UnEncript(password);
            //var list = from p in db.Access where p.UserName == userName && p.Password == password select p;

            string sql = string.Format("select count(id) as count from Access where UserName = '{0}' and Password = '{1}' ",
                                        userName, password);

            int count = db.Database.SqlQuery<int>(sql).Single();

            if (count > 0)
            {
                var list = db.Access.First(c => c.UserName == userName && c.Password == password);
                return Ok(list);
            }
            else
                return Ok(0);

        }

        [Route("api/ProgramList/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_ProgramList(string userName, string password)
        {
            try
            {
                //userName = UnEncript(userName);
                // password = UnEncript(password);
                var list = from p in db.Access where p.UserName == userName && p.Password == password select p;
                //var list = db.Access.First(c => c.UserName == userName && c.Password == password);
                return Ok(list);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }


        // Get: api/Account/InformationSql دریافت اطلاعات اس کیو ال  
        [Route("api/Account/InformationSql/{userName}/{password}/{userKarbord}/{ace}/{group}/{sal}/{serialnumber}/{modecode}/{act}/{bandNo}")]
        public async Task<IHttpActionResult> GetInformationSql(string userName, string password, string userKarbord, string ace, string group, string sal, long serialnumber, string modecode, int act , int bandNo)
        {
            try
            {
                if (act > 0)
                {
                    string sql = String.Format(@"EXEC[dbo].[Web_InsertLog]
                                                              @userName = '{0}',
                                                              @password = '{1}',
		                                                      @ace = {2},
		                                                      @group = {3},
		                                                      @sal = {4},
		                                                      @userKarbord = {5},
		                                                      @serialNumber = {6},
		                                                      @modeCode = {7},
		                                                      @act = {8},
                                                              @flag = 0,
                                                              @bandno = {9}",
                                                              userName,
                                                              password,
                                                              ace,
                                                              group,
                                                              sal,
                                                              userKarbord,
                                                              serialnumber,
                                                              modecode,
                                                              act,
                                                              bandNo);
                    int value = db.Database.SqlQuery<int>(sql).Single();
                    if (value > 0)
                    {
                        await db.SaveChangesAsync();
                    }
                }
                var data = from p in db.Access where p.UserName == userName && p.Password == password select p;
                return Ok(data);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        // Get: api/Account/Log 
        [Route("api/Account/Log/{userName}/{password}/{userKarbord}/{ace}/{group}/{sal}/{serialnumber}/{modecode}/{act}/{flag}/{bandNo}")]
        public async Task<IHttpActionResult> GetLog(string userName, string password, string userKarbord, string ace, string group, string sal, long serialnumber, string modecode, int act, byte flag,int bandNo)
        {
            try
            {
                if (act > 0)
                {
                    string sql = String.Format(@"EXEC[dbo].[Web_InsertLog]
                                                              @userName = '{0}',
                                                              @password = '{1}',
		                                                      @ace = {2},
		                                                      @group = {3},
		                                                      @sal = {4},
		                                                      @userKarbord = {5},
		                                                      @serialNumber = {6},
		                                                      @modeCode = {7},
		                                                      @act = {8},
                                                              @flag = {9},
                                                              @bandno = {10} ",
                                                              userName,
                                                              password,
                                                              ace,
                                                              group,
                                                              sal,
                                                              userKarbord,
                                                              serialnumber,
                                                              modecode,
                                                              act,
                                                              flag,
                                                              bandNo);
                    int value = db.Database.SqlQuery<int>(sql).Single();
                    if (value > 0)
                    {
                        await db.SaveChangesAsync();
                    }
                }
                return Ok(1);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }




        public class Message
        {
            public long id { get; set; }

            public string lockNumber { get; set; }

            public string expireDate { get; set; }

            public string message { get; set; }

            public bool? active { get; set; }

        }
        // GET: api/Account/Messages
        [Route("api/Account/Messages/{lockNumber}")]
        public async Task<IHttpActionResult> GetWeb_Messages(string lockNumber)
        {
            string sql = string.Format("select * from Message where active = 1 and lockNumber = '{0}' or lockNumber is null ", lockNumber);
            var list  = db.Database.SqlQuery<Message>(sql).ToList();
            return Ok(list);
        }






        public class OutBox
        {
            public long id { get; set; }

            public string lockNumber { get; set; }

            public string date_send { get; set; }

            public string title { get; set; }

            public string body { get; set; }

            public string link { get; set; }

        }
        // GET: api/Account/OutBox
        [Route("api/Account/OutBox/{lockNumber}")]
        public async Task<IHttpActionResult> GetWeb_Outbox(string lockNumber)
        {
            string sql = string.Format("select * from Outbox where lockNumber = '{0}'", lockNumber);
            var list = db.Database.SqlQuery<OutBox>(sql).ToList();
            return Ok(list);
        }

    }
}
