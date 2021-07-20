using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net.Http.Headers;

using System.Globalization;
using System.Text;
using System.Web.Hosting;
using System.Data.SqlClient;
using System.Data;
using ApiKarbordAccount.Models;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Configuration;

namespace ApiKarbordAccount.Controllers
{
    public class TicketPoshtibaniController : ApiController
    {


        /*  public partial class TicketPoshtibani
          {
              public int id { get; set; }

              public string SqlServerName { get; set; }

              public string SqlUserName { get; set; }

              public string SqlPassword { get; set; }

              public string group { get; set; }

          }


          public static string ConnectionDatabase()
          {
              Models.ModelAccount db = new ModelAccount();

              string sql = string.Format("select * from TicketPoshtibani");

              TicketPoshtibani list = db.Database.SqlQuery<TicketPoshtibani>(sql).First();

              string connectionString = String.Format(
                  @"data source = {0};initial catalog = {1};persist security info = True;user id = {2}; password = {3};  multipleactiveresultsets = True; application name = EntityFramework",
                  list.SqlServerName, "ACE_Web2" + list.group + "0000", list.SqlUserName, list.SqlPassword);
              return connectionString;
          }

      */

      /*  ModelTiket db_Tiket = new ModelTiket();

        public class Object_ErjDocXK
        {
            public int ModeCode { get; set; }

            public string LockNo { get; set; }
        }


        [Route("api/TicketPoshtibani/Web_ErjDocXK")]
        public async Task<IHttpActionResult> PostWeb_ErjDocXK(Object_ErjDocXK Object_ErjDocXK)
        {
            string sql = string.Format("select * from dbo.Web_ErjDocXK({0},'{1}')", Object_ErjDocXK.ModeCode, Object_ErjDocXK.LockNo);
            var list = db_Tiket.Database.SqlQuery<Web_ErjDocXK>(sql).ToList();
            return Ok(list);
        }


        public class Object_TicketStatus
        {
            public string SerialNumber { get; set; }
        }


        [Route("api/TicketPoshtibani/Web_TicketStatus")]
        public async Task<IHttpActionResult> PostWeb_TicketStatus(Object_TicketStatus Object_TicketStatus)
        {
            string sql = string.Format("select * from Web_TicketStatus where serialnumber in ({0})", Object_TicketStatus.SerialNumber);
            var list = db_Tiket.Database.SqlQuery<Web_TicketStatus>(sql).ToList();
            return Ok(list);
        }




        public class DocAttachObject
        {
            public int ModeCode { get; set; }
            public long SerialNumber { get; set; }
        }

        [Route("api/TicketPoshtibani/DocAttach")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_DocAttach(DocAttachObject DocAttachObject)
        {

            string sql = string.Format(CultureInfo.InvariantCulture,
                                       @"select  SerialNumber,Comm,FName,BandNo FROM Web_DocAttach
                                             where   ModeCode = {0} and ProgName='{1}' and SerialNumber = {2} order by BandNo desc",
                                         DocAttachObject.ModeCode,
                                         "ERJ1",
                                         DocAttachObject.SerialNumber);

            var list = db_Tiket.Database.SqlQuery<Web_DocAttach>(sql);
            return Ok(list);

        }




        public class DownloadAttachObject
        {
            public long SerialNumber { get; set; }
            public int ModeCode { get; set; }
        }

        public class DownloadAttach
        {
            public byte[] Atch { get; set; }
        }


        [Route("api/TicketPoshtibani/DownloadAttach")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostWeb_DownloadAttach(DownloadAttachObject DownloadAttachObject)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                      @"select Atch FROM Web_DocAttach where SerialNumber = {0} and ModeCode = {1}",
                      DownloadAttachObject.SerialNumber,
                      DownloadAttachObject.ModeCode);
            var list = db_Tiket.Database.SqlQuery<DownloadAttach>(sql).Single();
            return Ok(list.Atch);
        }



        [Route("api/TicketPoshtibani/UploadFile")]
        public async Task<IHttpActionResult> UploadFile()
        {

            var conString = ConfigurationManager.ConnectionStrings["TicketPoshtibani"].ConnectionString;

            string SerialNumber = HttpContext.Current.Request["SerialNumber"];
            string ProgName = HttpContext.Current.Request["ProgName"];
            string ModeCode = HttpContext.Current.Request["ModeCode"];
            string BandNo = HttpContext.Current.Request["BandNo"];
            string Code = HttpContext.Current.Request["Code"];
            string Comm = HttpContext.Current.Request["Comm"];
            string FName = HttpContext.Current.Request["FName"];
            var Atch = System.Web.HttpContext.Current.Request.Files["Atch"];

            //var req = HttpContext.Current.Request;
            //var file = req.Files[req.Files.Keys.Get(0)];


            int lenght = Atch.ContentLength;
            byte[] filebyte = new byte[lenght];
            Atch.InputStream.Read(filebyte, 0, lenght);

           
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();

            SqlCommand cmd = new SqlCommand("Web_DocAttach_Save", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgName", ProgName);
            cmd.Parameters.AddWithValue("@ModeCode", ModeCode);
            cmd.Parameters.AddWithValue("@SerialNumber", SerialNumber);
            cmd.Parameters.AddWithValue("@BandNo", BandNo);
            cmd.Parameters.AddWithValue("@Code", Code);
            cmd.Parameters.AddWithValue("@Comm", Comm);
            cmd.Parameters.AddWithValue("@FName", FName);
            cmd.Parameters.AddWithValue("@Atch", filebyte);

            cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(1);
        }



        public class ErjSaveTicket_HI
        {
            public long SerialNumber { get; set; }

            public string DocDate { get; set; }

            public string UserCode { get; set; }

            public string Status { get; set; }

            public string Spec { get; set; }

            public string LockNo { get; set; }

            public string Text { get; set; }

            public string F01 { get; set; }

            public string F02 { get; set; }

            public string F03 { get; set; }

            public string F04 { get; set; }

            public string F05 { get; set; }

            public string F06 { get; set; }

            public string F07 { get; set; }

            public string F08 { get; set; }

            public string F09 { get; set; }

            public string F10 { get; set; }

            public string F11 { get; set; }

            public string F12 { get; set; }

            public string F13 { get; set; }

            public string F14 { get; set; }

            public string F15 { get; set; }

            public string F16 { get; set; }

            public string F17 { get; set; }

            public string F18 { get; set; }

            public string F19 { get; set; }

            public string F20 { get; set; }

        }


        [Route("api/TicketPoshtibani/ErjSaveTicket_HI")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostErjSaveTicket_HI(ErjSaveTicket_HI ErjSaveTicket_HI)
        {
            string sql = string.Format(@"
                                    DECLARE	@DocNo_Out int
                                    EXEC	[dbo].[Web_ErjSaveTicket_HI]
		                                    @SerialNumber = {0},
		                                    @DocDate = '{1}',
		                                    @UserCode = '{2}',
		                                    @Status = '{3}',
		                                    @Spec = '{4}',
		                                    @LockNo = '{5}',
		                                    @Text = '{6}',
		                                    @F01 = '{7}',
		                                    @F02 = '{8}',
		                                    @F03 = '{9}',
		                                    @F04 = '{10}',
		                                    @F05 = '{11}',
		                                    @F06 = '{12}',
		                                    @F07 = '{13}',
		                                    @F08 = '{14}',
		                                    @F09 = '{15}',
		                                    @F10 = '{16}',
		                                    @F11 = '{17}',
		                                    @F12 = '{18}',
		                                    @F13 = '{19}',
		                                    @F14 = '{20}',
		                                    @F15 = '{21}',
		                                    @F16 = '{22}',
		                                    @F17 = '{23}',
		                                    @F18 = '{24}',
		                                    @F19 = '{25}',
		                                    @F20 = '{26}',
		                                    @DocNo_Out = @DocNo_Out OUTPUT
                                    SELECT	@DocNo_Out as N'DocNo_Out'",
                    ErjSaveTicket_HI.SerialNumber,
                    ErjSaveTicket_HI.DocDate ?? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(-1)),
                    ErjSaveTicket_HI.UserCode,
                    ErjSaveTicket_HI.Status,
                    ErjSaveTicket_HI.Spec,
                    ErjSaveTicket_HI.LockNo,
                    ErjSaveTicket_HI.Text,
                    ErjSaveTicket_HI.F01,
                    ErjSaveTicket_HI.F02,
                    ErjSaveTicket_HI.F03,
                    ErjSaveTicket_HI.F04,
                    ErjSaveTicket_HI.F05,
                    ErjSaveTicket_HI.F06,
                    ErjSaveTicket_HI.F07,
                    ErjSaveTicket_HI.F08,
                    ErjSaveTicket_HI.F09,
                    ErjSaveTicket_HI.F10,
                    ErjSaveTicket_HI.F11,
                    ErjSaveTicket_HI.F12,
                    ErjSaveTicket_HI.F13,
                    ErjSaveTicket_HI.F14,
                    ErjSaveTicket_HI.F15,
                    ErjSaveTicket_HI.F16,
                    ErjSaveTicket_HI.F17,
                    ErjSaveTicket_HI.F18,
                    ErjSaveTicket_HI.F19,
                    ErjSaveTicket_HI.F20
                    );
            int value = db_Tiket.Database.SqlQuery<int>(sql).Single();

            await db_Tiket.SaveChangesAsync();
            return Ok(value);
        }*/

    }
}

