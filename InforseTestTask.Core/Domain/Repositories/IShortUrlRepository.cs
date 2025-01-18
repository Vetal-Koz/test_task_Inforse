using InforseTestTask.Core.Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Domain.Repositories
{
    public interface IShortUrlRepository : ICrudRepository<ShortUrl>
    {
        Task<bool> IsExistByOriginalUrl(string originalUrl);
        Task<ShortUrl?> FindByShortCodeAsync(string shortCode);
    }
}
