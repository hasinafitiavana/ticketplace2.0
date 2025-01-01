using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace TicketPlace2._0.Service
{
    public class TicketService
    {
        // HttpClient est thread-safe et peut être réutilisé pour plusieurs requêtes HTTP
        private static readonly HttpClient client = new HttpClient();

        // Méthode pour générer un code QR
        public async Task<byte[]> GenerateQrCodeAsync(string text, int size = 200)
        {
            // Encoder le texte pour qu'il soit sûr pour une URL
            string encodedText = Uri.EscapeDataString(text);
            string url = $"https://chart.googleapis.com/chart?chs={size}x{size}&cht=qr&chl={encodedText}";

            // Télécharger l'image du code QR depuis l'URL
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Lire le contenu de la réponse en tant que tableau de bytes
            return await response.Content.ReadAsByteArrayAsync();
        }

        // Méthode pour générer un PDF contenant un code QR et du texte
        public async Task<byte[]> GeneratePdfWithQrCodeAsync(string text, string qrCodeText)
        {
            // Générer le code QR
            var qrCodeImageBytes = await GenerateQrCodeAsync(qrCodeText);

            // Créer un nouveau document PDF
            var document = new PdfDocument();
            var page = document.AddPage();
            using (XGraphics gfx = XGraphics.FromPdfPage(page))
            {
                // Charger l'image du code QR
                using (var ms = new MemoryStream(qrCodeImageBytes))
                {
                    var qrCodeImage = XImage.FromStream(ms);

                    // Dessiner le code QR sur le PDF
                    gfx.DrawImage(qrCodeImage, 20, 20, 100, 100);

                    // Ajouter du texte au PDF
                    gfx.DrawString(text, 
                                   new XFont("Verdana", 20), 
                                   XBrushes.Black, 
                                   new XRect(140, 40, page.Width, page.Height), 
                                   XStringFormats.TopLeft);
                }
            }

            // Sauvegarder le document PDF dans un MemoryStream
            using (var ms = new MemoryStream())
            {
                document.Save(ms, false);
                return ms.ToArray();
            }
        }
        public byte[] GeneratePdf(string content)
        {
            // Créer un nouveau document PDF
            var document = new PdfDocument();
            var page = document.AddPage();
            using (XGraphics gfx = XGraphics.FromPdfPage(page))
            {
                // Ajouter du texte au PDF
                gfx.DrawString(content, 
                               new XFont("Verdana", 20), 
                               XBrushes.Black, 
                               new XRect(20, 40, page.Width - 40, page.Height - 40), 
                               XStringFormats.TopLeft);
            }

            // Sauvegarder le document PDF dans un MemoryStream
            using (var ms = new MemoryStream())
            {
                document.Save(ms, false);
                return ms.ToArray();
            }
        }
    }
}