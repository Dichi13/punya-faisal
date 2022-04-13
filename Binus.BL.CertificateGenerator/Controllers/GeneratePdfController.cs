using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Wkhtmltopdf.NetCore;

namespace Binus.BL.CertificateGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratePdfController : ControllerBase
    {
        private IGeneratePdf _genratePdf;

        public GeneratePdfController(IGeneratePdf generatePdf)
        {
            _genratePdf = generatePdf;
        }

        [HttpPost]
        public string GeneratePdf([FromBody] GeneratePdfDTO model)
        {
            var data = _genratePdf.GetPDF(model.HtmlContent);

            return Convert.ToBase64String(data);
        }

        public class GeneratePdfDTO
        {
            public string HtmlContent { get; set; }
        }
    }
}
