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
        [Route("api/Account/InformationSql/{userName}/{password}/{userKarbord}/{ace}/{group}/{sal}/{serialnumber}/{act}")]
        public async Task<IHttpActionResult> GetInformationSql(string userName, string password, string userKarbord, string ace, string group, string sal, long serialnumber, int act)
        {
            try
            {

                if (act > 0)
                {
                    string sql = String.Format(@"EXEC[dbo].[Web_InsertLog]
                                                              @userName = {0},
                                                              @password = {1},
		                                                      @ace = {2},
		                                                      @group = {3},
		                                                      @sal = {4},
		                                                      @userKarbord = {5},
		                                                      @act = {6} ",
                                                              userName,
                                                              password,
                                                              ace,
                                                              group,
                                                              sal,
                                                              userKarbord,
                                                              act);
                    int value = db.Database.SqlQuery<int>(sql).Single();
                    if (value > 0)
                    {
                        await db.SaveChangesAsync();
                    }
                }
                var list = from p in db.Access where p.UserName == userName && p.Password == password select p;
                return Ok(list);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

    }
}
