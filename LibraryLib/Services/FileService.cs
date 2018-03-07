using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace LibraryLib
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Opens xlsx file
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="WrongFileException">Throwing if wrong file selected</exception>
        /// <returns></returns>
        public DataSet Open (string filePath)
        {
            if (filePath.Contains("Библиотека.xlsx"))
            {
                System.Data.OleDb.OleDbConnection MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;" +
                    $"Data Source='{filePath}';Extended Properties=Excel 8.0;");
                System.Data.OleDb.OleDbDataAdapter MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet0$]", MyConnection);
                DataSet DtSet = new DataSet();
                MyCommand.Fill(DtSet);
                return DtSet;
            }
            else
                throw new WrongFileException("You are openning wrong file");
        }

    }

    [Serializable]
    public class WrongFileException : Exception
    {
        public WrongFileException() { }
        public WrongFileException(string message) : base(message) { }
        public WrongFileException(string message, Exception inner) : base(message, inner) { }
        protected WrongFileException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
