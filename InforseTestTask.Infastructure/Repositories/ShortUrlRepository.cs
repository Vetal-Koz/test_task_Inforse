using InforseTestTask.Core.Domain.Entityes;
using InforseTestTask.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Infastructure.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly InforseDBContext _dbContext;

        public ShortUrlRepository(InforseDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ShortUrl> CreateAsync(ShortUrl entity)
        {
            _dbContext.Attach(entity.CreatedBy);
            _dbContext.ShortUrls.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbContext.ShortUrls.FindAsync(id);
            if (entity != null)
            {
                _dbContext.ShortUrls.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ShortUrl>> FindAllAsync()
        {
            return await _dbContext.ShortUrls.Include(su => su.CreatedBy).ToListAsync();
        }

        public async Task<ShortUrl?> FindByIdAsync(long id)
        {
            return await _dbContext.ShortUrls
            .Include(s => s.CreatedBy)
            .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ShortUrl?> FindByOriginalUrl(string originalUrl)
        {
            return await _dbContext.ShortUrls.FirstOrDefaultAsync(url => url.OriginalUrl == originalUrl);
        }

        public async Task<ShortUrl?> FindByShortCodeAsync(string shortCode)
        {
            return await _dbContext.ShortUrls.Include(su => su.CreatedBy).FirstOrDefaultAsync(x => x.ShortenedUrl.EndsWith($"/{shortCode}"));
        }

        public async Task<bool> IsExistByOriginalUrl(string originalUrl)
        {
            return await _dbContext.ShortUrls.AnyAsync(url => url.OriginalUrl == originalUrl);
        }

        public async Task UpdateAsync(ShortUrl entity)
        {
            _dbContext.ShortUrls.Update(entity);
            await _dbContext.SaveChangesAsync();
        }


    }
}
