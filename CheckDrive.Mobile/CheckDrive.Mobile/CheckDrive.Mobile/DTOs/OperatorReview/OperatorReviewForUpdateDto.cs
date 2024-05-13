using CheckDrive.Domain.DTOs.MechanicAcceptance;
using System;

namespace CheckDrive.Domain.DTOs.OperatorReview
{
    public class OperatorReviewForUpdateDto
    {
        public double OilAmount { get; set; }
        public string Comments { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; }

        public int OperatorId { get; set; }
        public int DriverId { get; set; }
    }
}
