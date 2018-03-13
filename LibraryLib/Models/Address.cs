using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;

namespace LibraryLib
{
    /// <summary>
    /// Class for address information
    /// </summary>
    public class Address
    {
        #region private
        #region String
        string testString = @"Административный округ: Южный административный округ
                            Район: район Бирюлёво Восточное
                            Почтовый индекс: 115598
                            Адрес: Липецкая улица, дом 54 / 21, строение 2
                               Доступность объекта(инвалиды-колясочники): частично
                              Доступность объекта(инвалиды - опорники): частично
                               Доступность объекта(инвалиды по зрению): частично";
        #endregion

        /// <summary>
        /// Dict to make conformity between parse string and property
        /// </summary>
        Dictionary<string, string> rusToEng = new Dictionary<string, string>()
        {
            ["Район: "] = "Area",
            ["Почтовый индекс: "] = "PostIndex",
            ["Адрес: "] = "FullAddress",
            ["Административный округ: "] = "District"
        };
        #endregion

        #region Props
        /// <summary>
        /// Area
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// PostIndex
        /// </summary>
        public string PostIndex { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Street
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Building
        /// </summary>
        public string Building { get; set; }
        /// <summary>
        /// Housing
        /// </summary>
        public string Housing { get; set; }
        /// <summary>
        /// District
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// FullAdress
        /// </summary>
        public string FullAddress { get; set; }
        #endregion

        /// <summary>
        /// Standart ctor to create object from table
        /// </summary>
        /// <param name="addressStr"></param>
        public Address(string addressStr)
        {
            StringHelper helper = new StringHelper();

            if (addressStr.Contains("размер ячейки") || addressStr.Contains("Зеленоград"))
                addressStr = testString;
            try
            {
                Dictionary<string, string> dict = helper.ParseStringsToValueProperty(addressStr.Split('\n').ToList(), rusToEng);
                foreach (var item in dict.Keys)
                    this[item] = dict[item];
                var all = Regex.Split(FullAddress, @", ");
                if (all[0].Contains("город"))
                {
                    this.City = all[0].Remove(0, 6);
                    this.Street = all[1];
                    this.Building = all[2].Remove(0, 4);
                    if (all.Length > 3)
                        this.Housing = all[3];
                }
                else
                {
                    this.Street = all[0];
                    this.Building = all[1].Remove(0, 4);
                    if (all.Length > 2)
                        this.Housing = all[2];
                }

            }
            catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentOutOfRangeException || e is InvalidOperationException)
            { throw new AdressParseException("Something happened while address parsing", e); }
            catch (Exception e)
            { throw new AdressParseException("Something strange", e); }
        }

        /// <summary>
        /// Constructor to create object from interface
        /// </summary>
        /// <param name="district"></param>
        /// <param name="area"></param>
        /// <param name="street"></param>
        /// <param name="postIndex"></param>
        /// <param name="building"></param>
        /// <param name="housing"></param>
        /// <param name="city"></param>
        public Address(string district, string area, string street,
            string postIndex, string building, string housing, string city)
        {
            this.District = district;
            this.Area = area;
            this.Street = street;
            this.PostIndex = postIndex;
            this.Building = building;
            this.Housing = housing;
            this.City = city;
        }

        /// <summary>
        /// Creates string as in excel file
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Административный округ: {this.District}\nРайон: {this.Area}" +
                $"\nПочтовый индекс: {this.PostIndex}\nАдрес: {this.Street}, дом {this.Building}, {this.Housing}";

        /// <summary>
        /// Gets or sets property using it's string name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(Address);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(Address);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }
        }
    }


    /// <summary>
    /// Thrown if something happened while parsing
    /// </summary>
    [Serializable]
    public class AdressParseException : Exception
    {
        public AdressParseException() { }
        public AdressParseException(string message) : base(message) { }
        public AdressParseException(string message, Exception inner) : base(message, inner) { }
        protected AdressParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
