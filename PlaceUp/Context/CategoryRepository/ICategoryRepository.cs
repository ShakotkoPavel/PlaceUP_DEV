using PlaceUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PlaceUp.Context.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

        Task Add(Category category);

        Task DeleteByGuid(Guid? guid);

        Task<Category> GetByGuid(Guid? guid);

        Task Edit(Category place);

        Task<bool> IsExist(string name);

        Task<bool> IsExist(Guid? guid);
    }
}