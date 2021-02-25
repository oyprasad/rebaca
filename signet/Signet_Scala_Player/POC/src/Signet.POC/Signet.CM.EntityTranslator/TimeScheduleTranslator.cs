namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Playlist;
    using Signet.Core.DomainObjects;
    using Signet.Core.Extensions;

    public class TimeScheduleTranslator : EntityMapperTranslator<ITimeScheduleEntity, timeScheduleTO>
    {
        protected override timeScheduleTO BusinessToService(ITimeScheduleEntity value)
        {
            throw new NotImplementedException();
        }

        protected override ITimeScheduleEntity ServiceToBusiness(timeScheduleTO value)
        {
            return new TimescheduleEntity 
            { 
                Id = value.id, 
                StartTime = value.startTime.ParseAsDateTime(),
                EndTime = value.endTime.ParseAsDateTime(), 
                Weekdays = value.days.ToEntityEnumArray(), 
                SortOrder = value.sortOrder 
            };
        }
    }
}