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
        TableManager tableManager;
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
                DataTable table = fileService.Open(dialogService.FilePath);
                tableManager = new TableManager(table);
                Libraries libraries = tableManager.GetLibrariesFromLoadedTable();
                DataView.DataContext = tableManager.CreateTableFromLibs(libraries);
                EditMenuItem.Visibility = Visibility.Visible;
        }
            catch (Exception ex) when(ex is FileServiceException || ex is TableValidationException || ex is TableParseException) { MessageBox.Show(ex.Message); }
            catch (Exception) { MessageBox.Show("Everything gone wrong"); }
}

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) => this.Close();

        private void AddLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataView.Visibility = Visibility.Hidden;
            ContentGrid.Visibility = Visibility.Visible;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Libraries libraries = tableManager.GetLibrariesFromLoadedTable();
            OrgInfo orgInfo = new OrgInfo(FullNameTextBox.Text,
                TaxPayerIdTextBox.Text, new Person(NameTextBox.Text, SecondNameTextBox.Text, SurnameTextBox.Text));
            Library lib = new Library(orgInfo);
            libraries.Create(lib);
            tableManager.AddLibraryToCustomTable(lib);
            DataView.DataContext = tableManager.CustomTable;
            DataView.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Hidden;
        }

        private void BackToTableButton_Click(object sender, RoutedEventArgs e)
        {
            DataView.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Hidden;
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            fileService.Save(tableManager.LoadedTable);
            MessageBox.Show("Успешно");
        }
    }
}
