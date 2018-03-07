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
