using PlaceUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PlaceUp.Context.FeedbackRepository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DataContext _db;

        public FeedbackRepository(DataContext context)
        {
            _db = context;
        }

        public async Task Add(Feedback feedback)
        {
            feedback.FeedbackId = Guid.NewGuid();
            _db.Feedbacks.Add(feedback);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteByGuid(Guid? guid)
        {
            if (await IsExist(guid))
            {
                _db.Feedbacks.Remove(await GetByGuid(guid));
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Feedback>> GetAll()
        {
            return await _db.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> GetByGuid(Guid? guid)
        {
            return await _db.Feedbacks.FindAsync(guid);
        }

        public async Task<bool> IsExist(Guid? guid)
        {
            var tag = await GetByGuid(guid);
            return tag == null ? false : true;
        }
    }
}