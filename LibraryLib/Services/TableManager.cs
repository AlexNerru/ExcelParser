using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LibraryLib
{
    /// <summary>
    /// Class using to operate with Libraries and table
    /// </summary>
    public class TableManager : ITableManager
    {
        /// <summary>
        /// Loaded table
        /// </summary>
        public DataTable LoadedTable { get; set; }

        /// <summary>
        /// Add libs
        /// </summary>
        public List<Library> Libraries { get; set; }

        /// <summary>
        /// Columns than we need to create in table
        /// </summary>
        List<string> columnNames = new List<string>() { "FullName", "HeadFullName", "TaxPayerId",
                "TaxId", "HeadPhoneNumber", "GovermentId", "Area", "PostIndex", "City", "Street",
                "Building", "Housing", "District", "Coords", "Phone", "Fax", "Email", "Site"};

        /// <summary>
        /// Get all column names
        /// </summary>
        public List<string> ColumnNames { get => columnNames; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataTable"></param>
        public TableManager(DataTable dataTable)
        {
            if (ValidateTable(dataTable))
            {
                LoadedTable = dataTable;
                Libraries = GetLibraries(dataTable);
            }
            else throw new TableValidationException("Table is not legal");
        }

        /// <summary>
        /// Delete lib
        /// </summary>
        /// <param name="index"></param>
        public void DeleteLibrary(int index)
        {
            if (index >= 0)
                Libraries.RemoveAt(index);
        }

        /// <summary>
        /// Add lib
        /// </summary>
        /// <param name="lib"></param>
        public void AddLibrary(Library lib)=> Libraries.Add(lib);

        /// <summary>
        /// Create table in loaded format
        /// </summary>
        /// <returns></returns>
        public DataTable CreateLoadedTableFromLibs()
        {
            LoadedTable.Rows.Clear();
            foreach (Library item in Libraries)
            {
                DataRow row = LoadedTable.NewRow();
                row["Category"] = "Библиотека";
                row["FullName"] = item.FullName;
                row["OrgInfo"] = item.OrgInfo.ToString();
                row["ObjectAddress"] = item.Address.ToString();
                row["PublicPhone"] = item.Phone;
                row["Email"] = item.Email;
                row["Fax"] = item.Fax;
                row["WebSite"] = item.Site;
                row["GeoData"] = item.Coords;
                row["WorkingHours"] = item.Hours;
                LoadedTable.Rows.Add(row); 
            }
            return LoadedTable;
        }

        /// <summary>
        /// Creates DataTable в Libraries object
        /// </summary>
        /// <param name="libs"></param>
        /// <returns></returns>
        public DataTable CreateCustomTable(List<Library> libs)
        {
            DataTable table = new DataTable();
            foreach (var item in columnNames)
                table.Columns.Add(item);
            foreach (var lib in libs)
            {
                DataRow dr = table.NewRow();
                dr = this.CreateDataRow(dr, lib);
                table.Rows.Add(dr);
            }
            return table;
        }

        /// <summary>
        /// Get all libs from loaded table
        /// </summary>
        /// <param name="LoadedTable"></param>
        /// <returns></returns>
        public List<Library> GetLibraries(DataTable LoadedTable)
        {
            if (LoadedTable != null)
            {
                List<Library> libs = new List<Library>();
                foreach (DataRow item in LoadedTable.Rows)
                    try
                    {
                        libs.Add(new Library(item));
                    }
                    catch (Exception e) when (e is OrgInfoParseException || e is AdressParseException
                    || e is GeoParseException || e is GeoParseException || e is WorkingHoursParseException)
                    {
                        throw new TableParseException("Smth with table parcing", e);
                    }
                return libs;
            }
            else throw new TableNotProvidedException("You haven't loaded table");
        }

        /// <summary>
        /// Create data row
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="lib"></param>
        /// <returns></returns>
        DataRow CreateDataRow(DataRow dr, Library lib)
        {
            foreach (var item in columnNames)
                dr[item] = lib[item];
            return dr;
        }

        /// <summary>
        /// If table is valid
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        bool ValidateTable(DataTable table)
        {
            bool flag = true;
            foreach (DataRow row in table.Rows)
                if (row[0].ToString() != "Библиотека")
                    flag = false;
            return flag;
        }
    }

    /// <summary>
    /// Thrown if there is no table
    /// </summary>
    [Serializable]
    public class TableNotProvidedException : Exception
    {
        public TableNotProvidedException() { }
        public TableNotProvidedException(string message) : base(message) { }
        public TableNotProvidedException(string message, Exception inner) : base(message, inner) { }
        protected TableNotProvidedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Thrown if smth happened while parsing
    /// </summary>
    [Serializable]
    public class TableParseException : Exception
    {
        public TableParseException() { }
        public TableParseException(string message) : base(message) { }
        public TableParseException(string message, Exception inner) : base(message, inner) { }
        protected TableParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// If table is not valid
    /// </summary>
    [Serializable]
    public class TableValidationException : Exception
    {
        public TableValidationException() { }
        public TableValidationException(string message) : base(message) { }
        public TableValidationException(string message, Exception inner) : base(message, inner) { }
        protected TableValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
