using System;

namespace NodaTimeAutoMapping.Entities
{
    public class ActionPlan : BaseEntity
    {
        public ActionPlan()
        {
            StartedAt = DateTime.UtcNow;
            DateStartedAt = DateTime.UtcNow.Date;
            TimeStartedAt = DateTime.UtcNow.TimeOfDay;
            TimeZone = "America/New_York";
        }

        public DateTime StartedAt { get; set; }
        public DateTime DateStartedAt { get; set; }
        public TimeSpan TimeStartedAt { get; set; }
        public string TimeZone { get; set; }
    }
}
