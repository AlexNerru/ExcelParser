using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    public class Contact
    {
        /// <summary>
        /// Public phone
        /// </summary>
        public string PublicPhone { get; set; }
        /// <summary>
        /// Fax number
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Website link
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="fax"></param>
        /// <param name="email"></param>
        /// <param name="site"></param>
        public Contact(string phone, string fax, string email, string site)
        {
            PublicPhone = phone;
            Fax = fax;
            Email = email;
            Website = site;
        }
    }
}
