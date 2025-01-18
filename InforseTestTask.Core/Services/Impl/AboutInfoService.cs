using InforseTestTask.Core.Domain.Repositories;
using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.DTO.Response;
using InforseTestTask.Core.Exceptions;

namespace InforseTestTask.Core.Services.Impl
{
    public class AboutInfoService : IAboutInfoService
    {
        private readonly IAboutInfoRepository _aboutInfoRepository;

        public AboutInfoService(IAboutInfoRepository aboutInfoRepository)
        {
            _aboutInfoRepository = aboutInfoRepository;
        }

        public Task<AboutInfoResponse> CreateAsync(AboutInfoRequest req)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AboutInfoResponse>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<AboutInfoResponse> FindById(long id)
        {
            var info = await _aboutInfoRepository.FindByIdAsync(id);
            if (info == null)
            {
                throw new EntityNotFoundException("Entity not found");
            }
            return new AboutInfoResponse(info);
        }

        public async Task UpdateAsync(AboutInfoRequest req, long id)
        {
            var info = await _aboutInfoRepository.FindByIdAsync(id);
            if (info == null)
            {
                throw new EntityNotFoundException("Entity not found");
            }
            info.Description = req.Content;
            info.LastUpdated = DateTime.UtcNow;
            await _aboutInfoRepository.UpdateAsync(info);
        }
    }
}
