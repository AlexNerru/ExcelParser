using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    public class Address
    {
        public string Area { get; set; }
        public string PostIndex { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Housing { get; set; }
        public string District { get; set; }

        /// <summary>
        /// Standart ctor to create object from table
        /// </summary>
        /// <param name="FullAdress"></param>
        public Address(string FullAdress)
        {
            List<string> needToFind = new List<string>() { "Район", "Почтовый индекс", "Адрес", "Административный округ" };
            List<string> stringList = FullAdress.Split('\n').ToList();
            List<string> toParse = new List<string>();
            foreach (var str in needToFind)
                toParse.Add(stringList.Where(stringToCheck => stringToCheck.Contains(str)).First());
            try
            {
                this.Area = Regex.Split(toParse[0], @": ").Last();
                this.PostIndex = Regex.Split(toParse[1], @": ").Last();
                this.District = Regex.Split(toParse[3], @": ").Last();
                var address = toParse[2].Split(':')[1];
                var all = Regex.Split(address, @", ");
                if (all[0].Contains("город"))
                {
                    this.City = all[0].Remove(0,6);
                    this.Street = all[1].Remove(0, 1);
                    this.Building = all[2].Remove(0,4);
                    if (all.Length>3)
                        this.Housing = all[3];
                }
                else
                {
                    this.Street = all[0].Remove(0, 1); 
                    this.Building = all[1].Remove(0,4);
                    if (all.Length > 2)
                        this.Housing = all[2];
                }

            }
            catch(IndexOutOfRangeException e)
            {
                throw new AdressParseException("Data Parse error, not all fields provided", e);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new AdressParseException("Data Parse error, not all fields provided\n", e);
            }

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
            string postIndex, string building, string housing ="", string city = "")
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
        public override string ToString()
        {
            return $"Административный округ: {this.District}\nРайон: {this.Area}" +
                $"\nПочтовый индекс: {this.PostIndex}\nАдрес: {this.Street}, дом {this.Building}, {this.Housing}";
        }
    }

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
