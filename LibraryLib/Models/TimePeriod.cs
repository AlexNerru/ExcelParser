using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    public struct TimePeriod
    {
        int hourStart;
        int hourEnd;
        bool holiday;

        public int OpenHour { get => hourStart; }
        public int CloseHour { get => hourEnd; }

        public TimePeriod(int hourStart, int hourEnd, bool holiday = false)
        {
            this.hourStart = hourStart;
            this.hourEnd = hourEnd;
            this.holiday = holiday;
        }
        
        public override string ToString()
        {
            if (!holiday)
                return $"{hourStart}:00-{hourEnd}:00";
            else
                return "выходной";
        }
    }
}
