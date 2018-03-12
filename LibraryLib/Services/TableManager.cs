using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LibraryLib
{
    public class TableManager
    {
        public DataTable LoadedTable { get; set; }
        public List<Library> Libraries { get; set; }

        List<string> columnNames = new List<string>() { "FullName", "HeadFullName", "TaxPayerId",
                "TaxId", "HeadPhoneNumber", "GovermentId", "Area", "PostIndex", "City", "Street",
                "Building", "Housing", "District", "Coords", "Phone", "Fax", "Email", "Site"};

        public TableManager(DataTable dataTable)
        {
            if (ValidateTable(dataTable))
            {
                LoadedTable = dataTable;
                Libraries = GetLibraries(dataTable);
            }
            else throw new TableValidationException("Table is not legal");
        }

        public void DeleteLibrary(int index)
        {
            if (index >= 0)
                Libraries.RemoveAt(index);
        }

        public void AddLibrary(Library lib)=> Libraries.Add(lib);

        
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


        List<Library> GetLibraries(DataTable LoadedTable)
        {
            if (LoadedTable != null)
            {
                List<Library> libs = new List<Library>();
                foreach (DataRow item in LoadedTable.Rows)
                    //try
                    //{
                        libs.Add(new Library(item));
                    //}
                    //catch (Exception e) when (e is OrgInfoParseException || e is AdressParseException
                    //|| e is GeoParseException || e is GeoParseException || e is WorkingHoursParseException)
                    //{
                    //    throw new TableParseException("Smth with table parcing", e);
                    //}
                return libs;
            }
            else throw new TableNotProvidedException("You haven't loaded table");
        }

        DataRow CreateDataRow(DataRow dr, Library lib)
        {
            foreach (var item in columnNames)
                dr[item] = lib[item];
            return dr;
        }

        bool ValidateTable(DataTable table)
        {
            bool flag = true;
            foreach (DataRow row in table.Rows)
                if (row[0].ToString() != "Библиотека")
                    flag = false;
            return flag;
        }
    }

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
