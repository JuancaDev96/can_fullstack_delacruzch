using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Request.Email
{
    public class EmailRequest
    {
        public string Destinatario { get; set; } = default!;
        public string Asunto { get; set; } = default!;
        public string Cuerpo { get; set; } = default!;
    }
}
