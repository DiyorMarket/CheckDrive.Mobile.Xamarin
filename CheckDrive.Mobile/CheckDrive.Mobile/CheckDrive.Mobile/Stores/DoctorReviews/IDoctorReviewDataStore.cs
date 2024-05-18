using CheckDrive.ApiContracts.DoctorReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DoctorReviews
{
    public interface IDoctorReviewDataStore
    {
        Task<List<DoctorReviewDto>> GetDoctorReviews();
        Task<DoctorReviewDto> GetDoctorReview(int id);
        Task<DoctorReviewDto> CreateDoctorReview(DoctorReviewDto review);
        Task<DoctorReviewDto> UpdateDoctorReview(int id, DoctorReviewDto review);
        Task DeleteDoctorReview(int id);
    }
}
