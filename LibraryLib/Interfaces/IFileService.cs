using System.Data;

namespace LibraryLib
{
    public interface IFileService
    {
        DataSet Open(string filePath);
    }
}