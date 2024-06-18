using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.Mobile.Responses;
using System;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.OperatorReviews
{
    public interface IOperatorReviewDataStore
    {
        GetOperatorReviewResponse GetOperatorReviewsAsync();
        GetOperatorReviewResponse GetOperatorReviewsByDriverIdAsync(int driverId);
        GetOperatorReviewResponse GetOperatorReviewsAsync(DateTime date);
        OperatorReviewDto GetOperatorReviewAsync(int id);
        OperatorReviewDto CreateOperatorReviewAsync(OperatorReviewForCreateDto operatorReview);
    }
}
