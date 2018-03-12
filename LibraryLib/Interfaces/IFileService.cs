using System.Data;

namespace LibraryLib
{
    public interface IFileService
    {
        DataTable OpenExcelAsDataTable(string filePath, bool hasHeader= true);
        void Save(DataTable table);
        void SaveAs(DataTable table, string filePath);
    }
}