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
using System.Data;
using System.Device.Location;
using LibraryLib;



namespace Karpin2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// current row number
        /// </summary>
        private int rowNumber;
        /// <summary>
        /// edit index
        /// </summary>
        private int editIndex;
        /// <summary>
        /// List of all text boxes
        /// </summary>
        List<TextBox> textBoxList;
        /// <summary>
        /// integer text boxes
        /// </summary>
        List<TextBox> numericTextBoxes;
        /// <summary>
        /// double text boxes
        /// </summary>
        List<TextBox> doubleTextBoxes;

        /// <summary>
        /// Dialog service
        /// </summary>
        IDialogService dialogService;

        /// <summary>
        /// File sevice
        /// </summary>
        IExcelFileService fileService;

        /// <summary>
        /// <token manager
        /// </summary>
        ITableManager tableManager;

        /// <summary>
        /// validator
        /// </summary>
        Validator validator;

        /// <summary>
        /// window to enter row number
        /// </summary>
        RowsNumberWindow numberWindow;

        /// <summary>
        /// filter window
        /// </summary>
        FilterWindow filterWindow;

        /// <summary>
        /// chart window
        /// </summary>
        ChartWindow chartWindow;

        /// <summary>
        /// Ctor
        /// </summary>
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

        #region Handlers
        /// <summary>
        /// Open file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            dialogService = new DialogService();
            dialogService.OpenDialog = new OpenFileDialog();
            dialogService.OpenDialog.FileOk += new CancelEventHandler(openFileDialog_FileOk);
            dialogService.OpenFileDialog();
        }

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            { MessageBox.Show($"{ex.Message}\n"); }
            catch (Exception) { MessageBox.Show("Everything gone wrong"); }
        }

        /// <summary>
        /// Change rows number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) => this.Close();

        /// <summary>
        /// Add library button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddLibraryGrid.Visibility = Visibility.Visible;
            UpdateLibraryGrid.Visibility = Visibility.Hidden;
            HideDataGrid();
        }

        /// <summary>
        /// Add library
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Back to table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToTableButton_Click(object sender, RoutedEventArgs e) => this.ShowDataGrid();

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fileService.SaveDataTableToExcel(tableManager.CreateLoadedTableFromLibs());
                MessageBox.Show("Успешно");
            }
            catch (NullReferenceException) { MessageBox.Show("Нельзя сохранить то, что не открыто"); }
        }

        /// <summary>
        /// Delete library
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            tableManager.DeleteLibrary(DataView.SelectedIndex);
            UpdateDataGridContext();
        }

        /// <summary>
        /// Save as
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fileService.SaveDataTableToExcel(tableManager.CreateLoadedTableFromLibs(), dialogService.SaveFileDialog());
                MessageBox.Show("Успешно");
            }
            catch (NullReferenceException) { MessageBox.Show("Нельзя сохранить то, что не открыто"); }
        }

        /// <summary>
        /// Filter button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterLibraryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = tableManager.CreateCustomTable(tableManager.Libraries.Take(rowNumber).ToList());
            filterWindow = new FilterWindow();
            foreach (var item in tableManager.ColumnNames)
                filterWindow.ColumnComboBox.Items.Add(item);
            filterWindow.GoButton.Click += GoFilter;
            filterWindow.Show();
            filterWindow.BringIntoView();
        }

        /// <summary>
        /// Use filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoFilter(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Library> libs = new List<Library>();
                foreach (var lib in tableManager.Libraries)
                {
                    if ((bool)filterWindow.AccurateCheckBox.IsChecked)
                    {
                        if (lib[filterWindow.ColumnComboBox.Text].ToString() == filterWindow.FilterTextBox.Text)
                            libs.Add(lib);
                    }
                    else
                    {
                        if (lib[filterWindow.ColumnComboBox.Text].ToString().Contains(filterWindow.FilterTextBox.Text))
                            libs.Add(lib);
                    }
                }
                UpdateDataGridContext(libs);
                filterWindow.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// Get text box values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditLibraryMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //Think how to do it right
            textBoxList.ForEach(box => box.Clear());
            int index = DataView.SelectedIndex;
            if (index >= 0)
            {
                HideDataGrid();
                AddLibraryGrid.Visibility = Visibility.Hidden;
                UpdateLibraryGrid.Visibility = Visibility.Visible;
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

        /// <summary>
        /// Add library
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            tableManager.Libraries.Insert(editIndex, CreateLibFromTextBoxes());
            UpdateDataGridContext();
            ShowDataGrid();
        }

        /// <summary>
        /// Opens window to enter number of rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenRowNumberWindow(object sender, RoutedEventArgs e)
        {
            numberWindow = new RowsNumberWindow();
            numberWindow.Show();
            numberWindow.BringIntoView();
            numberWindow.SelectRowsButton.Click += SelectRowsButton_Click;
        }

        /// <summary>
        /// Reset DataView regenerating it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetFilterItemMenu_Click(object sender, RoutedEventArgs e) => UpdateDataGridContext();

        /// <summary>
        /// Open Diagram Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiagramsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            chartWindow = new ChartWindow(tableManager.Libraries);
            chartWindow.Show();
        }
        #endregion

        #region HelpFunctions
        /// <summary>
        /// Updating datagrid from tableManager libraries
        /// </summary>
        private void UpdateDataGridContext() => DataView.DataContext = tableManager.CreateCustomTable(tableManager.Libraries.Take(rowNumber).ToList());

        /// <summary>
        /// Updating datagrid from libraries
        /// </summary>
        /// <param name="libs">Libraries to form datagrid</param>
        private void UpdateDataGridContext(List<Library> libs) => DataView.DataContext = tableManager.CreateCustomTable(libs);

        /// <summary>
        /// Gets all textboxes values and creating library
        /// </summary>
        /// <returns>Created library</returns>
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
        #endregion

        #region VisibilitySwitchers
        /// <summary>
        /// Showing DataView and hiding contentGrid
        /// </summary>
        private void ShowDataGrid()
        {
            DataView.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Hidden;
        }
        
        /// <summary>
        /// Showing ContentGrid and hidding DataView
        /// </summary>
        private void HideDataGrid()
        {
            DataView.Visibility = Visibility.Hidden;
            ContentGrid.Visibility = Visibility.Visible;
        }





        #endregion

        
    }
}
