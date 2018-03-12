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
using System.Device.Location;


namespace Karpin2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int rowNumber;
        private int editIndex;
        List<TextBox> textBoxList;
        List<TextBox> numericTextBoxes;
        List<TextBox> doubleTextBoxes;


        DialogService dialogService;
        FileService fileService;
        TableManager tableManager;
        Validator validator;

        RowsNumberWindow numberWindow;

        public MainWindow()
        {
            InitializeComponent();
            textBoxList = new List<TextBox>() { TaxIdTextBox, FullNameTextBox,
                TaxPayerIdTextBox, HeadPhoneTextBox, NameTextBox,
                SecondNameTextBox, SurnameTextBox, GovermentIdTextBox,
            AreaTextBox, CityTextBox, StreetTextBox, BuildingTextBox, HousingTextBox,
            DistrictTextBox, PostIndexTextBox, PhoneTextBpx, EmailTextBox, FaxTextBox, SiteTextBox,
                OpenHourTextBox, CloseHourTextBox, LatitudeTextBox, LongitudeTextBox };
            numericTextBoxes = new List<TextBox>() { OpenHourTextBox, CloseHourTextBox };
            doubleTextBoxes = new List<TextBox>() { LatitudeTextBox, LongitudeTextBox };
            validator = new Validator();
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
                DataTable table = fileService.OpenExcelAsDataTable(dialogService.FilePath);
                tableManager = new TableManager(table);

                numberWindow = new RowsNumberWindow();
                numberWindow.SelectRowsButton.Click += SelectRowsButton_Click;
                numberWindow.Show();
                numberWindow.BringIntoView();
            }
            catch (Exception ex) when (ex is FileServiceException
                                   || ex is TableValidationException
                                   || ex is TableParseException)
            { MessageBox.Show($"{ex.Message}\n{ex.InnerException.Message}"); }
            catch (Exception) { MessageBox.Show("Everything gone wrong"); }
        }

        private void SelectRowsButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(numberWindow.RowsTextBox.Text, out rowNumber))
            {
                this.rowNumber = int.Parse(numberWindow.RowsTextBox.Text);
                if (rowNumber < 1 || rowNumber > tableManager.Libraries.Count)
                    MessageBox.Show("Wrong row numbers");
                else
                {
                    UpdateDataGridContext();
                    numberWindow.Close();
                    EditMenuItem.Visibility = Visibility.Visible;
                }
            }
            else
                MessageBox.Show("Enter number");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) => this.Close();

        private void AddLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddLibraryGrid.Visibility = Visibility.Visible;
            UpdateLibraryGrid.Visibility = Visibility.Hidden;
            HideDataGrid();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (validator.AreBoxesNotEmpty(textBoxList)
                && validator.AreBoxesTextIsInt(numericTextBoxes)
                && validator.AreBoxesTextIsDouble(doubleTextBoxes))
            {
                tableManager.Libraries.Add(CreateLibFromTextBoxes());
                UpdateDataGridContext();
                ShowDataGrid();
            }
            else MessageBox.Show("Проверьте поля");
        }

        private void BackToTableButton_Click(object sender, RoutedEventArgs e) => this.ShowDataGrid();

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fileService.SaveDataTableToExcel(tableManager.CreateLoadedTableFromLibs());
                MessageBox.Show("Успешно");
            }
            catch (NullReferenceException) { MessageBox.Show("Нельзя сохранить то, что не открыто"); }
        }

        private void DeleteLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            tableManager.DeleteLibrary(DataView.SelectedIndex);
            UpdateDataGridContext();
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fileService.SaveDataTableToExcel(tableManager.CreateLoadedTableFromLibs(), dialogService.SaveFileDialog());
                MessageBox.Show("Успешно");
            }
            catch (NullReferenceException) { MessageBox.Show("Нельзя сохранить то, что не открыто"); }
        }

        private void FilterLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditLibraryMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //Think how to do it right
            textBoxList.ForEach(box => box.Clear());
            HideDataGrid();
            AddLibraryGrid.Visibility = Visibility.Hidden;
            UpdateLibraryGrid.Visibility = Visibility.Visible;
            int index = DataView.SelectedIndex;
            if (index >= 0)
            {
                Library lib = tableManager.Libraries[index];
                FullNameTextBox.Text = lib.FullName;
                TaxPayerIdTextBox.Text = lib.TaxPayerId;
                TaxIdTextBox.Text = lib.TaxId;
                GovermentIdTextBox.Text = lib.GovermentId;
                HeadPhoneTextBox.Text = lib.HeadPhoneNumber;
                NameTextBox.Text = lib.OrgInfo.HeadFullName.Name;
                SecondNameTextBox.Text = lib.OrgInfo.HeadFullName.SecondName;
                SurnameTextBox.Text = lib.OrgInfo.HeadFullName.Surname;
                OpenHourTextBox.Text = lib.WorkingHours.hours[Day.Wednesday].OpenHour.ToString();
                CloseHourTextBox.Text = lib.WorkingHours.hours[Day.Wednesday].CloseHour.ToString();
                LatitudeTextBox.Text = lib.GeoData.Coordinates[0].Latitude.ToString();
                LongitudeTextBox.Text = lib.GeoData.Coordinates[0].Longitude.ToString();
                CityTextBox.Text = lib.City;
                AreaTextBox.Text = lib.Area;
                BuildingTextBox.Text = lib.Building;
                StreetTextBox.Text = lib.Street;
                HousingTextBox.Text = lib.Housing;
                DistrictTextBox.Text = lib.District;
                PostIndexTextBox.Text = lib.PostIndex;
                PhoneTextBpx.Text = lib.Phone;
                EmailTextBox.Text = lib.Email;
                FaxTextBox.Text = lib.Fax;
                SiteTextBox.Text = lib.Site;
                tableManager.Libraries.RemoveAt(index);
                editIndex = index;
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            tableManager.Libraries.Insert(editIndex, CreateLibFromTextBoxes());
            UpdateDataGridContext();
            ShowDataGrid();
        }

        private void UpdateDataGridContext() => DataView.DataContext = tableManager.CreateCustomTable(tableManager.Libraries.Take(rowNumber).ToList());

        private Library CreateLibFromTextBoxes()
        {
            OrgInfo orgInfo = new OrgInfo(TaxPayerIdTextBox.Text, FullNameTextBox.Text,
                        HeadPhoneTextBox.Text, new Person(NameTextBox.Text, SecondNameTextBox.Text, SurnameTextBox.Text),
                        TaxIdTextBox.Text, GovermentIdTextBox.Text);
            Address address = new Address(DistrictTextBox.Text, AreaTextBox.Text,
                StreetTextBox.Text, PostIndexTextBox.Text, BuildingTextBox.Text, HousingTextBox.Text,
                CityTextBox.Text);
            Contact contact = new Contact(PhoneTextBpx.Text, FaxTextBox.Text, EmailTextBox.Text, SiteTextBox.Text);
            WorkingHours workingHours = new WorkingHours(int.Parse(OpenHourTextBox.Text), int.Parse(CloseHourTextBox.Text));
            GeoData geoData = new GeoData(double.Parse(LatitudeTextBox.Text), double.Parse(LongitudeTextBox.Text));
            Library lib = new Library(orgInfo, address, geoData, workingHours, contact);
            return lib;
        }

        #region VisibilitySwitchers
        private void ShowDataGrid()
        {
            DataView.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Hidden;
        }

        private void HideDataGrid()
        {
            DataView.Visibility = Visibility.Hidden;
            ContentGrid.Visibility = Visibility.Visible;
        }



        #endregion


    }
}
