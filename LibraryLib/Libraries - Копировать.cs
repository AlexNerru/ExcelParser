using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLib
{
    public class Libraries
    {
        List<Library> libraries = new List<Library>();
        public void Create(Library lib) => libraries.Add(lib);

        public void Delete (Library lib) => libraries.Remove(lib);

    }
}
