using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.Mobile.Responses;
using System;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.OperatorReviews
{
    public interface IOperatorReviewDataStore
    {
        Task<GetOperatorReviewResponse> GetOperatorReviewsAsync();
        Task<GetOperatorReviewResponse> GetOperatorReviewsByDriverIdAsync(int driverId);
        Task<GetOperatorReviewResponse> GetOperatorReviewsAsync(DateTime date, int driverId);
        Task<OperatorReviewDto> GetOperatorReviewAsync(int id);
    }
}
