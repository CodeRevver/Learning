
using AutoMapper;

using ConsoleDump;

using NodaTime;
using NodaTime.Extensions;

using NodaTimeAutoMapping.DTOs;
using NodaTimeAutoMapping.Entities;

namespace NodaTimeAutoMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var actionPlanEntity = new ActionPlan();
            var baseEntity = new BaseEntity();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ActionPlan, ActionPlanDto>()
                    .IncludeBase<BaseEntity, BaseEntityDto>()
                    .ForMember(dest => dest.DateStartedAt, opts => opts
                        .MapFrom(x => (x.DateStartedAt + x.TimeStartedAt).ToLocalDateTime()
                        .InZoneStrictly(DateTimeZoneProviders.Tzdb.GetZoneOrNull(x.TimeZone)).Date))
                    .ForMember(dest => dest.TimeStartedAt, opts => opts
                        .MapFrom(x => (x.DateStartedAt + x.TimeStartedAt).ToLocalDateTime()
                        .InZoneStrictly(DateTimeZoneProviders.Tzdb.GetZoneOrNull(x.TimeZone)).LocalDateTime.TimeOfDay))
                    .ForMember(dest => dest.StartedAtUtc, opts => opts
                        .MapFrom(x => x.StartedAt.ToInstant().InZone(DateTimeZoneProviders.Tzdb.GetZoneOrNull(x.TimeZone)).ToInstant()))
                    .ForMember(dest => dest.StartedAt, opts => opts
                        .MapFrom(x => x.StartedAt.ToInstant().InZone(DateTimeZoneProviders.Tzdb.GetZoneOrNull(x.TimeZone))));
                cfg.CreateMap<BaseEntity, BaseEntityDto>()
                    .ForMember(dest => dest.CreatedAt, opts => opts
                        .MapFrom(x => x.CreatedAt.ToInstant().InUtc()));
            });

            var baseDto = Mapper.Map<BaseEntity, BaseEntityDto>(baseEntity);
            baseDto.Dump();

            var actionPlanDto = Mapper.Map<ActionPlan, ActionPlanDto>(actionPlanEntity);
            actionPlanDto.Dump();
        }
    }
}
