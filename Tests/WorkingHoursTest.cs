using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryLib;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class WorkingHoursTest
    {
        string testStr;
        WorkingHours hours;

        [TestInitialize]
        public void Init()
        {
            #region str

            
            testStr = @"День недели: понедельник
Часы работы: выходной

День недели: вторник
Часы работы: 12:00-21:00

День недели: среда
Часы работы: 12:00-21:00

День недели: четверг
Часы работы: 12:00-21:00

День недели: пятница
Часы работы: 12:00-21:00

День недели: суббота
Часы работы: 12:00-21:00

День недели: воскресенье
Часы работы: 12:00-20:00";
            #endregion
        }
        [TestMethod]
        public void CtorTest()
        {
            hours = new WorkingHours(testStr);
            Dictionary<Day, TimePeriod> newHours = new Dictionary<Day, TimePeriod>();

            newHours.Add(Day.Monday, new TimePeriod(0, 0, true));
            newHours.Add(Day.Tuesday, new TimePeriod(12, 21, true));
            newHours.Add(Day.Wednesday, new TimePeriod(12, 21, true));
            newHours.Add(Day.Thusday, new TimePeriod(12, 21, true));
            newHours.Add(Day.Friday, new TimePeriod(12, 21, true));
            newHours.Add(Day.Saturday, new TimePeriod(12, 21, true));
            newHours.Add(Day.Sunday, new TimePeriod(12, 20, true));
            Assert.AreEqual(hours.hours.Keys.Count, newHours.Keys.Count);
        }
        
    }
    
}
