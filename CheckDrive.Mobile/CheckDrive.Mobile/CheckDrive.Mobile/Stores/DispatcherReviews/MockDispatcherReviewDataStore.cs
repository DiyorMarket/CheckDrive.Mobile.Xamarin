using CheckDrive.ApiContracts.DispatcherReview;
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
                new DispatcherReviewDto { Id = 1, FuelSpended = 50, DistanceCovered = 100, Date = DateTime.Now, DriverId = 1, DriverName = "John", DispatcherId = 1, DispatcherName = "Harry1", MechanicId = 1, MechanicName = "Bill1", OperatorId = 1, OperatorName = "Jack1"},
                new DispatcherReviewDto { Id = 2, FuelSpended = 45, DistanceCovered = 120, Date = DateTime.Now, DriverId = 1, DriverName = "John", DispatcherId = 2, DispatcherName = "Harry2", MechanicId = 2, MechanicName = "Bill2", OperatorId = 2, OperatorName = "Jack2"},
                new DispatcherReviewDto { Id = 3, FuelSpended = 40, DistanceCovered = 80, Date = DateTime.Now, DriverId = 1, DriverName = "John", DispatcherId = 2, DispatcherName = "Harry2", MechanicId = 1, MechanicName = "Bill1", OperatorId = 3, OperatorName = "Jack3"},
                new DispatcherReviewDto { Id = 4, FuelSpended = 55, DistanceCovered = 90, Date = DateTime.Now, DriverId = 4, DriverName = "John4", DispatcherId = 3, DispatcherName = "Harry3", MechanicId = 3, MechanicName = "Bill3", OperatorId = 4, OperatorName = "Jack4"},
                new DispatcherReviewDto { Id = 5, FuelSpended = 60, DistanceCovered = 50, Date = DateTime.Now, DriverId = 3, DriverName = "John3", DispatcherId = 3, DispatcherName = "Harry3", MechanicId = 1, MechanicName = "Bill1", OperatorId = 5, OperatorName = "Jack5"},
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
