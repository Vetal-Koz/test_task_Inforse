using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Services
{
    public interface ICrudService<REQ, RES> where REQ : ApiRequest where RES : ApiResponse
    {
        Task<RES> CreateAsync(REQ req);
        Task UpdateAsync(REQ req, long id);
        Task DeleteAsync(long id);
        Task<RES> FindById(long id);
        Task<List<RES>> FindAll();
    }
}
