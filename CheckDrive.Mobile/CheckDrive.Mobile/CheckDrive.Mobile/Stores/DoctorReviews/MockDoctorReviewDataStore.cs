using CheckDrive.ApiContracts.DoctorReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DoctorReviews
{
    public class MockDoctorReviewDataStore : IDoctorReviewDataStore
    {
        private readonly List<DoctorReviewDto> _reviews;

        public MockDoctorReviewDataStore()
        {
            _reviews = new List<DoctorReviewDto>
            {
                new DoctorReviewDto { Id = 1, IsHealthy = true, Comments = "Good condition", Date = DateTime.Now, DriverId = 1, DriverName = "Jose" },
                new DoctorReviewDto { Id = 2, IsHealthy = false, Comments = "Needs rest", Date = DateTime.Now.AddDays(-1), DriverId = 2, DriverName = "Willian" },
                new DoctorReviewDto { Id = 3, IsHealthy = true, Comments = "Good condition", Date = DateTime.Now.AddDays(-2), DriverId = 1, DriverName = "Jose" },
                new DoctorReviewDto { Id = 4, IsHealthy = false, Comments = "Needs rest", Date = DateTime.Now.AddDays(-2), DriverId = 2, DriverName = "Willian" },
                new DoctorReviewDto { Id = 5, IsHealthy = true, Comments = "Good condition", Date = DateTime.Now.AddDays(-3), DriverId = 1, DriverName = "Jose" },
                new DoctorReviewDto { Id = 6, IsHealthy = false, Comments = "Needs rest", Date = DateTime.Now.AddDays(-4), DriverId = 2, DriverName = "Willian" },
                new DoctorReviewDto { Id = 7, IsHealthy = true, Comments = "Good condition", Date = DateTime.Now.AddDays(-5), DriverId = 1, DriverName = "Jose" },
                new DoctorReviewDto { Id = 8, IsHealthy = false, Comments = "Needs rest", Date = DateTime.Now.AddDays(-5), DriverId = 2, DriverName = "Willian" },
            };
        }

        public async Task<List<DoctorReviewDto>> GetDoctorReviews()
        {
            await Task.Delay(100);
            return _reviews.ToList();
        }

        public async Task<DoctorReviewDto> GetDoctorReview(int id)
        {
            await Task.Delay(100); 
            return _reviews.FirstOrDefault(r => r.Id == id);
        }

        public async Task<DoctorReviewDto> CreateDoctorReview(DoctorReviewDto review)
        {
            await Task.Delay(100); 
            review.Id = _reviews.Max(r => r.Id) + 1;
            _reviews.Add(review);
            return review;
        }

        public async Task<DoctorReviewDto> UpdateDoctorReview(int id, DoctorReviewDto review)
        {
            await Task.Delay(100);
            var existingReview = _reviews.FirstOrDefault(r => r.Id == id);
            if (existingReview != null)
            {
                existingReview.IsHealthy = review.IsHealthy;
                existingReview.Comments = review.Comments;
                existingReview.Date = review.Date;
            }
            return existingReview;
        }

        public async Task DeleteDoctorReview(int id)
        {
            await Task.Delay(100); 
            var reviewToRemove = _reviews.FirstOrDefault(r => r.Id == id);
            if (reviewToRemove != null)
            {
                _reviews.Remove(reviewToRemove);
            }
        }
    }
}
