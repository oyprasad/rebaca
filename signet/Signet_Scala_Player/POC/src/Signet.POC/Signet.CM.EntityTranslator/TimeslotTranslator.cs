namespace Signet.CM.EntityTranslator
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Channel;
    using Signet.Core.DomainObjects;
    using Signet.Core.Extensions;

    public class TimeslotTranslator : EntityMapperTranslator<ITimeslotEntity, timeslotTO>
    {
        protected override timeslotTO BusinessToService(ITimeslotEntity value)
        {
            throw new NotImplementedException();
        }

        private List<DayOfWeek> getWeekdays(string days)
        {
            List<DayOfWeek> weekdays = new List<DayOfWeek>();
            days = days.Trim(new char[] { ',' });
            string[] listOfDays = days.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string day in listOfDays)
            {
                weekdays.Add((DayOfWeek)int.Parse(day));
            }
            return weekdays;
        }

        protected override ITimeslotEntity ServiceToBusiness(timeslotTO value)
        {
            return new TimeslotEntity 
            { 
                Id = value.id, 
                Name = value.name, 
                PlaylistId = new int?(value.playlistId), 
                Color = value.color.IsValidColorCode() ? new Color?(ColorTranslator.FromHtml(value.color)) : null, 
                IsLocked = value.@lock, 
                IsCharted = value.isCharted, 
                PlayFullScreen = value.playFullscreen, 
                DuckAudio = value.duckAudio, 
                SortOrder = value.sortOrder, 
                Description = value.description, 
                StartDateTime = value.startDate, 
                EndDateTime = value.endDate, 
                RecurrencePattern = value.recurrencePeriod.ToEnum<RecurrencePattern>(RecurrencePattern.Unknown), 
                Weekdays = this.getWeekdays(value.recurrenceDays), 
                MonthPeriod = (MonthPeriod)value.recurrenceInterval, 
                Priority = (TimeslotPriorityEnum)value.priority 
            };
        }
    }
}