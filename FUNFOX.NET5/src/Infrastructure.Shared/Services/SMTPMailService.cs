using FUNFOX.NET5.Application.Configurations;
using FUNFOX.NET5.Application.Interfaces.Services;
using FUNFOX.NET5.Application.Requests.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Infrastructure.Shared.Services
{
    public class SMTPMailService : IMailService
    {
        private readonly MailConfiguration _config;
        private readonly ILogger<SMTPMailService> _logger;

        public SMTPMailService(IOptions<MailConfiguration> config, ILogger<SMTPMailService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_config.DisplayName, _config.From));
                message.To.Add(new MailboxAddress("", request.To));
                message.Subject = request.Subject;

                message.Body = new TextPart("plain")
                {
                    Text = request.Body
                };
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.UserName, _config.Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            } 
           /* BaseResponse<string> response = new BaseResponse<string>();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PUCON", "f20ba127@ibitpu.edu.pk"));
            message.To.Add(new MailboxAddress("", request.To));
            message.Subject = request.Subject ;

            message.Body = new TextPart("plain")
            {
                Text = request.Body
            };



            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("f20ba127@ibitpu.edu.pk", "jokp myjv lnnl uohu");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                   // response.Success = true;
                   // return response;
                }
                catch (Exception ex)
                {
                   // response.Success = false;
                    // response.Message = ex.Message;
                   // return response;
                }*/
            }

        }
    }
