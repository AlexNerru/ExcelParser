using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace LibraryLib
{
    public class Library
    {
        OrgInfo _orgInfo;
        Address _address;
        GeoData _geoData;
        WorkingHours _workingHours;
        Contact _contact;

        public string FullName
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.FullName;
                else return string.Empty;
            }
        }
        public string HeadFullName
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.HeadFullName.ToString();
                else return string.Empty;
            }
        }
        public string TaxPayerId
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.TaxPayerId;
                else return string.Empty;
            }
        }
        public string HeadPhoneNumber
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.HeadPhoneNumber;
                else return string.Empty;
            }
        }
        public string TaxId
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.TaxId;
                else return string.Empty;
            }
        }
        public string GovermentId
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.GovermentId;
                else return string.Empty;
            }
        }

        public string Area
        {
            get
            {
                if (_address != null) return _address.Area;
                else return string.Empty;
            }
        }
        public string Street
        {
            get
            {
                if (_address != null) return _address.Street;
                else return string.Empty;
            }
        }
        public string Building
        {
            get
            {
                if (_address != null) return _address.Building;
                else return string.Empty;
            }
        }
        public string Housing
        {
            get
            {
                if (_address != null) return _address.Housing;
                else return string.Empty;
            }
        }
        public string City
        {
            get
            {
                if (_address != null) return _address.City;
                else return string.Empty;
            }
        }
        public string PostIndex
        {
            get
            {
                if (_address != null) return _address.PostIndex;
                else return string.Empty;
            }
        }
        public string District
        {
            get
            {
                if (_address != null) return _address.District;
                else return string.Empty;
            }
        }

        public string Phone
        {
            get
            {
                if (_contact != null) return _contact.PublicPhone;
                else return string.Empty;
            }
        }
        public string Site
        {
            get
            {
                if (_contact != null) return _contact.Website;
                else return string.Empty;
            }
        }
        public string Fax
        {
            get
            {
                if (_contact != null) return _contact.Fax;
                else return string.Empty;
            }
        }
        public string Email
        {
            get
            {
                if (_contact != null) return _contact.Email;
                else return string.Empty;
            }
        }

        public string Coords
        {
            get
            {
                if (_geoData != null) return _geoData.ToString();
                else return string.Empty;
            }
        }



        public Library(DataRow row)
        {
            //TODO: Update with column name search
            //row.Table.Columns["ColumnName"].Ordinal + 1
            _orgInfo = new OrgInfo(row[4].ToString());
            _address = new Address(row[5].ToString());
            _geoData = new GeoData(row[19].ToString());
            _workingHours = new WorkingHours(row[12].ToString());
            _contact = new Contact(row[9].ToString(), row[10].ToString(), row[11].ToString(), row[14].ToString());
        }

        public Library(OrgInfo org) => _orgInfo = org;
        public Library(OrgInfo org, Address address) : this(org) => _address = address;
        public Library(OrgInfo org, Address address, GeoData geo) : this(org, address) => _geoData = geo;
        public Library(OrgInfo org, Address address, GeoData geo, WorkingHours hours)
            : this(org, address, geo) => _workingHours = hours;
        public Library(OrgInfo org, Address address, GeoData geo, WorkingHours hours, Contact contact)
            : this(org, address, geo, hours) => _contact = contact;

        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(Library);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(Library);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }

        }

    }
}
