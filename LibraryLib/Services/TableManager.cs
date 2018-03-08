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
        DataTable Table { get; set; }

        public TableManager(DataSet dataSet)
        {
            if (ValidateTable(dataSet.Tables[0]))
            {
                Table = dataSet.Tables[0];
                
            }
            else throw new TableValidationException("Table is not legal");
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
       
        public Libraries GetLibraries ()
        {
            if (Table != null)
            {
                Libraries libraries = new Libraries();
                foreach (DataRow item in Table.Rows)
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
            else throw new TableNotProvidedException("Данные какие-то кривые");
        }

       public DataTable CreateTableFromLibs (Libraries libs)
        {
            DataTable table = new DataTable();
            table.Columns.Add("FullName");
            table.Columns.Add("HeadFullName");
            table.Columns.Add("TaxPayerId");
            table.Columns.Add("Area");
            table.Columns.Add("Street");
            table.Columns.Add("Building");
            table.Columns.Add("Phone");
            table.Columns.Add("Site");
            foreach (var lib in libs.GetAll())
            {
                DataRow dr = table.NewRow();
                dr["FullName"] = lib.FullName;
                dr["HeadFullName"] = lib.HeadFullName;
                dr["TaxPayerId"] = lib.TaxPayerId;
                dr["Area"] = lib.Area;
                dr["Street"] = lib.Street;
                dr["Building"] = lib.Building;
                dr["Phone"] = lib.Phone;
                dr["Site"] = lib.Site;
                table.Rows.Add(dr);
            }
            return table;
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
