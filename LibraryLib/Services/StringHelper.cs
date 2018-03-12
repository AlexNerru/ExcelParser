using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibraryLib
{
    public class StringeHelper
    {
        public Dictionary<string, string> GetValues(List<string> allString, 
            List<string> needToFind, Dictionary<string,string> rusToProp)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < needToFind.Count; i++)
            {
                string str1 = Regex.Split(allString.Where(str => str.Contains(needToFind[i])).First(), @": ").Last();
                dict.Add(rusToProp[needToFind[i]], str1);
            }
            return dict;
        }
    }
}
