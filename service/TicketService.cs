using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using QRCoder;
namespace TicketPlace2._0.Service
{
    public class TicketService
    {
        
        public byte[] GeneratePdf(string content)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            using (XGraphics gfx = XGraphics.FromPdfPage(page))
            {
                gfx.DrawString(content, 
                               new XFont("Verdana", 20), 
                               XBrushes.Black, 
                               new XRect(20, 40, page.Width - 40, page.Height - 40), 
                               XStringFormats.TopLeft);
            }

            using (var ms = new MemoryStream())
            {
                document.Save(ms, false);
                return ms.ToArray();
            }
        }
        public byte[] GeneratePdfWithQrCode(string content)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            using (XGraphics gfx = XGraphics.FromPdfPage(page))
            {
                gfx.DrawString(content, 
                               new XFont("Verdana", 20), 
                               XBrushes.Black, 
                               new XRect(20, 40, page.Width - 40, page.Height - 40), 
                               XStringFormats.TopLeft);
                
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrImage = qrCode.GetGraphic(20);

                using (var qrStream = new MemoryStream())
                {
                    qrImage.Save(qrStream, System.Drawing.Imaging.ImageFormat.Png);
                    qrStream.Seek(0, SeekOrigin.Begin);
                    var qrBitmap = XImage.FromStream(qrStream);
                    gfx.DrawImage(qrBitmap, 20, 120, 100, 100); 
                }
            }
            using (var ms = new MemoryStream())
            {
                document.Save(ms, false);
                return ms.ToArray();
            }
        }
    }
}