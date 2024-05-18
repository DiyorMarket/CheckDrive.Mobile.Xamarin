using CheckDrive.DTOs.DispatcherReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.DispatcherReviews
{
    public class MockDispatcherReviewDataStore : IDispatcherReviewDataStore
    {
        private readonly List<DispatcherReviewDto> _reviews;

        public MockDispatcherReviewDataStore()
        {
            _reviews = new List<DispatcherReviewDto>
            {
                new DispatcherReviewDto { Id = 1, FuelSpended = 50, DistanceCovered = 100, Date = DateTime.Now, DriverId = 2},
                new DispatcherReviewDto { Id = 2, FuelSpended = 60, DistanceCovered = 120, Date = DateTime.Now.AddDays(-1), DriverId = 2},
                new DispatcherReviewDto { Id = 2, FuelSpended = 60, DistanceCovered = 120, Date = DateTime.Now.AddDays(-2) },
                new DispatcherReviewDto { Id = 2, FuelSpended = 60, DistanceCovered = 120, Date = DateTime.Now.AddDays(-3) , DriverId = 2},
                new DispatcherReviewDto { Id = 2, FuelSpended = 60, DistanceCovered = 120, Date = DateTime.Now.AddDays(-4) , DriverId = 2 },
                new DispatcherReviewDto { Id = 2, FuelSpended = 60, DistanceCovered = 120, Date = DateTime.Now.AddDays(-5) },
            };
        }

        public async Task<List<DispatcherReviewDto>> GetDispatcherReviews()
        {
            await Task.Delay(100);
            return _reviews.ToList();
        }

        public async Task<DispatcherReviewDto> GetDispatcherReview(int id)
        {
            await Task.Delay(100);
            return _reviews.FirstOrDefault(r => r.Id == id);
        }

        public async Task<DispatcherReviewDto> CreateDispatcherReview(DispatcherReviewDto review)
        {
            await Task.Delay(100);
            review.Id = _reviews.Max(r => r.Id) + 1;
            _reviews.Add(review);
            return review;
        }

        public async Task<DispatcherReviewDto> UpdateDispatcherReview(int id, DispatcherReviewDto review)
        {
            await Task.Delay(100);
            var existingReview = _reviews.FirstOrDefault(r => r.Id == id);
            if (existingReview != null)
            {
                existingReview.FuelSpended = review.FuelSpended;
                existingReview.DistanceCovered = review.DistanceCovered;
                existingReview.Date = review.Date;
            }
            return existingReview;
        }

        public async Task DeleteDispatcherReview(int id)
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
