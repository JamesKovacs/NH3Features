using System;

namespace Nh3Hacking {
    public class DateTimeEntity {
        public DateTimeEntity() {
            var localTime = DateTime.Now;
            var utcTime = DateTime.UtcNow;
            CreationTimeAsDateTime = localTime;
            CreationTimeAsDateTime2 = localTime;
            CreationTimeAsLocalDateTime = localTime;
            CreationTimeAsUtcDateTime = utcTime;
            CreationTimeAsDateTimeOffset = localTime;
            CreationTimeAsDate = localTime;
            CreationTimeAsTime = localTime;
            CreationTimeAsTimeAsTimeSpan = localTime.TimeOfDay;
            CreationTimeAsTimeSpan = localTime.TimeOfDay;
        }

        public virtual Guid Id { get; private set; }
        public virtual DateTime CreationTimeAsDateTime { get; set; }
        public virtual DateTime CreationTimeAsLocalDateTime { get; set; }
        public virtual DateTime CreationTimeAsUtcDateTime { get; set; }
        public virtual DateTimeOffset CreationTimeAsDateTimeOffset { get; set; }
        public virtual DateTime CreationTimeAsDateTime2 { get; set; }
        public virtual DateTime CreationTimeAsDate { get; set; }
        public virtual DateTime CreationTimeAsTime { get; set; }
        public virtual TimeSpan CreationTimeAsTimeAsTimeSpan { get; set; }
        public virtual TimeSpan CreationTimeAsTimeSpan { get; set; }
        
        public override string ToString() {
            return string.Format("Id: {0}\r\n\tCreationTimeAsDateTime: {1}\r\n\tCreationTimeAsLocalDateTime: {2}\r\n\tCreationTimeAsUtcDateTime: {3}\r\n\tCreationTimeAsDateTimeOffset: {4}\r\n\tCreationTimeAsDateTime2: {5}\r\n\tCreationTimeAsDate: {6}\r\n\tCreationTimeAsTime: {7}\r\n\tCreationTimeAsTimeAsTimeSpan: {8}\r\n\tCreationTimeAsTimeSpan: {9}", Id, CreationTimeAsDateTime.Dump(), CreationTimeAsLocalDateTime.Dump(), CreationTimeAsUtcDateTime.Dump(), CreationTimeAsDateTimeOffset, CreationTimeAsDateTime2.Dump(), CreationTimeAsDate.Dump(), CreationTimeAsTime.Dump(), CreationTimeAsTimeAsTimeSpan, CreationTimeAsTimeSpan);
        }
    }
}