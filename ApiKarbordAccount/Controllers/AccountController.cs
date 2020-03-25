using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApiKarbordAccount.Models;

namespace ApiKarbordAccount.Controllers
{
    public class AccountController : ApiController
    {
        Models.ModelAccount db = new ModelAccount();
        // GET: api/Account
        [Route("api/Account/{userName}/{password}")]
        public async Task<IHttpActionResult> GetWeb_Account(string userName, string password)
        {
            try
            {
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
    }
}
