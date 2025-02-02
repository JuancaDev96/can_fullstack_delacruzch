using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Response
{
    public class ResponseBase
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
    }

    public class ResponseBase<T> : ResponseBase
    {
        public T? Data { get; set; }
    }
}
