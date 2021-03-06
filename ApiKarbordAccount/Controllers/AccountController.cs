﻿using System;
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
                sql = string.Format(@"SELECT Id,lockNumber,CompanyName,UserName,Password,AddressApi,fromDate,toDate,userCount,'*******' as SqlServerName , '*******' as SqlUserName , '*******' as SqlPassword,
                                             AFI1_Group, AFI1_Access, AFI8_Group, AFI8_Access, ERJ_Group, ERJ_Access, active, ProgName
                                      FROM   Access
                                      where  UserName = '{0}' and Password = '{1}' ",
                                             userName, password);
                var list = db.Database.SqlQuery<Access>(sql).Single(); // db.Access.First(c => c.UserName == userName && c.Password == password);
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
        public async Task<IHttpActionResult> GetInformationSql(string userName, string password, string userKarbord, string ace, string group, string sal, long serialnumber, string modecode, int act, int bandNo)
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
        public async Task<IHttpActionResult> GetLog(string userName, string password, string userKarbord, string ace, string group, string sal, long serialnumber, string modecode, int act, byte flag, int bandNo)
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


        public partial class Message
        {
            public long id { get; set; }

            public string lockNumber { get; set; }

            public string expireDate { get; set; }

            public string title { get; set; }

            public string body { get; set; }

            public bool? active { get; set; }
        }

        // GET: api/Account/Messages
        [Route("api/Account/Messages/{lockNumber}")]
        public async Task<IHttpActionResult> GetWeb_Messages(string lockNumber)
        {
            string sql = string.Format("SELECT * FROM [dbo].[Message] where active = 1 and (lockNumber is null  or lockNumber = '{0}' or lockNumber = '')", lockNumber);
            var list = db.Database.SqlQuery<Message>(sql).ToList(); // db.Access.First(c => c.UserName == userName && c.Password == password);
            return Ok(list);
        }








        public class Box
        {
            public long id { get; set; }

            public byte mode { get; set; }

            public string lockNumber { get; set; }

            public string date { get; set; }

            public string title { get; set; }

            public string body { get; set; }

            public string namefile { get; set; }

        }

        public class BoxObject
        {
            public string LockNumber { get; set; }

            public byte Mode { get; set; }

            public string UserCode { get; set; }

        }


        // GET: api/Account/Box
        [Route("api/Account/Box/")]
        public async Task<IHttpActionResult> PostWeb_Box(BoxObject BoxObject)
        {
            string sql = string.Format(@"declare @mode tinyint = {0} 
                                         select* from Box where lockNumber = '{1}' and 
                                                                ((@mode = 0 and (mode = 1 or mode = 2)) or mode = @mode)
                                         order by date , id", BoxObject.Mode, BoxObject.LockNumber);

            var list = db.Database.SqlQuery<Box>(sql).ToList();
            return Ok(list);
        }



        public class InsertBoxObject
        {

            public byte Mode { get; set; }

            public string LockNumber { get; set; }

            public string Date { get; set; }

            public string Title { get; set; }

            public string Body { get; set; }

            public string NameFile { get; set; }

            public string UserCode { get; set; }

        }


        // Post: api/Account/Box
        [Route("api/Account/InsertBox/")]
        public async Task<IHttpActionResult> PostWeb_InsertBox(InsertBoxObject InsertBoxObject)
        {
            string sql = string.Format("INSERT INTO Box (mode,locknumber,date,title,body,namefile)VALUES({0},'{1}','{2}','{3}','{4}','{5}')",
                                      InsertBoxObject.Mode,
                                      InsertBoxObject.LockNumber,
                                      InsertBoxObject.Date,
                                      InsertBoxObject.Title,
                                      InsertBoxObject.Body,
                                      InsertBoxObject.NameFile);
            var list = db.Database.SqlQuery<Box>(sql).ToList();
            await db.SaveChangesAsync();
            return Ok(list);
        }


        // get: api/Account/DeleteBox
        [Route("api/Account/DeleteBox/{lockNumber}/{id}")]
        public async Task<IHttpActionResult> GetWeb_DeleteBox(string lockNumber, long id)
        {
            string sql = string.Format("update box set mode = 3 WHERE id = {0} and lockNumber = '{1}' and  mode = 1 select 0", id, lockNumber);
            var list = db.Database.SqlQuery<int>(sql).ToList();
            await db.SaveChangesAsync();
            return Ok(list);
        }
    }
}
