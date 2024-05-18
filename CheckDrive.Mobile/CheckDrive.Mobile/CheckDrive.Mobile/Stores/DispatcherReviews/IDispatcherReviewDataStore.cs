using CheckDrive.ApiContracts.DispatcherReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DispatcherReviews
{
    public interface IDispatcherReviewDataStore
    {
        Task<List<DispatcherReviewDto>> GetDispatcherReviews();
        Task<DispatcherReviewDto> GetDispatcherReview(int id);
        Task<DispatcherReviewDto> CreateDispatcherReview(DispatcherReviewDto review);
        Task<DispatcherReviewDto> UpdateDispatcherReview(int id, DispatcherReviewDto review);
        Task DeleteDispatcherReview(int id);
    }
}
