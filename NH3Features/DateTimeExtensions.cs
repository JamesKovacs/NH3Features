using System;

namespace Nh3Hacking {
    public static class DateTimeExtensions {
        public static string Dump(this DateTime dateTime) {
            return String.Format("{0} ({1})", dateTime, dateTime.Kind);
        }
    }
}