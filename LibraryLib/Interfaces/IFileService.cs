using System.Data;

namespace LibraryLib
{
    /// <summary>
    /// Express service to work with ExcelFile
    /// </summary>
    public interface IExcelFileService
    {
        DataTable OpenExcelAsDataTable(string filePath, bool hasHeader= true);
        void SaveDataTableToExcel(DataTable table, string filePath);
    }
}