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
    /// <summary>
    /// Works with dialogs windows
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Loaded file patb
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Openfiledialog
        /// </summary>
        public OpenFileDialog OpenDialog { get;  set; }

        /// <summary>
        /// Savefiledialog
        /// </summary>
        public SaveFileDialog SaveDialog { get;  set; }

        /// <summary>
        /// Opening open file dialog
        /// </summary>
        public void OpenFileDialog()
        {
            OpenDialog.Filter = "Excel Files| *.xls; *.xlsx";
            OpenDialog.ValidateNames = true;
            OpenDialog.CheckFileExists = true;
            OpenDialog.ShowDialog();
        }

        /// <summary>
        /// Openeing save file dialog
        /// </summary>
        /// <returns></returns>
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
