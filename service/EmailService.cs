using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using MailKit.Net.Smtp;
using MimeKit;

namespace TicketPlace2._0.service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string htmlBody)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Smtp:Username"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = htmlBody };

            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        public void mailBody(string subject, string email,string evenementName, string date, string place, string prix, int idEvenement ){
            string htmlBody = $@"
                <h1>{subject}</h1>
                <p>Cher(e) client(e),</p>
                <p>Nous vous remercions pour votre achat de billet pour l'événement {evenementName} qui aura lieu le {date}.</p>
                <p>Vous avez acheté une place pour un montant total de {prix}.</p>
                <p>Vous pouvez télécharger votre billet ici</p>
                <a href='http://localhost:5125/EvenementTypePlace/DownloadTicket?idEvenement={idEvenement}&numeroTicket={place}'>Télécharger mon billet</a>
                <p>Merci de votre confiance.</p>
                <p>L'équipe TicketPlace</p>";

            SendEmail(email, subject, htmlBody);
        }
    }
}