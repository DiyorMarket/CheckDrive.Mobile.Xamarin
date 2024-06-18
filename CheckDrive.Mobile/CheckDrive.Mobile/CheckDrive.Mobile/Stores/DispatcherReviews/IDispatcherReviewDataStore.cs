using CheckDrive.ApiContracts.DispatcherReview;
using CheckDrive.Mobile.Responses;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DispatcherReviews
{
    public interface IDispatcherReviewDataStore
    {
        GetDispatcherReviewResponse GetDispatcherReviewsAsync();
        DispatcherReviewDto GetDispatcherReviewAsync(int id);
        DispatcherReviewDto CreateDispatcherReviewAsync(DispatcherReviewForCreateDto dispatcherReview);
    }
}
