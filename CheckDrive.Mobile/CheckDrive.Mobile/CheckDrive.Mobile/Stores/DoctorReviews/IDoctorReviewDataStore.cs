using CheckDrive.ApiContracts.DoctorReview;
using CheckDrive.Mobile.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DoctorReviews
{
    public interface IDoctorReviewDataStore
    {
        GetDoctorReviewResponse GetDoctorReviews();
        GetDoctorReviewResponse GetDoctorReviewsByDriverId(int driverId);
        DoctorReviewDto GetDoctorReview(int id);
        DoctorReviewDto CreateDoctorReview(DoctorReviewForCreateDto review);
        DoctorReviewDto UpdateDoctorReview(int id, DoctorReviewForUpdateDto review);
        void DeleteDoctorReview(int id);
    }
}