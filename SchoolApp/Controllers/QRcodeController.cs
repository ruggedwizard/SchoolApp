using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using SchoolApp.Models;
namespace SchoolApp.Controllers
{
    public class QRcodeController : ApiController
    {
        private BFSDBEntities db = new BFSDBEntities();
       
        //Convert Image To Byte Function
        public byte[] ImageToByteArray(Image imageIn)
        {
           MemoryStream ms = new MemoryStream();
           imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           return ms.ToArray();
        }


        //Generating QRCodes With Any Given String 
        public HttpResponseMessage Get(string QRcodeText)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            //Check The Database for StudentRecord 
            Student studentRecord = db.Students.FirstOrDefault(p => p.StudentNumber == QRcodeText);
            if (studentRecord == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;

            }

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(QRcodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Image qrCodeImage = qrCode.GetGraphic(20);

            //Generating Image to send and Response for the User
            MemoryStream ms =   new MemoryStream(ImageToByteArray(qrCodeImage));
            var bytes = ImageToByteArray(qrCodeImage);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return response;
     
        }
        
       
    }
}
