using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryLib;

namespace Tests
{
    [TestClass]
    public class GeoDataTest
    {
        string testString;
        GeoData info;
        [TestInitialize]
        public void TestInit()
        {
            #region string
            testString = @"{type=MultiPoint, coordinates=[[37.690183014419, 55.774272167955], [37.690380487432, 55.774195659242], [37.651137395594, 55.754799076964], [37.676112805065, 55.76802782714], [37.708986149815, 55.773385069344]]}";
            #endregion

        }
        [TestMethod]
        public void CtorTest()
        {
            info = new GeoData(testString);

        }
    }
}
