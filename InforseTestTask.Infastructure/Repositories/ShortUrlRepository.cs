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
            return await _dbContext.ShortUrls.ToListAsync();
        }

        public async Task<ShortUrl?> FindByIdAsync(long id)
        {
            return await _dbContext.ShortUrls.FindAsync(id);
        }

        public async Task UpdateAsync(ShortUrl entity)
        {
            _dbContext.ShortUrls.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
