using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    public class OrgInfo
    {
        public string TaxPayerId { get; set; }
        public string FullName { get; set; }
        public string HeadPhoneNumber { get; set; }
        public Person HeadFullName { get; set; }
        public string TaxId { get; set; }
        public string GovermentId { get; set; }

        public OrgInfo(string FullOrgInfo)
        {
            List<string> needToFind = new List<string>() { "ИНН",
                "Полное наименование учреждения", "Телефон руководителя",
                "ФИО руководителя учреждения", "КПП", "ОГРН" };
            List<string> stringList = Regex.Split(FullOrgInfo,"\r\n").ToList();
            List<string> toParse = new List<string>();
            foreach (var str in needToFind)
                toParse.Add(stringList.Where(stringToCheck => stringToCheck.Contains(str)).First());
            try
            {
                this.TaxPayerId = Regex.Split(toParse[0], @": ").Last();
                this.FullName = Regex.Split(toParse[1], @": ").Last();
                this.HeadPhoneNumber= Regex.Split(toParse[2], @": ").Last();
                this.HeadFullName = new Person(Regex.Split(toParse[3], @": ").Last());
                this.TaxId = Regex.Split(toParse[4], @": ").Last();
                this.GovermentId = Regex.Split(toParse[5], @": ").Last();
            }
            catch (IndexOutOfRangeException e)
            {
                throw new OrgInfoParseException("Data Parse error, not all fields provided", e);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new OrgInfoParseException("Data Parse error, not all fields provided\n", e);
            }
            catch (ArgumentException e)
            {
                throw new OrgInfoParseException("Data Parse error", e);
            }
        }

        public override string ToString()
        {
            return $"Телефон руководителя: {HeadPhoneNumber}\n" +
                $"Полное наименование учреждения: {FullName}\nИНН: {TaxPayerId}\nКПП: {TaxId}\n" +
                $"ОГРН: {GovermentId}\nФИО руководителя учреждения: {HeadFullName.ToString()}";
        }
    }

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
