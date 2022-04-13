﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Binus.BL.CertificateGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneratePdfController : ControllerBase
    {
        private readonly ILogger<GeneratePdfController> _logger;

        public GeneratePdfController(ILogger<GeneratePdfController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GeneratePdfCertificate")]
        [Route("GeneratePdfCertificate")]
        public async Task<IActionResult> GeneratePdfCertificate()
        {
            try
            {
                byte[] res;
                using (MemoryStream ms = new MemoryStream())
                {
                    //var html = "<html><head> <link href='https://fonts.googleapis.com/css?family=Open Sans' rel='stylesheet'> <style type='text/css'> body, html{margin: 0; padding: 0;}body{color: black; display: table; font-family: 'Open Sans'; text-align: left;}.container{width: 1119px; height: 791px;}.text-container{position: absolute; left: 85px; top: 192px;}.text-head{font-family: 'Open Sans'; font-style: normal; font-weight: 700; font-size: 46.2907px; line-height: 63px; color: #1797B7;}.text-sub{font-family: 'Open Sans'; font-style: normal; font-weight: 400; font-size: 20.5736px; line-height: 28px; color: #2C2C47;}.text-name{font-family: 'Open Sans'; font-style: normal; max-width: 750px; min-height: 150px; /* border: 2px black solid; */ font-weight: 700; font-size: 61.7209px; line-height: 84px; color: #2C2C47; text-transform: uppercase;}.text-course{font-family: 'Open Sans'; font-style: normal; font-weight: 600; font-size: 30.8604px; line-height: 42px; color: #1797B7;}.text-small{font-family: 'Open Sans'; font-style: normal; font-weight: 400; font-size: 15.4302px; line-height: 21px; color: #2C2C47;}.text-small-bold{font-family: 'Open Sans'; font-style: normal; font-weight: 600; font-size: 15.4302px; line-height: 21px; color: #2C2C47;}.text-container-bottom{position: absolute; left: 481px; top: 677px; display: flex; flex-direction: row;}.text-container-bottom-right{position: absolute; left: 800px; top: 677px; display: flex; flex-direction: row;}.bg{width: 100%; height: 100%; object-fit: contain;}</style></head><body> <div class=\"container\"> <div class=\"text-container\"> <div class=\"text-head\"> VERIFIED CERTIFICATE </div><div class=\"text-sub\"> This is to certify that </div><div class=\"text-container-name\" style=\"margin-top: 10px;\"> <div class=\"text-name\">{{userName}}</div></div><div> <div class=\"text-sub\" style=\"margin: 10px 0px;\"> has successfully completed {{language}} {{courseLevel}}-Level courses and received passing grade for </div><div class=\"text-course\" style=\"margin: 10px 0px;\">{{courseTitle}}</div><div class=\"text-sub\" style=\"margin: 10px 0px;\"> A digital language learning system provided by Binus University </div></div></div><div class=\"text-container-bottom\"> <div> <div class=\"text-small\"> Verified Certificate </div><div class=\"text-small-bold\"> Issued {{issuedDate}}</div></div></div><div class=\"text-container-bottom-right\"> <div> <div class=\"text-small\"> Valid Certificate ID </div><div class=\"text-small-bold\">{{assertionNumber}}</div></div></div><img class=\"bg\" src=\"https://stblresourcesdev.blob.core.windows.net/certification-service/certificate-template-background.png\"/> </div></body></html>";
                    var path = "C:/Users/faisa/Downloads/haheho.html";
                    string html = "";
                    using (FileStream fs = System.IO.File.OpenRead(path))
                    {
                        byte[] b = new byte[1024];
                        UTF8Encoding temp = new UTF8Encoding(true);
                        while (fs.Read(b, 0, b.Length) > 0)
                        {
                            html = temp.GetString(b);
                        }
                    }
                    var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4, margin: 0);
                    pdf.Save(ms);
                    res = ms.ToArray();
                }
                string fileName = "Certificate " + Guid.NewGuid().ToString() + ".pdf";
                return File(res, "application/pdf", fileName);
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}