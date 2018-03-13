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
    /// <summary>
    /// Incapsulates file logic
    /// </summary>
    public class FileService:IExcelFileService
    {
        /// <summary>
        /// Stores file path ща opened file
        /// </summary>
        string _openedFilePath;

        /// <summary>
        /// Opens xlsx file
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="FileServiceException">Throwing if wrong file selected</exception>
        /// <returns></returns>
        public DataTable OpenExcelAsDataTable(string filePath, bool hasHeader = true)
        {
            _openedFilePath = filePath;
            using (var pck = new ExcelPackage())
            {
                try
                {
                    using (var stream = File.OpenRead(filePath)) { pck.Load(stream); }
                }
                catch (IOException e) { throw new FileServiceException("Problem occured while opening stream", e); }

                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                for (int rowNum = hasHeader ? 2 : 1; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                        row[cell.Start.Column - 1] = cell.Text;
                }
                return tbl;
            }
        }

        /// <summary>
        /// Saves provided DataTable to it's opened path or to new path
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="filePath">Custom file path</param>
        public void SaveDataTableToExcel(DataTable table, string filePath = "")
        {
            using (ExcelPackage objExcelPackage = new ExcelPackage())
            {
                if (filePath == "")
                    filePath = _openedFilePath;
                ExcelWorksheet objWorksheet = objExcelPackage.Workbook.Worksheets.Add("Libraries");
                objWorksheet.Cells["A1"].LoadFromDataTable(table, true);
                objWorksheet.Cells.AutoFitColumns();
                try
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    FileStream objFileStrm = File.Create(filePath);
                    objFileStrm.Close();
                }
                catch (IOException e) { throw new FileServiceException("Problem occured while opening stream\nClose excel", e); }
                catch(UnauthorizedAccessException e) { throw new FileServiceException("Something with access", e); }
                File.WriteAllBytes(filePath, objExcelPackage.GetAsByteArray());
            }
        }
    }

    /// <summary>
    /// Thrown if smth happened while working with files
    /// </summary>
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
