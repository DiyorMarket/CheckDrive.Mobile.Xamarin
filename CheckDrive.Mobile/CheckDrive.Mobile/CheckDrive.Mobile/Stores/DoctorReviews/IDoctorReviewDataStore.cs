using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DoctorReviews
{
    public interface IDoctorReviewDataStore
    {
        Task<List<DoctorReviewDto>> GetDoctorReviews();
        Task<DoctorReviewDto> GetDoctorReview(int id);
        Task<DoctorReviewDto> CreateDoctorReview(DoctorReviewForCreateDto review);
        Task<DoctorReviewDto> UpdateDoctorReview(int id, DoctorReviewForUpdateDto review);
        Task DeleteDoctorReview(int id);
    }
}
