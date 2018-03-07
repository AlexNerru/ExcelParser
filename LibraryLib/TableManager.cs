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
        public DataTable Table { get; private set; }

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
