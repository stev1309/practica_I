using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class EmailProvider
    {

        #region Attributes

        private string emailTransmitter;
        private string passwordTransmitter;
        private string emailSubject;
        private int portTransmitter;
        private string hostTransmitter;
        private string emailBody;
        private bool emailHTML;
        private string emailDestination;
        private string nameTransmitter;

        #endregion

        #region Properties
        public string EmailTransmitter
        {
            get { return this.emailTransmitter; }
            set { this.emailTransmitter = value; }
        }
        public string PasswordTransmitter
        {
            get { return this.passwordTransmitter; }
            set { this.passwordTransmitter = value; }
        }
        public string EmailSubject
        {
            get { return this.emailSubject; }
            set { this.emailSubject = value; }
        }
        public int PortTransmitter
        {
            get { return this.portTransmitter; }
            set { this.portTransmitter = value; }
        }
        public string HostTransmitter
        {
            get { return this.hostTransmitter; }
            set { this.hostTransmitter = value; }
        }
        public string EmailBody
        {
            get { return this.emailBody; }
            set { this.emailBody = value; }
        }
        public bool EmailHTML
        {
            get { return this.emailHTML; }
            set { this.emailHTML = value; }
        }
        public string EmailDestination
        {
            get { return this.emailDestination; }
            set { this.emailDestination = value; }
        }
        public string NameTransmitter
        {
            get { return this.nameTransmitter; }
            set { this.nameTransmitter = value; }
        }
        #endregion

        #region Builder
        public EmailProvider()
        {
        }

        public EmailProvider(string emailSubject, string emailBody, string emailDestination, string nameTransmitter = "Steven Luna", string emailTransmitter = "adriantj1309@gmail.com", string passwordTransmitter = "w123df55133sdf", int portTransmitter = 587, string hostTransmitter = "smtp.gmail.com", bool emailHTML = true)
        {
            this.emailSubject = emailSubject;
            this.emailTransmitter = emailTransmitter;
            this.passwordTransmitter = passwordTransmitter;
            this.portTransmitter = portTransmitter;
            this.hostTransmitter = hostTransmitter;
            this.emailBody = emailBody;
            this.emailHTML = emailHTML;
            this.emailDestination = emailDestination;
            this.nameTransmitter = nameTransmitter;
        }
        #endregion

        #region Methods
        public Task SendEmailAsync()
        {
            try
            {
                NetworkCredential credential = new NetworkCredential(EmailTransmitter, PasswordTransmitter);

                // Command-line argument must be the SMTP host.
                SmtpClient client = new SmtpClient()
                {
                    Port = PortTransmitter,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = HostTransmitter,
                    EnableSsl = true,
                    Credentials = credential,
                };

                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(EmailTransmitter, NameTransmitter),
                    Subject = EmailSubject,
                    IsBodyHtml = EmailHTML,
                    Body = EmailBody,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    Priority = MailPriority.Normal,
                };

                message.To.Add(new MailAddress(EmailDestination));

                client.Send(message);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("No se envió el correo: </br> " + ex.Message + "</br> " + Task.CompletedTask.Exception.Message);

            }
            return Task.CompletedTask;
        }
        #endregion
    }
}
