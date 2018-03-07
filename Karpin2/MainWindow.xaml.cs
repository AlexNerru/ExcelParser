using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.ComponentModel;
using LibraryLib;
using System.Data;


namespace Karpin2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDialogService dialogService;
        IFileService fileService;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            dialogService = new DialogService();
            dialogService.OpenDialog = new OpenFileDialog();
            dialogService.OpenDialog.FileOk += new CancelEventHandler(openFileDialog_FileOk);
            dialogService.OpenFileDialog();

        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            fileService = new FileService();
            dialogService.FilePath = dialogService.OpenDialog.FileName;
            try
            {
                DataSet filePath = fileService.Open(dialogService.FilePath);
                TableManager tableManager = new TableManager(filePath);
            }
            catch (WrongFileException exp)
            {
                MessageBox.Show(exp.Message);
            }
            catch (TableValidationException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
