using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LibraryLib
{
    class Library
    {
        OrgInfo _orgInfo;
        Address _address;
        GeoData _geoData;
        WorkingHours _workingHours;
        Contact _contact;
        public Library(DataRow row, Address address)
        {

        }
        public Library(DataRow row)
        {
            
        }
    }
}
