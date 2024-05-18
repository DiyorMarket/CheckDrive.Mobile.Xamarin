using System;
using System.Collections.Generic;
using System.Text;

namespace CheckDrive.Mobile.Helpers
{
    public class History
    {
        public DateTime Date { get; set; }
        public bool IsHealthy { get; set; }
        public bool IsAccepted { get; set; }
        private bool IsHanded { get; set; }
    }
}
