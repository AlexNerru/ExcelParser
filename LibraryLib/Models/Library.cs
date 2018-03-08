using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LibraryLib
{
    public class Library
    {
        OrgInfo _orgInfo;
        Address _address;
        GeoData _geoData;
        WorkingHours _workingHours;
        Contact _contact;

        public string FullName { get { return _orgInfo.FullName; } }
        public string HeadFullName { get { return _orgInfo.HeadFullName.ToString(); } }
        public string TaxPayerId { get { return _orgInfo.TaxPayerId; } }
        public string Area { get { return _address.Area; } }
        public string Street { get { return _address.Street; } }
        public string Building { get { return _address.Building; } }
        public string Phone { get { return _contact.PublicPhone; } }
        public string Site { get { return _contact.Website; } }

        public Library(DataRow row)
        {
            _orgInfo = new OrgInfo(row[4].ToString());
            _address = new Address(row[5].ToString());
            _geoData = new GeoData(row[19].ToString());
            _workingHours = new WorkingHours(row[12].ToString());
            _contact = new Contact(row[9].ToString(), row[10].ToString(), row[11].ToString(), row[14].ToString());
        }
    }
}
