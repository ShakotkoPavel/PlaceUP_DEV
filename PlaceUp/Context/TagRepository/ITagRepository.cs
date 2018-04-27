using PlaceUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceUp.Context
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAll();

        Task Add(Tag tag);

        Task DeleteByGuid(Guid? guid);

        Task<Tag> GetByGuid(Guid? guid);

        Task<bool> IsExist(string name);

        Task<bool> IsExist(Guid? guid);
    }
}
