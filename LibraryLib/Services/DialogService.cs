using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using System.ComponentModel;

namespace LibraryLib
{
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; }
        public OpenFileDialog OpenDialog { get;  set; }
        public SaveFileDialog SaveDialog { get;  set; }

        public void OpenFileDialog()
        {
            OpenDialog.Filter = "Excel Files| *.xls; *.xlsx";
            OpenDialog.ValidateNames = true;
            OpenDialog.CheckFileExists = true;
            OpenDialog.ShowDialog();
        }

        public string SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files| *.xls; *.xlsx";
            if (saveFileDialog.ShowDialog() == true)
                FilePath = saveFileDialog.FileName;
            return FilePath;
        }

    }
}
