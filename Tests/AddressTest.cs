using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryLib;

namespace Tests
{
    [TestClass]
    public class AddressTest
    {
        string testString;
        Address address;
        [TestInitialize]
        public void Inizialize()
        {
            #region String
            testString = @"Административный округ: Южный административный округ
                            Район: район Бирюлёво Восточное
                            Почтовый индекс: 115598
                            Адрес: Липецкая улица, дом 54 / 21, строение 2
                               Доступность объекта(инвалиды-колясочники): частично
                              Доступность объекта(инвалиды - опорники): частично
                               Доступность объекта(инвалиды по зрению): частично";
            #endregion

            
        }

        [TestMethod]
        public void CtorTest()
        {
            address = new Address(testString);
            Assert.AreEqual(address.Area, "район Бирюлёво Восточное");
            Assert.AreEqual(address.Building, "54 / 21");
            Assert.AreEqual(address.Housing, "строение 2");
            Assert.AreEqual(address.Street, "Липецкая улица");
            Assert.AreEqual(address.PostIndex, "115598");
            Assert.AreEqual(address.District, "Южный административный округ");
        }

        [TestMethod]
        public void CtorPartTest()
        {
            address = new Address("Южный административный округ",
                "район Бирюлёво Восточное", "Липецкая улица", "115598", "54 / 21", "строение 2");
            Assert.AreEqual(address.Area, "район Бирюлёво Восточное");
            Assert.AreEqual(address.Building, "54 / 21");
            Assert.AreEqual(address.Housing, "строение 2");
            Assert.AreEqual(address.Street, "Липецкая улица");
            Assert.AreEqual(address.PostIndex, "115598");
            Assert.AreEqual(address.District, "Южный административный округ");
        }

        [TestMethod]
        public void ToStringTest()
        {
            #region String
            string actual = @"Административный округ: Южный административный округ
Район: район Бирюлёво Восточное
Почтовый индекс: 115598
Адрес: Липецкая улица, дом 54 / 21, строение 2";
            #endregion
            address = new Address(testString);
            Assert.AreEqual(address.ToString(), actual);
        }
    }
}
