// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    internal static class DateTimeUtilities
    {
        public static DateTime GetContextNow(DateTimeKind kind)
        {
            if (kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            return (kind == DateTimeKind.Utc)
              ? DateTime.UtcNow
              : DateTime.Now;
        }

        public static bool IsSameDate(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return false;

            return date1.Value.Date == date2.Value.Date;
        }
    }
}
