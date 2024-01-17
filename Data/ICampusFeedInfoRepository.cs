using CampusFeedApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusFeedApi.Data
{
    public interface ICampusFeedInfoRepository : IRepository<CampusFeedInfo>
    {
        Task<List<CampusFeedInfo>> GetAllAsync();
        Task<CampusFeedInfo?> GetById(string id);

    }
}
