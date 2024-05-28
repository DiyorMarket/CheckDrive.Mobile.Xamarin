using CheckDrive.ApiContracts.OperatorReview;
using CheckDrive.ApiContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.OperatorReviews
{
    public class MockOperatorReviewDataStore
    {
        private readonly List<OperatorReviewDto> _operatorReviews;

        public MockOperatorReviewDataStore()
        {
            _operatorReviews = new List<OperatorReviewDto>
            {
                new OperatorReviewDto { Id = 1, IsGiven = true, OilAmount = 25.0, Comments = "Good service", Status = StatusForDto.Completed, Date = DateTime.Now.AddDays(-35), OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 2, IsGiven = false,  OilAmount = 15.0, Comments = "Good service", Status = StatusForDto.Completed, Date = DateTime.Now.AddDays(-6), OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 3, IsGiven = true,  OilAmount = 5.0, Comments = "Good service", Status = StatusForDto.Completed, Date = DateTime.Now.AddDays(-40), OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 4, IsGiven = false,  OilAmount = 30.0, Comments = "Good service", Status = StatusForDto.Completed, Date = DateTime.Now.AddDays(-8), OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 5, IsGiven = true,  OilAmount = 10.0, Comments = "Good service", Status = StatusForDto.Completed, Date = DateTime.Now.AddDays(-1), OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 6, IsGiven = false,  OilAmount = 15.0, Comments = "Good service", Status = StatusForDto.Completed, Date = DateTime.Now.AddDays(-2), OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 7, IsGiven = true,  OilAmount = 10.0, Comments = "Good service", Status = StatusForDto.Completed, Date = DateTime.Now.AddDays(-8), OperatorId = 1, DriverId = 1 },
                new OperatorReviewDto { Id = 8, IsGiven = false,  OilAmount = 6.0, Comments = "Needs improvement", Status = StatusForDto.Pending, Date = DateTime.Now.AddDays(-1), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 9, IsGiven = true,  OilAmount = 6.0, Comments = "Needs improvement", Status = StatusForDto.Pending, Date = DateTime.Now.AddDays(-2), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 10, IsGiven = true,  OilAmount = 6.0, Comments = "Needs improvement", Status = StatusForDto.Pending, Date = DateTime.Now.AddDays(-3), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 11, IsGiven = true,  OilAmount = 6.0, Comments = "Needs improvement", Status = StatusForDto.Pending, Date = DateTime.Now.AddDays(-4), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 12, IsGiven = false,  OilAmount = 6.0, Comments = "Needs improvement", Status = StatusForDto.Pending, Date = DateTime.Now.AddDays(-5), OperatorId = 2, DriverId = 1 },
                new OperatorReviewDto { Id = 13, IsGiven = true,  OilAmount = 6.0, Comments = "Needs improvement", Status = StatusForDto.Pending, Date = DateTime.Now.AddDays(-6), OperatorId = 2, DriverId = 1 },
            };
        }

        public List<OperatorReviewDto> GetOperatorReviews()
        {
            return _operatorReviews.ToList();
        }

        public OperatorReviewDto GetOperatorReview(int id)
        {
            return _operatorReviews.FirstOrDefault(or => or.Id == id);
        }

        public OperatorReviewDto CreateOperatorReview(OperatorReviewDto operatorReview)
        {
            operatorReview.Id = _operatorReviews.Max(or => or.Id) + 1;
            _operatorReviews.Add(operatorReview);
            return operatorReview;
        }

        public OperatorReviewDto UpdateOperatorReview(int id, OperatorReviewDto operatorReview)
        { 
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

        public void DeleteOperatorReview(int id)
        {
            var operatorReviewToRemove = _operatorReviews.FirstOrDefault(or => or.Id == id);
            if (operatorReviewToRemove != null)
            {
                _operatorReviews.Remove(operatorReviewToRemove);
            }
        }
    }
}
