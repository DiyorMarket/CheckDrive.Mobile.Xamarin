using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.Mobile.Responses;
using System;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DoctorReviews
{
    public interface IDoctorReviewDataStore
    {
        Task<GetDoctorReviewResponse> GetDoctorReviewsAsync(DateTime date, int driverId);
        Task<GetDoctorReviewResponse> GetDoctorReviewsAsync();
        Task<GetDoctorReviewResponse> GetDoctorReviewsByDriverIdAsync(int driverId);
        Task<DoctorReviewDto> GetDoctorReviewAsync(int id);
    }
}