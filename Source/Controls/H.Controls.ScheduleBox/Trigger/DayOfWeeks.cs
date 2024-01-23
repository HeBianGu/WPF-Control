using System;
using System.Collections.Generic;

namespace H.Controls.ScheduleBox
{
    public class DayOfWeeks : List<DayOfWeek>
    {
        public List<DayOfWeek> Source { get; set; } = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
    }
}
