using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OfficeOpenXml;
using System.IO;


namespace LibraryLib
{
    public class FileService : IFileService
    {
        string _filePath;
        /// <summary>
        /// Opens xlsx file
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="FileServiceException">Throwing if wrong file selected</exception>
        /// <returns></returns>
        public DataTable Open(string filePath, bool hasHeader = true)
        {
            _filePath = filePath;
            using (var pck = new ExcelPackage())
            {
                try
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        pck.Load(stream);
                    }
                }
                catch (IOException e)
                {
                    throw new FileServiceException("Problem occured while opening stream", e);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                        row[cell.Start.Column - 1] = cell.Text;
                }
                return tbl;
            }

        }

        public void Save(DataTable table)
        {
            using (ExcelPackage objExcelPackage = new ExcelPackage())
            {
                ExcelWorksheet objWorksheet = objExcelPackage.Workbook.Worksheets.Add("Libraries");
                objWorksheet.Cells["A1"].LoadFromDataTable(table, true);
                objWorksheet.Cells.AutoFitColumns();
                if (File.Exists(_filePath))
                    File.Delete(_filePath);
                FileStream objFileStrm = File.Create(_filePath);
                objFileStrm.Close();
                File.WriteAllBytes(_filePath, objExcelPackage.GetAsByteArray());
            }
        }

    }

    [Serializable]
    public class FileServiceException : Exception
    {
        public FileServiceException() { }
        public FileServiceException(string message) : base(message) { }
        public FileServiceException(string message, Exception inner) : base(message, inner) { }
        protected FileServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
