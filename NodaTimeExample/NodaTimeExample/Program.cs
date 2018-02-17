using System;
using System.Linq;
using NodaTime;
using NodaTime.Extensions;

namespace NodaTimeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var ianaAllZones = DateTimeZoneProviders.Tzdb.GetAllZones().ToList();
            var dateCreatedUtc = DateTime.UtcNow;
            var instantDateCreatdUtc = dateCreatedUtc.ToInstant();
            var zoneDateCreated = instantDateCreatdUtc.InUtc();
            var zoneDateCreatedInGmt = instantDateCreatdUtc.InZone(ianaAllZones.Single(x => x.Id == "Europe/London"));

            Console.WriteLine("dateCreatedUtc: " + dateCreatedUtc);
            Console.WriteLine("instantDateCreatdUtc: " + instantDateCreatdUtc);
            Console.WriteLine("zoneDateCreated: " + zoneDateCreated);
            Console.WriteLine("zoneDateCreatedInGmt: " + zoneDateCreatedInGmt);
            Console.WriteLine("zoneDateCreatedInGmtToUtc: " + zoneDateCreatedInGmt.ToDateTimeUtc());

            Console.WriteLine();

            var databaseTimeZone = ianaAllZones.Single(x => x.Id == "Europe/London");
            var databaseDateTime = new DateTime(2017, 1, 1, 1, 0, 0);
            var localDateTime = databaseDateTime.ToLocalDateTime();
            var zoneDateTimeInEuropeLondon = localDateTime.InZoneStrictly(databaseTimeZone);
            var zoneDateTimeConvertedToUtc = zoneDateTimeInEuropeLondon.ToDateTimeUtc();
            var asiaHkTimeZone = ianaAllZones.Single(x => x.Id == "Asia/Hong_Kong");
            var zoneDateTimeConvertedToAsiaHk = zoneDateTimeInEuropeLondon.ToInstant().InZone(asiaHkTimeZone);

            Console.WriteLine("databaseTimeZone: " + databaseTimeZone);
            Console.WriteLine("databaseDateTime: " + databaseDateTime);
            Console.WriteLine("localDateTime: " + localDateTime);
            Console.WriteLine("zoneDateTimeInEuropeLondon: " + zoneDateTimeInEuropeLondon);
            Console.WriteLine("zoneDateTimeConvertedToUtc: " + zoneDateTimeConvertedToUtc);
            Console.WriteLine("zoneDateTimeConvertedToAsiaHk: " + zoneDateTimeConvertedToAsiaHk);

            Console.WriteLine();

            var databaseDateTimeInUtc = new DateTime(2017, 1, 1, 1, 0, 0);
            var dateTimeInUtcInstant = databaseDateTimeInUtc.ToLocalDateTime().InUtc().ToInstant();

            Console.WriteLine("databaseDateTimeInUtc: " + databaseDateTimeInUtc);
            Console.WriteLine("dateTimeInUtcInstant: " + dateTimeInUtcInstant);
            Console.WriteLine();

            var dateStarted = new LocalDate(2017, 8, 22);
            var taskStarted = new DateTime(2017, 8, 4, 12, 0, 0).ToLocalDateTime().InZoneStrictly(ianaAllZones.Single(x => x.Id == "Europe/London"));
            var taskStartedInUtc = taskStarted.ToInstant();

            LocalDate? dateCompleted = null;
            Console.WriteLine("Lose weight");
            Console.WriteLine("Date Started: " + dateStarted);
            Console.WriteLine("Date Completed: {0}", dateCompleted);
            Console.WriteLine("Task 1 - Bring calories down to 1800 daily");
            Console.WriteLine("Task started GMT: " + taskStarted);
            Console.WriteLine("Task started UTC: " + taskStartedInUtc);

            Console.WriteLine();
            var duration20Hours = Duration.FromHours(20);
            var interval = new Interval(taskStarted.ToInstant(), taskStarted.PlusHours(20).ToInstant());

            Console.WriteLine("Task will take 20 hours");
            Console.WriteLine("duration20Hours: " + duration20Hours);
            Console.WriteLine("interval: " + interval);
            Console.WriteLine("Completed at: " + interval.End);

            var period1Day = Period.FromDays(1);
            Console.WriteLine("Period for task is 1 day");
            Console.WriteLine("Period: " + period1Day);
            Console.WriteLine("started local datetime at (in Europe/London): " + taskStarted.LocalDateTime);
            Console.WriteLine("Completed at (in Europe/London): " + taskStarted.LocalDateTime.Plus(period1Day));
            var addedPeriod = taskStarted.LocalDateTime.Plus(period1Day);
            Console.WriteLine("Completed at UTC: " + addedPeriod.InZoneStrictly(ianaAllZones.Single(x => x.Id == "Europe/London")).ToDateTimeUtc());

            Console.ReadLine();
        }
    }
}
