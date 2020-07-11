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
                    temp = Int32.Parse(User[i]) / 1024;
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
            try
            {
                userName = UnEncript(userName);
                password = UnEncript(password);
                //var list = from p in db.Access where p.UserName == userName && p.Password == password select p;
                var list = db.Access.First(c => c.UserName == userName && c.Password == password);
                return Ok(list);
            }
            catch (Exception)
            {
                return NotFound();
                throw;
            }
        }

        [Route("api/ProgramList/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_ProgramList(string userName, string password)
        {
            try
            {
                userName = UnEncript(userName);
                password = UnEncript(password);
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
        [Route("api/Account/InformationSql/{userName}/{password}")]
        public async Task<IHttpActionResult> GetInformationSql(string userName, string password)
        {
            try
            {
                userName = UnEncript(userName);
                password = UnEncript(password);
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
