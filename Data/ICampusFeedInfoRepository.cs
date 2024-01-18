using CampusFeedApi.Dto;
using CampusFeedApi.Models;

namespace CampusFeedApi.Data
{
    public interface ICampusFeedInfoRepository : IRepository<CampusFeedInfo>
    {
        void Add(CampusFeedDto feed);
        Task<List<CampusFeedInfo>> GetAllAsync();
        Task<CampusFeedInfo?> GetById(string id);

    }
}
