using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    /// <summary>
    /// Represent period of time
    /// </summary>
    public struct TimePeriod
    {
        /// <summary>
        /// start
        /// </summary>
        int hourStart;

        /// <summary>
        /// end
        /// </summary>
        int hourEnd;

        /// <summary>
        /// if holiday
        /// </summary>
        bool holiday;

        /// <summary>
        /// Open hour
        /// </summary>
        public int OpenHour { get => hourStart; }

        /// <summary>
        /// closehour
        /// </summary>
        public int CloseHour { get => hourEnd; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="hourStart"></param>
        /// <param name="hourEnd"></param>
        /// <param name="holiday"></param>
        public TimePeriod(int hourStart, int hourEnd, bool holiday = false)
        {
            this.hourStart = hourStart;
            this.hourEnd = hourEnd;
            this.holiday = holiday;
        }
        
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!holiday)
                return $"{hourStart}:00-{hourEnd}:00";
            else
                return "выходной";
        }
    }
}
