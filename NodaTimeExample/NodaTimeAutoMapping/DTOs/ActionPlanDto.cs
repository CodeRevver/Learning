
using NodaTime;

namespace NodaTimeAutoMapping.DTOs
{
    class ActionPlanDto : BaseEntityDto
    {
        public Instant StartedAtUtc { get; set; }
        public ZonedDateTime StartedAt { get; set; }
        public LocalDate DateStartedAt { get; set; }
        public LocalTime TimeStartedAt { get; set; }
    }
}
