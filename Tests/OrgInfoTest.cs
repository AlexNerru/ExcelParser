using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryLib;

namespace Tests
{
    [TestClass]
    public class OrgInfoTest
    {
        string testString;
        OrgInfo info;
        [TestInitialize]
        public void TestInit()
        {
            #region string
           testString = @"  Телефон руководителя: (495) 687-75-71
Полное наименование учреждения: Государственное бюджетное учреждение культуры города Москвы «Централизованная библиотечная система Северо-Восточного административного округа»
ИНН: 7717694852
КПП: 771701001
ОГРН: 1117746181497
Юридический адрес: 129626, г. Москва, Новоалексеевская улица, дом 1
ФИО руководителя учреждения: Пилюгина Нина Александровна
Должность руководителя: генеральный директор";
            #endregion

        }
        [TestMethod]
        public void CtorTest()
        {
            info = new OrgInfo(testString);
            Assert.AreEqual(info.FullName, "Государственное бюджетное учреждение культуры города Москвы «Централизованная библиотечная система Северо-Восточного административного округа»\r");
            Assert.AreEqual(info.GovermentId, "1117746181497\r");
            Assert.AreEqual(info.HeadFullName.ToString(), "Пилюгина Нина Александровна\r");
            Assert.AreEqual(info.HeadPhoneNumber, "(495) 687-75-71\r");
            Assert.AreEqual(info.TaxId, "771701001\r");
            Assert.AreEqual(info.TaxPayerId, "7717694852\r");
        }

       
        [TestMethod]
        public void ToStringTest()
        {
            #region String
            string actual = @"Телефон руководителя: (495) 687-75-71
Полное наименование учреждения: Государственное бюджетное учреждение культуры города Москвы «Централизованная библиотечная система Северо-Восточного административного округа»
ИНН: 7717694852
КПП: 771701001
ОГРН: 111774618149
ФИО руководителя учреждения: Пилюгина Нина Александровна";
            #endregion
            info = new OrgInfo(testString);
            Assert.AreEqual(info.ToString(), actual);
        }

    }
}
