using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Services
{
    public interface IShortUrlService : ICrudService<UrlRequest, UrlResponse>
    {
    }
}
