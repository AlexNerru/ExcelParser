using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryLib;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace LibraryLib
{
    public class TableManager
    {
        public DataTable LoadedTable { get; set; }
        public DataTable CustomTable { get; set; }
        List<string> columnNames = new List<string>() { "FullName", "HeadFullName", "TaxPayerId",
                "TaxId", "HeadPhoneNumber", "GovermentId", "Area", "PostIndex", "City", "Street",
                "Building", "Housing", "District", "Coords", "Phone", "Fax", "Email", "Site"};
        public TableManager(DataTable dataTable)
        {
            if (ValidateTable(dataTable))
            {
                LoadedTable = dataTable;
            }
            else throw new TableValidationException("Table is not legal");
        }
       
        public Libraries GetLibrariesFromLoadedTable ()
        {
            if (LoadedTable != null)
            {
                Libraries libraries = new Libraries();
                foreach (DataRow item in LoadedTable.Rows)
                    try
                    {
                        libraries.Create(new Library(item));
                    }
                    catch(Exception e) when (e is OrgInfoParseException || e is AdressParseException 
                    || e is GeoParseException || e is GeoParseException || e is WorkingHoursParseException)
                    {
                        throw new TableParseException("Smth with table parcing", e);
                    }          
                return libraries;
            }
            else throw new TableNotProvidedException("You haven't loaded table");
        }

        public void AddLibraryToCustomTable(Library lib)
        {
            DataRow dr = CustomTable.NewRow();
            dr = this.CreateDataRow(dr, lib);
            this.AddLibraryToLoadedTabel(dr);
            CustomTable.Rows.Add(dr);
        }

        void AddLibraryToLoadedTabel(DataRow row)
        {
            DataRow dr = LoadedTable.NewRow();
            dr["Category"] = "Библиотека";
            dr["FullName"] = row["FullName"];
            dr["OrgInfo"] = $"Телефон руководителя: {row["HeadPhoneNumber"]}\nПолное наименование учреждения: {row["FullName"]}\nИНН: {row["TaxPayerId"]}\nКПП: {row["TaxId"]}\nОГРН: {row["GovermentId"]}\nФИО руководителя учреждения: {row["HeadFullName"]}";
            LoadedTable.Rows.Add(dr);
            dr["ObjectAddress"] = $"Административный округ: {row["District"]}\nРайон: {row["Area"]}\nПочтовый индекс: {row["PostIndex"]}\nАдрес: {row["City"]}, {row["Street"]}, {row["Building"]}, {row["Housing"]}";
            dr["geoData"] = row["Coords"];
            dr["PublicPhone"] = row["Phone"];
            dr["Email"] = row["Email"];
            dr["Fax"] = row["Fax"];
            dr["Site"] = row["Site"];
            //TODO: Working hours
        }

       /// <summary>
       /// Creates DataTable в Libraries object
       /// </summary>
       /// <param name="libs"></param>
       /// <returns></returns>
       public DataTable CreateTableFromLibs (Libraries libs)
        {
            DataTable table = new DataTable();
            foreach (var item in columnNames)
            {
                table.Columns.Add(item);
            }
            foreach (var lib in libs.GetAll())
            {
                DataRow dr = table.NewRow();
                dr = this.CreateDataRow(dr, lib);
                table.Rows.Add(dr);
            }
            this.CustomTable = table;
            return CustomTable;
        }

        DataRow CreateDataRow (DataRow dr, Library lib)
        {
            foreach (var item in columnNames)
                dr[item] = lib[item];
            return dr;
        }

        bool ValidateTable(DataTable table)
        {
            bool flag = true;
            foreach (DataRow row in table.Rows)
            {
                if (row[0].ToString() != "Библиотека")
                    flag = false;
            }
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
