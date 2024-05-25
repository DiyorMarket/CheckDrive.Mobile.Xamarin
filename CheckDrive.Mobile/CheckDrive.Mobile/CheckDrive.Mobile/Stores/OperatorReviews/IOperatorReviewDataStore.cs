using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.Mobile.Responses;

namespace CheckDrive.Web.Stores.OperatorReviews
{
    public interface IOperatorReviewDataStore
    {
        GetOperatorReviewResponse GetOperatorReviews(int driverId);
        OperatorReviewDto GetOperatorReview(int id);
        OperatorReviewDto CreateOperatorReview(OperatorReviewForCreateDto operatorReview);
        OperatorReviewDto UpdateOperatorReview(int id, OperatorReviewForUpdateDto operatorReview);
        void DeleteOperatorReview(int id);
    }
}
