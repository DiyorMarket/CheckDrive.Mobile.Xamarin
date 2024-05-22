using CheckDrive.ApiContracts.DispatcherReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DispatcherReviews
{
    public interface IDispatcherReviewDataStore
    {
        Task<List<DispatcherReviewDto>> GetDispatcherReviewsAsync();
        Task<DispatcherReviewDto> GetDispatcherReviewAsync(int id);
        Task<DispatcherReviewDto> CreateDispatcherReviewAsync(DispatcherReviewForCreateDto dispatcherReview);
        Task<DispatcherReviewDto> UpdateDispatcherReviewAsync(int id, DispatcherReviewForUpdateDto dispatcherReview);
        Task DeleteDispatcherReviewAsync(int id);
    }
}
