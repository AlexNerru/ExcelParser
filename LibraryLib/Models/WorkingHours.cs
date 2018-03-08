using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    public struct TimePeriod
    {
        int hourStart;
        int hourEnd;
        bool holiday;
        public TimePeriod(int hourStart, int hourEnd, bool holiday=false)
        {
            this.hourStart = hourStart;
            this.hourEnd = hourEnd;
            this.holiday = holiday;
        }
        public override string ToString()
        {
            return $"{hourStart} {hourEnd}";
        }
    }

    public enum Day { Monday, Tuesday, Wednesday, Thusday, Friday, Saturday, Sunday, };

    public class WorkingHours
    {
        public Dictionary<Day, TimePeriod> hours  = new Dictionary<Day,TimePeriod>();
        Dictionary<string, Day> dayToDay = new Dictionary<string, Day>()
        {
            {"понедельник", Day.Monday },
            {"вторник", Day.Tuesday },
            {"среда", Day.Wednesday },
            {"четверг", Day.Thusday },
            {"пятница", Day.Friday },
            {"суббота", Day.Saturday },
            {"воскресенье", Day.Sunday },
        };
        public WorkingHours(string workingHours)
        {
            List<string> stringList = Regex.Split(workingHours, "\n\n").ToList();
            foreach (var day in stringList)
            {
                List<string> anotherList = Regex.Split(day, "\n").ToList();
                var dayLine =  Regex.Split(anotherList[0], @": ").Last();
                var hoursLine= Regex.Split(anotherList[1], @": ").Last();
                var hoursLineSplt = Regex.Split(hoursLine, @"-");
                try
                {
                    if (!hoursLine.Contains("выходной") && !hoursLine.Contains("закрыто"))
                    {
                        var hourStart = int.Parse(hoursLine.Substring(0, 2));
                        var hourEnd = int.Parse(hoursLine.Substring(6, 2));                       
                        hours.Add(dayToDay[dayLine], new TimePeriod(hourStart, hourEnd, false));
                    }
                    else
                        hours.Add(dayToDay[dayLine], new TimePeriod(0, 0, true));
                }
                catch (FormatException e)
                {
                    throw new WorkingHoursParseException("Smth with data parse", e);
                }

            }
        }
    }

    [Serializable]
    public class WorkingHoursParseException : Exception
    {
        public WorkingHoursParseException() { }
        public WorkingHoursParseException(string message) : base(message) { }
        public WorkingHoursParseException(string message, Exception inner) : base(message, inner) { }
        protected WorkingHoursParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

   
}
