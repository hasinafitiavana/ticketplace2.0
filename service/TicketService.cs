using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using QRCoder;
using TicketPlace2._0.Models;
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
        public string placeVenduToString(PlaceVendueModel placeVendueModel)
        {
            return "Nom: " + placeVendueModel.Utilisateur.Nom + "\n" +
                   "Prenom: " + placeVendueModel.Utilisateur.Prenom + "\n" +
                    "Evenement: " + placeVendueModel.Evenement.Nom + "\n" +
                   "Type de place: " + placeVendueModel.TypePlace.Type + "\n" +
                   "Numero de place: " + placeVendueModel.NumeroDePlace + "\n" +
                   "Prix: " + placeVendueModel.Prix + "\n" +
                   "Date de reservation: " + placeVendueModel.OnCreate;
        }
        public byte[] GeneratePdfWithQrCode(PlaceVendueModel placeVendueModel, string url)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            using (XGraphics gfx = XGraphics.FromPdfPage(page))
            {
                double x = 20;
                double y = 40;
                double lineHeight = 30;
                 gfx.DrawString("Nom: " + placeVendueModel.Utilisateur.Nom, new XFont("Verdana", 20), XBrushes.Black, new XPoint(x, y));
                y += lineHeight;
                gfx.DrawString("Prenom: " + placeVendueModel.Utilisateur.Prenom, new XFont("Verdana", 20), XBrushes.Black, new XPoint(x, y));
                y += lineHeight;
                gfx.DrawString("Evenement: " + placeVendueModel.Evenement.Nom, new XFont("Verdana", 20), XBrushes.Black, new XPoint(x, y));
                y += lineHeight;
                gfx.DrawString("Type de place: " + placeVendueModel.TypePlace.Type, new XFont("Verdana", 20), XBrushes.Black, new XPoint(x, y));
                y += lineHeight;
                gfx.DrawString("Numero de place: " + placeVendueModel.NumeroDePlace, new XFont("Verdana", 20), XBrushes.Black, new XPoint(x, y));
                y += lineHeight;
                gfx.DrawString("Prix: " + placeVendueModel.Prix, new XFont("Verdana", 20), XBrushes.Black, new XPoint(x, y));
                y += lineHeight;
                gfx.DrawString("Date de reservation: " + placeVendueModel.OnCreate.ToString("dd/MM/yyyy"), new XFont("Verdana", 20), XBrushes.Black, new XPoint(x, y));
        
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrImage = qrCode.GetGraphic(20);

                using (var qrStream = new MemoryStream())
                {
                    qrImage.Save(qrStream, System.Drawing.Imaging.ImageFormat.Png);
                    qrStream.Seek(0, SeekOrigin.Begin);
                    var qrBitmap = XImage.FromStream(qrStream);
                    gfx.DrawImage(qrBitmap, 20, y + lineHeight, 100, 100); 
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