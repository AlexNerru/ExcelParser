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

        public OrgInfo(string taxPayerId, string fullName, string headPhoneNumber, Person headFullName, string taxId, string govermentId)
        {
            FullName = fullName;
            HeadPhoneNumber = headPhoneNumber;
            HeadFullName = headFullName;
            TaxId = taxId;
            GovermentId = govermentId;
        }

        public OrgInfo(string FullOrgInfo)
        {
            List<string> needToFind = new List<string>() { "ИНН: ",
                "Полное наименование учреждения: ", "Телефон руководителя: ",
                "ФИО руководителя учреждения: ", "КПП: ", "ОГРН: " };
            List<string> stringList = Regex.Split(FullOrgInfo,"\n").ToList();
            try
            {
                this.TaxPayerId
                    = Regex.Split(stringList.Where(str=>str.Contains(needToFind[0])).First(), @": ").Last();
                this.FullName = Regex.Split(stringList.Where(str => str.Contains(needToFind[1])).First(), @": ").Last();
                this.HeadPhoneNumber= Regex.Split(stringList.Where(str => str.Contains(needToFind[2])).First(), @": ").Last();
                try
                {
                    this.HeadFullName = new Person(Regex.Replace(Regex.Split(stringList.Where(str => str.Contains(needToFind[3])).First(), @": ").Last(), @"\t|\n|\r", ""));
            }
            catch (PersonParseException e)
            {
                throw new OrgInfoParseException("Data Parse error, not all fields provided", e);
            }
            this.TaxId = Regex.Split(stringList.Where(str => str.Contains(needToFind[4])).First(), @": ").Last();
                this.GovermentId = Regex.Split(stringList.Where(str => str.Contains(needToFind[5])).First(), @": ").Last();
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
