using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

/// <summary>
/// Main namespace
/// </summary>
namespace LibraryLib
{
    /// <summary>
    /// Class represents library
    /// </summary>
    public class Library
    {
        /// <summary>
        /// OrgInfo field
        /// </summary>
        OrgInfo _orgInfo;

        /// <summary>
        /// Address
        /// </summary>
        Address _address;

        /// <summary>
        /// GeoData
        /// </summary>
        GeoData _geoData;
        /// <summary>
        /// Working Hours
        /// </summary>
        WorkingHours _workingHours;
        /// <summary>
        /// ContactInfo
        /// </summary>
        Contact _contact;

        //TODO: Think how to use objects
        #region Properties
        /// <summary>
        /// Full name of library
        /// </summary>
        public string FullName
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.FullName;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Full name of chief of library
        /// </summary>
        public string HeadFullName
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.HeadFullName.ToString();
                else return string.Empty;
            }
        }

        /// <summary>
        /// Tax Payer Id of library
        /// </summary>
        public string TaxPayerId
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.TaxPayerId;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Phone of chief of library
        /// </summary>
        public string HeadPhoneNumber
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.HeadPhoneNumber;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Tax Id of library
        /// </summary>
        public string TaxId
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.TaxId;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Goverment Id of library
        /// </summary>
        public string GovermentId
        {
            get
            {
                if (_orgInfo != null) return _orgInfo.GovermentId;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Area of library
        /// </summary>
        public string Area
        {
            get
            {
                if (_address != null) return _address.Area;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Street of library
        /// </summary>
        public string Street
        {
            get
            {
                if (_address != null) return _address.Street;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Building of library
        /// </summary>
        public string Building
        {
            get
            {
                if (_address != null) return _address.Building;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Housing of library
        /// </summary>
        public string Housing
        {
            get
            {
                if (_address != null) return _address.Housing;
                else return string.Empty;
            }
        }

        /// <summary>
        /// City of library
        /// </summary>
        public string City
        {
            get
            {
                if (_address != null) return _address.City;
                else return string.Empty;
            }
        }

        /// <summary>
        /// PostIndex of library
        /// </summary>
        public string PostIndex
        {
            get
            {
                if (_address != null) return _address.PostIndex;
                else return string.Empty;
            }
        }

        /// <summary>
        /// District of library
        /// </summary>
        public string District
        {
            get
            {
                if (_address != null) return _address.District;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Public phone of library
        /// </summary>
        public string Phone
        {
            get
            {
                if (_contact != null) return _contact.PublicPhone;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Website of library
        /// </summary>
        public string Site
        {
            get
            {
                if (_contact != null) return _contact.Website;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Fax of library
        /// </summary>
        public string Fax
        {
            get
            {
                if (_contact != null) return _contact.Fax;
                else return string.Empty;
            }
        }

        /// <summary>
        /// Email of library
        /// </summary>
        public string Email
        {
            get
            {
                if (_contact != null) return _contact.Email;
                else return string.Empty;
            }
        }

        /// <summary>
        /// String represent of geoData
        /// </summary>
        public string Coords { get => _geoData != null ? _geoData.ToString() : string.Empty; }

        /// <summary>
        /// String represent of working hours
        /// </summary>
        public string Hours { get => _workingHours != null ? _workingHours.ToString() : string.Empty; }

        /// <summary>
        /// Contact member
        /// </summary>
        public Contact Contact { get => _contact; set => _contact = value; }
        /// <summary>
        /// OrgInfo member
        /// </summary>
        public OrgInfo OrgInfo { get => _orgInfo; set => _orgInfo = value; }
        /// <summary>
        /// Address member
        /// </summary>
        public Address Address { get => _address; set => _address = value; }
        /// <summary>
        /// GeoData member
        /// </summary>
        public GeoData GeoData { get => _geoData; set => _geoData = value; }
        /// <summary>
        /// Working Hours member
        /// </summary>
        public WorkingHours WorkingHours { get => _workingHours; set => _workingHours = value; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the Library class to the value indicated by datarow
        /// </summary>
        /// <param name="row">DataRow</param>
        public Library(DataRow row)
        {
            _orgInfo = new OrgInfo(row[row.Table.Columns["OrgInfo"].Ordinal].ToString());
            _address = new Address(row[row.Table.Columns["ObjectAddress"].Ordinal].ToString());
            _geoData = new GeoData(row[row.Table.Columns["geoData"].Ordinal].ToString());
            _workingHours = new WorkingHours(row[row.Table.Columns["WorkingHours"].Ordinal].ToString());
            _contact = new Contact(row[row.Table.Columns["PublicPhone"].Ordinal].ToString(),
                row[row.Table.Columns["Fax"].Ordinal].ToString(), 
                row[row.Table.Columns["Email"].Ordinal].ToString(),
                row[row.Table.Columns["WebSite"].Ordinal].ToString());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="org"></param>
        public Library(OrgInfo org) => _orgInfo = org;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="org"></param>
        /// <param name="address"></param>
        public Library(OrgInfo org, Address address) : this(org) => _address = address;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="org"></param>
        /// <param name="address"></param>
        /// <param name="geo"></param>
        public Library(OrgInfo org, Address address, GeoData geo) : this(org, address) => _geoData = geo;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="org"></param>
        /// <param name="address"></param>
        /// <param name="geo"></param>
        /// <param name="hours"></param>
        public Library(OrgInfo org, Address address, GeoData geo, WorkingHours hours)
            : this(org, address, geo) => _workingHours = hours;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="org"></param>
        /// <param name="address"></param>
        /// <param name="geo"></param>
        /// <param name="hours"></param>
        /// <param name="contact"></param>
        public Library(OrgInfo org, Address address, GeoData geo, WorkingHours hours, Contact contact)
            : this(org, address, geo, hours) => _contact = contact;

        
        /// <summary>
        /// Indexator to acces property by it's string name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
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
