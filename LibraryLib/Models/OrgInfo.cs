using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    /// <summary>
    /// Class represents org info
    /// </summary>
    public class OrgInfo
    {
        /// <summary>
        /// TaxPayerId
        /// </summary>
        public string TaxPayerId { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Chief phone number
        /// </summary>
        public string HeadPhoneNumber { get; set; }

        /// <summary>
        /// Chief full name
        /// </summary>
        public Person HeadFullName { get; set; }

        /// <summary>
        /// Tax Id
        /// </summary>
        public string TaxId { get; set; }

        /// <summary>
        /// Govermant Id
        /// </summary>
        public string GovermentId { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="taxPayerId"></param>
        /// <param name="fullName"></param>
        /// <param name="headPhoneNumber"></param>
        /// <param name="headFullName"></param>
        /// <param name="taxId"></param>
        /// <param name="govermentId"></param>
        public OrgInfo(string taxPayerId, string fullName, string headPhoneNumber, Person headFullName, string taxId, string govermentId)
        {
            FullName = fullName;
            HeadPhoneNumber = headPhoneNumber;
            HeadFullName = headFullName;
            TaxId = taxId;
            GovermentId = govermentId;
        }

        /// <summary>
        /// Ctor for parsing
        /// </summary>
        /// <param name="FullOrgInfo"></param>
        public OrgInfo(string FullOrgInfo)
        {
            List<string> needToFind = new List<string>() { "ИНН: ",
                "Полное наименование учреждения: ", "Телефон руководителя: ",
                "ФИО руководителя учреждения: ", "КПП: ", "ОГРН: " };
            List<string> stringList = Regex.Split(FullOrgInfo, "\n").ToList();
            try
            {
                this.TaxPayerId
                    = Regex.Split(stringList.Where(str => str.Contains(needToFind[0])).First(), @": ").Last();
                this.FullName = Regex.Split(stringList.Where(str => str.Contains(needToFind[1])).First(), @": ").Last();
                this.HeadPhoneNumber = Regex.Split(stringList.Where(str => str.Contains(needToFind[2])).First(), @": ").Last();
                this.HeadFullName = new Person(Regex.Replace(Regex.Split(stringList.Where(str => str.Contains(needToFind[3])).First(), @": ").Last(), @"\t|\n|\r", ""));
                this.TaxId = Regex.Split(stringList.Where(str => str.Contains(needToFind[4])).First(), @": ").Last();
                this.GovermentId = Regex.Split(stringList.Where(str => str.Contains(needToFind[5])).First(), @": ").Last();
            }
            catch (Exception e) when (e is PersonParseException || e is IndexOutOfRangeException || e is ArgumentOutOfRangeException || e is ArgumentException)
            {
                throw new OrgInfoParseException("Data Parse error, not all fields provided", e);
            }
            catch (Exception e) { throw new OrgInfoParseException(e.Message); }
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Телефон руководителя: {HeadPhoneNumber}\n" +
                $"Полное наименование учреждения: {FullName}\nИНН: {TaxPayerId}\nКПП: {TaxId}\n" +
                $"ОГРН: {GovermentId}\nФИО руководителя учреждения: {HeadFullName.ToString()}";
        }
    }

    /// <summary>
    /// Thrown if smth with parsing
    /// </summary>
    [Serializable]
    public class OrgInfoParseException : Exception
    {
        public OrgInfoParseException() { }
        public OrgInfoParseException(string message) : base(message) { }
        public OrgInfoParseException(string message, Exception inner) : base(message, inner) { }
        protected OrgInfoParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
