using Microsoft.EntityFrameworkCore;
using CampusFeedApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusFeedApi.Data
{
    public class CampusFeedInfoRepository : ICampusFeedInfoRepository
    {
        private readonly CampusFeedInfoDataContext _context;

        public CampusFeedInfoRepository(CampusFeedInfoDataContext context)
        {
            _context = context;
        }

        public void Add(CampusFeedInfo newT)
        {
            _context.Add(newT);
        }

        public void Delete(CampusFeedInfo newT)
        {
            _context.Remove(newT);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update<K>(K id, CampusFeedInfo input)
        {
            // Get the feed
            var theFeed = await _context.CampusFeedInfo.FindAsync(id);

            if (theFeed == null)
            {
                // Handle the case where the feed with the specified id is not found
                return false;
            }

            theFeed.CampusFeedId = input.CampusFeedId;
            theFeed.Content = input.Content;
            theFeed.Category = input.Category;
            theFeed.Date = input.Date;
            theFeed.Like = input.Like;
            theFeed.Dislike = input.Dislike;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CampusFeedInfo>> GetAllAsync()
        {
            return await _context.CampusFeedInfo.ToListAsync();
        }

        public async Task<CampusFeedInfo?> GetById(string id)
        {
            return await _context.CampusFeedInfo.FirstOrDefaultAsync(x => x.CampusFeedId == id);
        }

        void IRepository<CampusFeedInfo>.Update<K>(K id, CampusFeedInfo input)
        {
            throw new NotImplementedException();
        }
    }
}
