using System.Collections.Generic;
using System.Data;

namespace LibraryLib
{
    /// <summary>
    /// Express object to manage library table
    /// </summary>
    public interface ITableManager
    {
        List<Library> Libraries { get; set; }
        DataTable LoadedTable { get; set; }

        void AddLibrary(Library lib);
        DataTable CreateCustomTable(List<Library> libs);
        DataTable CreateLoadedTableFromLibs();
        void DeleteLibrary(int index);
        List<Library> GetLibrariesFromTable(DataTable LoadedTable);
    }
}