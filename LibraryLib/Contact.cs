using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    public class Contact
    {
        public string PublicPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public Contact(string phone, string fax, string email, string site)
        {
            PublicPhone = phone;
            Fax = fax;
            Email = email;
            Website = site;
        }
    }
}
