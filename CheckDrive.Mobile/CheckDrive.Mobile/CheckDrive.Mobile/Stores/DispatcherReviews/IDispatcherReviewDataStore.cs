using CheckDrive.ApiContracts.DispatcherReview;
using CheckDrive.Mobile.Responses;

namespace CheckDrive.Web.Stores.DispatcherReviews
{
    public interface IDispatcherReviewDataStore
    {
        GetDispatcherReviewResponse GetDispatcherReviews();
        DispatcherReviewDto GetDispatcherReview(int id);
        DispatcherReviewDto CreateDispatcherReview(DispatcherReviewForCreateDto dispatcherReview);
        DispatcherReviewDto UpdateDispatcherReview(int id, DispatcherReviewForUpdateDto dispatcherReview);
        void DeleteDispatcherReview(int id);
    }
}
