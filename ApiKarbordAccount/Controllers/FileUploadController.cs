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

namespace ApiKarbordAccount.Controllers
{
    public class FileUploadController : ApiController
    {

        [Route("api/FileUpload/UploadFile/{LockNumber}")]
        public async Task<IHttpActionResult> UploadFile(string LockNumber)
        {
            var folder = "C://App//Upload//" + LockNumber + "//";
            //var folder = "C://Test//App//Upload//" + LockNumber + "//";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            //var Atch = System.Web.HttpContext.Current.Request.Files["Atch"];
            //var req = HttpContext.Current.Request;
            //var file = req.Files[req.Files.Keys.Get(0)];

            var httpRequest = HttpContext.Current.Request.Files[0];
            var name = httpRequest.FileName.Split('.');
            string tempName = name[0] + "-" + DateTime.Now.ToString("yyMMddHHmmss") + "." + name[1];
            var filePath = folder + tempName;
            httpRequest.SaveAs(filePath);
            tempName = name[0] + "-" + DateTime.Now.ToString("yyMMddHHmmss") + "--" + name[1];
            return Ok(tempName);
        }



/*        public class FileDownload
        {
            public string LockNumber { get; set; }

            public string FileName { get; set; }

        }
        */


        [HttpGet]
        [Route("api/FileUpload/DownloadFile/{LockNumber}/{FileName}")]
        public HttpResponseMessage Download(string LockNumber, string FileName)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            FileName = FileName.Replace("--", ".");
            string filePath = "C://App//Upload//" + LockNumber + "//" + FileName;

            if (!File.Exists(filePath))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", FileName);
                throw new HttpResponseException(response);
            }


            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = FileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(FileName));
            return response;
        }


        [HttpGet]
        [Route("api/FileUpload/DeleteFile/{LockNumber}/{FileName}")]
        public async Task<IHttpActionResult> DeleteFile(string LockNumber, string FileName)
        {
            FileName = FileName.Replace("--", ".");

            string fullPath = "C://App//Upload//" + LockNumber + "//" + FileName;

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            return Ok("Ok");
        }


    }
}

