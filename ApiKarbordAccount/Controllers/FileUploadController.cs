using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;


namespace ApiKarbordAccount.Controllers
{
    public class FileUploadController : ApiController
    {
        public class DataFile
        {

            public string LockNumber { get; set; }

            public string Date_Send { get; set; }

        }

        [Route("api/FileUpload/UploadFile")]
        public async Task<IHttpActionResult> UploadFile()//(DataFile DataFile)
        {
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/Files/Send/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }
            return Ok("Ok");
        }

    }
}

