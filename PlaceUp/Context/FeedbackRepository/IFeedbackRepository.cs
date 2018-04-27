using PlaceUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceUp.Context.FeedbackRepository
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetAll();

        Task Add(Feedback feedback);

        Task DeleteByGuid(Guid? guid);

        Task<Feedback> GetByGuid(Guid? guid);

        Task<bool> IsExist(Guid? guid);
    }
}
