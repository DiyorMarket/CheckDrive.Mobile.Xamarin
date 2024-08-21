using CheckDrive.Mobile.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Mobile.Stores.DispatcherReviewDataStore
{
    public interface IDispatcherReviewDataStore
    {
        Task<GetDispatcherReviewResponse> GetDispatcherReviewResponses(int driverId);
    }
}
