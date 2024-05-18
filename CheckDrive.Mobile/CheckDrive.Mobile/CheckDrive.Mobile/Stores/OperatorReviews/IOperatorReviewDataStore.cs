using CheckDrive.DTOs.OperatorReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.OperatorReviews
{
    public interface IOperatorReviewDataStore
    {
        Task<List<OperatorReviewDto>> GetOperatorReviews();
        Task<OperatorReviewDto> GetOperatorReview(int id);
        Task<OperatorReviewDto> CreateOperatorReview(OperatorReviewDto operatorReview);
        Task<OperatorReviewDto> UpdateOperatorReview(int id, OperatorReviewDto operatorReview);
        Task DeleteOperatorReview(int id);
    }
}
