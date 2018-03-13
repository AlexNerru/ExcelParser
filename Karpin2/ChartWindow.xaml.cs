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
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using LibraryLib;

namespace Karpin2
{
    /// <summary>
    /// Логика взаимодействия для ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        List<string> columnNames = new List<string>() { "FullName", "HeadFullName", "TaxPayerId",
                "TaxId", "HeadPhoneNumber", "GovermentId", "Area", "PostIndex", "City", "Street",
                "Building", "Housing", "District", "Coords", "Phone", "Fax", "Email", "Site"};

        Dictionary<string, SeriesChartType> graphNames
            = new Dictionary<string, SeriesChartType>()
            {
            {"Line", SeriesChartType.Line },
                {"Point", SeriesChartType.Point },
                {"Bar", SeriesChartType.Bar },
                {"Candlestick", SeriesChartType.Candlestick },
                {"Column", SeriesChartType.Column }
            };

        List<Library> libs;

        public ChartWindow(List<Library> libs)
        {
            InitializeComponent();
            this.libs = libs;
            foreach (var item in columnNames)
                ColumnNamesComboBox.Items.Add(item);
            foreach (var item in graphNames.Keys)
                ChartTypeComboBox.Items.Add(item);
            chart.Series.Add(new Series("Series1"));
            chart.ChartAreas.Add(new ChartArea("Default"));
        }

        private void ShowChartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ColumnNamesComboBox.SelectedItem != null && ChartTypeComboBox.SelectedItem!=null)
            {
                chart.Series["Series1"].ChartArea = "Default";
                chart.Series["Series1"].ChartType = graphNames[ChartTypeComboBox.SelectedItem.ToString()];
                string val = ColumnNamesComboBox.SelectedItem.ToString();
                Dictionary<string, int> dict = new Dictionary<string, int>();
                try
                {
                    foreach (Library item in libs)

                        if (dict.ContainsKey(item[val].ToString()))
                            dict[item[val].ToString()]++;
                        else
                            dict[item[val].ToString()] = 1;


                    chart.Series["Series1"].Points.DataBindXY(dict.Keys, dict.Values);
                    chart.Show();
                }
                catch (NullReferenceException ex)
                {
                    System.Windows.MessageBox.Show("Данные не подходят для построения");
                }
            }
            else
                System.Windows.MessageBox.Show("Выберете колонку");
        }
    }
}
