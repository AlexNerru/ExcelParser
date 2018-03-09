using System.Data;

namespace LibraryLib
{
    public interface IFileService
    {
        DataTable Open(string filePath, bool hasHeader= true);
        void Save(DataTable table);
    }
}