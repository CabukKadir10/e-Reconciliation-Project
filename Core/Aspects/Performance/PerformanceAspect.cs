using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if(_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                string body = $"Performance: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}";

                SendConfirmEmail(body);
            }

            _stopwatch.Reset();
        }

        void SendConfirmEmail(string body) //????
        {
            string subject = "Performans Maili";
           
            SendMailDto sendMailDto = new SendMailDto()
            {
                Email = "cabuk0539@gmail.com",
                Password = "1",
                Port = 587,
                SMTP = "smtp.gmail.com",
                SSL = true,
                email = "cabuk0539@gmail.com",
                subject = subject,
                body = body
            };

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMailDto.Email);//burada null dönüyor
                mail.To.Add(sendMailDto.email);
                mail.Subject = sendMailDto.subject;
                mail.Body = sendMailDto.body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add();

                using (SmtpClient smtp = new SmtpClient(sendMailDto.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendMailDto.Email, sendMailDto.Password);
                    smtp.EnableSsl = sendMailDto.SSL;
                    smtp.Port = sendMailDto.Port;
                    smtp.Send(mail);
                }
            }
        }
    }

    public class SendMailDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string email { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
    }
}
