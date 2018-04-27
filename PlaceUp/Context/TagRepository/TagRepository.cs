using PlaceUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PlaceUp.Context.TagRepository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _db;

        public TagRepository(DataContext context)
        {
            _db = context;
        }

        public async Task Add(Tag tag)
        {
            if (!await IsExist(tag.Name))
            {
                tag.TagId = Guid.NewGuid();
                _db.Tags.Add(tag);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteByGuid(Guid? guid)
        {
            if (await IsExist(guid))
            {
                _db.Tags.Remove(await GetByGuid(guid));
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _db.Tags.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Tag> GetByGuid(Guid? guid)
        {
            return await _db.Tags.FindAsync(guid);
        }

        public async Task<bool> IsExist(string name)
        {
            var tag = await _db.Tags.FirstOrDefaultAsync(x => x.Name.ToLower() == name.Trim().ToLower());
            return tag == null ? false : true;
        }

        public async Task<bool> IsExist(Guid? guid)
        {
            var tag = await GetByGuid(guid);
            return tag == null ? false : true;
        }
    }
}