using Dto.Scraping.Request.Email;
using Dto.Scraping.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseBase> EnviarCorreo(EmailRequest request);
    }
}
