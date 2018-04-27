using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;
using PlaceUp.Models;
using System.Data.Entity;

namespace PlaceUp.Context.PlaceRepository
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly DataContext _db;

        public PlaceRepository(DataContext context)
        {
            _db = context;
        }

        public IEnumerable<Place> Places
        {
            get { return _db.Places; }
        }

        public async Task<IEnumerable<Place>> GetAll()
        {
            return await _db.Places.ToListAsync();
        }

        public async Task<IEnumerable<Place>> GetAllByTag(string name)
        {
            return await _db.Places.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _db.Categories.ToListAsync();
        }


        public async Task<IEnumerable<Place>> GetAllByCategoryId(Guid? guid)
        {
            return await _db.Places.Where(x => x.CategoryId == guid).ToListAsync();
        }

        public async Task<IEnumerable<Place>> GetAllByTagId(Guid? guid)
        {
            return await _db.Places.Where(s => s.Tags.Any(g => g.TagId == guid)).ToListAsync();
        }

        public async Task Add(Place place)
        {
            place.PlaceId = Guid.NewGuid();
            _db.Places.Add(place);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteByGuid(Guid? guid)
        {
            if (await IsExist(guid))
            {
                _db.Places.Remove(await GetByGuid(guid));
                await _db.SaveChangesAsync();
            }
        }

        public async Task Edit(Place place)
        {
            if (await IsExist(place.PlaceId))
            {
                var originalPlace = await GetByGuid(place.PlaceId);
                _db.Entry(originalPlace).CurrentValues.SetValues(place);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Place> GetByGuid(Guid? guid)
        {
            return await _db.Places.FindAsync(guid);
        }

        public async Task<bool> IsExist(Guid? guid)
        {
            var place = await GetByGuid(guid);
            return place == null ? false : true;
        }
    }
}