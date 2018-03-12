using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    public enum Day { Monday, Tuesday, Wednesday, Thusday, Friday, Saturday, Sunday };

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

                        var hourStart = int.Parse(Regex.Split(hoursLineSplt[0], @":").First());

                        var hourEnd = int.Parse(Regex.Split(hoursLineSplt[0], @":").First());                       
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
        public WorkingHours(int hourOpen, int hourClose)
        {
            foreach (Day item in dayToDay.Values)
            {
                hours.Add(item, new TimePeriod(hourOpen, hourClose, false));
            }
        }

        public override string ToString()
        {
            string str = string.Empty;
            foreach (string day in dayToDay.Keys)
            {
                if (day != "воскресенье")
                {
                    str += $"День недели: {day}\n";
                    str += $"Часы работы: {hours[dayToDay[day]].ToString()}\n\n";
                }
                else
                {
                    str += $"День недели: {day}\n";
                    str += $"Часы работы: {hours[dayToDay[day]].ToString()}";
                }
            }
            return str;
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
