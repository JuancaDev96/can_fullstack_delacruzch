using Dto.Scraping.Request.Email;
using Dto.Scraping.Response;
using Microsoft.Extensions.Configuration;
using Repositorios.Scraping.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Implementaciones
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ResponseBase> EnviarCorreo(EmailRequest request)
        {
            var respuesta = new ResponseBase();
            try
            {
                var smtpSetting = _config.GetSection("SmtpSettings");

                using (var client = new SmtpClient(smtpSetting["Servidor"], Convert.ToInt32(smtpSetting["Port"])))
                {
                    client.Credentials = new NetworkCredential(smtpSetting["Usuario"], smtpSetting["Password"]);
                    client.EnableSsl = true;

                    var correo = new MailMessage(smtpSetting["Usuario"]!, request.Destinatario)
                    {
                        Subject = request.Asunto,
                        Body = request.Cuerpo,
                        IsBodyHtml = true
                    };

                    await client.SendMailAsync(correo);
                    respuesta.Message = "Correo enviado correctamente";
                    respuesta.Success = true; 
                }

            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }
            return respuesta;
        }
    }
}
