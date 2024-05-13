using CheckDrive.Domain.DTOs.Dispatcher;
using CheckDrive.Domain.DTOs.DispatcherReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.DataStores.Dispatcher
{
    public interface IDispatcherReviewDataStore
    {
        Task<IEnumerable<DispatcherReviewDto>> GetReviewsAsync();
    }
}
