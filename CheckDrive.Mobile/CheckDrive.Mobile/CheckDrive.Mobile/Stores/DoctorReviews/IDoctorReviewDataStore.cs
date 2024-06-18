using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.Mobile.Responses;
using System;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DoctorReviews
{
    public interface IDoctorReviewDataStore
    {
        GetDoctorReviewResponse GetDoctorReviewsAsync(DateTime date);
        GetDoctorReviewResponse GetDoctorReviewsAsync();
        GetDoctorReviewResponse GetDoctorReviewsByDriverIdAsync(int driverId);
        DoctorReviewDto GetDoctorReviewAsync(int id);
        DoctorReviewDto CreateDoctorReviewAsync(DoctorReviewForCreateDto review);
    }
}