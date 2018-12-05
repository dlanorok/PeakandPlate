using System;

namespace PeakandPlate.Model
{
    public class RushHour
    {
        public PartsOfTheDay TimeOfTheDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
