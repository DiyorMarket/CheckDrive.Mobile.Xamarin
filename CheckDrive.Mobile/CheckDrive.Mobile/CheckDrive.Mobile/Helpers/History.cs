using System;

namespace CheckDrive.Mobile.Helpers
{
    public class History
    {
        public DateTime Date { get; set; }
        public bool IsHealthy { get; set; }
        public bool IsHanded { get; set; }
        public bool IsGiven { get; set; }
        public bool IsAccepted { get; set; }

        public bool IsAllTrue => IsHealthy && IsHanded && IsGiven && IsAccepted;
    }

}




















































