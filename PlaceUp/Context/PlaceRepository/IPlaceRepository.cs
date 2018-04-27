using PlaceUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceUp.Context.PlaceRepository
{
    public interface IPlaceRepository
    {
        IEnumerable<Place> Places { get; }
        
        Task<IEnumerable<Place>> GetAll();

        Task<IEnumerable<Place>> GetAllByTag(string name);

        Task<IEnumerable<Place>> GetAllByCategoryId(Guid? guid);

        Task<IEnumerable<Place>> GetAllByTagId(Guid? guid);

        Task Add(Place place);

        Task DeleteByGuid(Guid? guid);

        Task<Place> GetByGuid(Guid? guid);

        Task Edit(Place place);

        Task<bool> IsExist(Guid? guid);

        Task<IEnumerable<Category>> GetAllCategories();
    }
}
