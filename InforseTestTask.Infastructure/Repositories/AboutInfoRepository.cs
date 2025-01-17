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
    public class AboutInfoRepository : IAboutInfoRepository
    {
        private readonly InforseDBContext _dbContext;

        public AboutInfoRepository(InforseDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AboutInfo> CreateAsync(AboutInfo entity)
        {
            _dbContext.AboutInfos.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbContext.AboutInfos.FindAsync(id);
            if(entity != null)
            {
                _dbContext.AboutInfos.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<ICollection<AboutInfo>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AboutInfo?> FindByIdAsync(long id)
        {
            return await _dbContext.AboutInfos.FindAsync(id);
        }

        public async Task UpdateAsync(AboutInfo entity)
        {
            _dbContext.AboutInfos.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
