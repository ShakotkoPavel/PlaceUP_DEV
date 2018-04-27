using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PlaceUp.Models;
using System.Data.Entity;

namespace PlaceUp.Context.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _db;
        
        public CategoryRepository(DataContext context)
        {
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            _db = context;
        }

        public async Task Add(Category category)
        {
            if (!await IsExist(category.Name))
            {
                category.CategoryId = Guid.NewGuid();
                _db.Categories.Add(category);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteByGuid(Guid? guid)
        {
            if (await IsExist(guid))
            {
                _db.Categories.Remove(await GetByGuid(guid));
                await _db.SaveChangesAsync();
            }
        }

        public async Task Edit(Category category)
        {
            if (await IsExist(category.CategoryId))
            {
                var originalCategory = await GetByGuid(category.CategoryId);
                _db.Entry(originalCategory).CurrentValues.SetValues(category);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetByGuid(Guid? guid)
        {
            return await _db.Categories.FindAsync(guid);
        }

        public async Task<bool> IsExist(string name)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == name.Trim().ToLower());
            return category == null ? false : true;
        }

        public async Task<bool> IsExist(Guid? guid)
        {
            var category = await GetByGuid(guid);
            return category == null ? false : true;
        }
    }
}