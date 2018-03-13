using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace LibraryLib
{
    /// <summary>
    /// Class provides help functions for parsing
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Returns prop name and value
        /// </summary>
        /// <param name="allString"></param>
        /// <param name="rusToProp"></param>
        /// <returns></returns>
        public Dictionary<string, string> ParseStringsToValueProperty(List<string> allString, Dictionary<string, string> rusToProp)
        {
            List<string> needToFind = rusToProp.Keys.ToList();
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