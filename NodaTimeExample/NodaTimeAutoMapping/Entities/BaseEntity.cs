using System;

namespace NodaTimeAutoMapping.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow.AddHours(-30);
        }

        public DateTime CreatedAt { get; set; }
    }
}
