using CheckDrive.DTOs;
using CheckDrive.DTOs.OperatorReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.OperatorReviews
{
    public class MockOperatorReviewDataStore : IOperatorReviewDataStore
    {
        private readonly List<OperatorReviewDto> _operatorReviews;

        public MockOperatorReviewDataStore()
        {
            _operatorReviews = new List<OperatorReviewDto>
            {
                new OperatorReviewDto { Id = 1, OilAmount = 5.0, Comments = "Good service", Status = Status.Completed, Date = DateTime.Now, OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 2, OilAmount = 6.0, Comments = "Needs improvement", Status = Status.Pending, Date = DateTime.Now.AddDays(-1), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 3, OilAmount = 6.0, Comments = "Needs improvement", Status = Status.Pending, Date = DateTime.Now.AddDays(-2), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 4, OilAmount = 6.0, Comments = "Needs improvement", Status = Status.Pending, Date = DateTime.Now.AddDays(-3), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 5, OilAmount = 6.0, Comments = "Needs improvement", Status = Status.Pending, Date = DateTime.Now.AddDays(-4), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 6, OilAmount = 6.0, Comments = "Needs improvement", Status = Status.Pending, Date = DateTime.Now.AddDays(-5), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 7, OilAmount = 6.0, Comments = "Needs improvement", Status = Status.Pending, Date = DateTime.Now.AddDays(-6), OperatorId = 2, DriverId = 1 },
            };
        }

        public async Task<List<OperatorReviewDto>> GetOperatorReviews()
        {
            await Task.Delay(100);
            return _operatorReviews.ToList();
        }

        public async Task<OperatorReviewDto> GetOperatorReview(int id)
        {
            await Task.Delay(100); 
            return _operatorReviews.FirstOrDefault(or => or.Id == id);
        }

        public async Task<OperatorReviewDto> CreateOperatorReview(OperatorReviewDto operatorReview)
        {
            await Task.Delay(100);
            operatorReview.Id = _operatorReviews.Max(or => or.Id) + 1;
            _operatorReviews.Add(operatorReview);
            return operatorReview;
        }

        public async Task<OperatorReviewDto> UpdateOperatorReview(int id, OperatorReviewDto operatorReview)
        {
            await Task.Delay(100); 
            var existingOperatorReview = _operatorReviews.FirstOrDefault(or => or.Id == id);
            if (existingOperatorReview != null)
            {
                existingOperatorReview.OilAmount = operatorReview.OilAmount;
                existingOperatorReview.Comments = operatorReview.Comments;
                existingOperatorReview.Status = operatorReview.Status;
                existingOperatorReview.Date = operatorReview.Date;
                existingOperatorReview.OperatorId = operatorReview.OperatorId;
                existingOperatorReview.DriverId = operatorReview.DriverId;
            }
            return existingOperatorReview;
        }

        public async Task DeleteOperatorReview(int id)
        {
            await Task.Delay(100); 
            var operatorReviewToRemove = _operatorReviews.FirstOrDefault(or => or.Id == id);
            if (operatorReviewToRemove != null)
            {
                _operatorReviews.Remove(operatorReviewToRemove);
            }
        }
    }
}
