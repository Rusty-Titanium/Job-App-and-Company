using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Xml.Linq;

namespace Job_App_and_Company
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /**
         * 
         * Quick note. When focusing textbox, highlight text for easy re-searching for seraching textbox.
         * 
         * things that still need to be done:
         * - application object still needs to be implemented properly.
         * - I want a better way to access specific columns and whatnot, not using the hacky shit my anime organizer shit used. I think there might be a 
         * better way (mainly I didn't use the object sender when I really should be, so make sure to do that so its more seamless for choosing specific columns for editing)
         * - I should add in a way to delete just in case
         * - I need to implement a way to check if there are copies and shit for companies so I dont have duplicates.
         * 
         * additions
         * - I want to make sure that I have a way of adding aplications to job shit. I think I already am starting to put stuff in place, but I just wanted to put it in writing just in case.
         * 
         */

        //public ObservableCollection<Company> companies = new ObservableCollection<Company>();
        public bool changesSaved = true;

        //private String FileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // this one is for testing
        private String saveFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // this one will be the normal one we use, aka non-testing purposes.\
        
        private DataTable dTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();

            String jsonString;


            try
            {
                // original
                //jsonString = File.ReadAllText(saveFileDirectory + "/JobAppCompanyFile.json");
                //companies = JsonSerializer.Deserialize<ObservableCollection<Company>>(jsonString);

                jsonString = File.ReadAllText(saveFileDirectory + "/JobAppCompanyFile.json");
                ObservableCollection<Company> companies = JsonSerializer.Deserialize<ObservableCollection<Company>>(jsonString);


                
                dTable = new DataTable();
                dTable.BeginLoadData();
                dTable = ToDataTable(companies);
                dTable.EndLoadData();
                //dTable.AcceptChanges(); // Not actually sure if I need this here, but putting here for now in the event I do.
                dGrid.ItemsSource = dTable.DefaultView;
                

            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is JsonException)
            {

                if (ex is FileNotFoundException)
                {
                    MessageBoxResult result = MessageBox.Show("A file could not be found. Would you like to create a new file for this program?", "File Not Found", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                        File.Create(saveFileDirectory + "/JobAppCompanyFile.json");
                    else // Means the person clicked cancel. This should stop the app from running.
                        this.Close();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Existing save file can not be read. Would you like to open the application anyway?", "Issue Reading File", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Cancel)
                        this.Close();
                }
            }


            // we set companies list to the file I read in.
            // then I set the itemsource and should be good from there.

            //original
            //dGrid.ItemsSource = companies;


        }




        /// <summary>
        /// Converts a List to a DataTable. It seems like a generic implementation so I this should be useable anywhere with this problem.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T)); // Gets Collection of properties from Company Class
            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType); // Adds column with a name (value Name like 'Name' or 'OnGovWebsite') and sets its type.
            }

            object[] values = new object[props.Count]; // gets everything into a generic array

            foreach (T item in data) // This loops through each item in the original list and adds the values to each cooresponding row (which seems automatic)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// This method is to convert DataTable information to a List that can then be turned into a JSON format. this method is NOT generic and can't work with everything.
        /// </summary>
        /// <returns></returns>
        private IList<Company> DataTableToList()
        {
            List<Company> companyList = new List<Company>();

            foreach (DataRow row in dTable.Rows)
            {
                Company company = new Company();

                company.Name = (String)row["Name"];
                company.Trust = (Trust)row["Trust"];
                company.OnGovWebsite = (bool)row["OnGovWebsite"];
                company.Applications = (List<JobApplication>)row["Applications"];
                company.Notes = (String)row["Notes"];

                companyList.Add(company);
            }

            return companyList;
        }





        
        /// <summary>
        /// This method will filter out Companies based on the text in the Search Textbox. The link provided in the comments is to help understand how to do the filter text.
        /// https://www.csharp-examples.net/dataview-rowfilter/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            String filter = searchBox.Text;
            if (String.IsNullOrEmpty(filter))
                dTable.DefaultView.RowFilter = null;
            else
                dTable.DefaultView.RowFilter = String.Format("Name Like '%{0}%'", filter); // For more information on how this works, click the link above in the event's description.
        }


        private void dGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            // Currently I just stop any form of editing from happening, but will be changing that in the future probably.

            DataRow row = (DataRow)((DataRowView)e.Row.Item).Row;


            Debug.WriteLine(row["Notes"]);

            //Debug.WriteLine(e.Row.Item); // this item is the Company Object.
            //Debug.WriteLine(sender.GetType()); // this is the datagrid object


            Company company = new Company();

            company.Name = (String)row["Name"];
            company.Trust = (Trust)row["Trust"];
            company.OnGovWebsite = (bool)row["OnGovWebsite"];
            company.Applications = (List<JobApplication>)row["Applications"];
            company.Notes = (String)row["Notes"];

            MoreInfo more = new MoreInfo(company);

            mainGrid.Children.Add(more);




            e.Cancel = true;
        }









        /// <summary>
        /// This method is for making the page of creating a new Company object visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Company_Click(object sender, RoutedEventArgs e)
        {
            newCompany.Visibility = Visibility.Visible;
        }

        
        public void AddCompany(Company company)
        {
            DataRow dRow = dTable.NewRow();

            // Don't delete in the event I add more columns in the future. There is apparently no good way of getting the column and setting it that way, so you have to do it manually via
            //      the indexes of the columns which you see below.
            //Debug.WriteLine(dTable.Columns[0].ColumnName + "\n" + dTable.Columns[1].ColumnName + "\n" + dTable.Columns[2].ColumnName + "\n" + dTable.Columns[3].ColumnName + "\n" + dTable.Columns[4].ColumnName);

            dRow["Name"] = company.Name;
            dRow["Trust"] = company.Trust;
            dRow["OnGovWebsite"] = company.OnGovWebsite;
            dRow["Applications"] = company.Applications;
            dRow["Notes"] = company.Notes;

            dTable.Rows.Add(dRow);
            changesSaved = false;
        }
        




        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!changesSaved)
            {
                MessageBoxResult result = MessageBox.Show("There are changes that need to be saved. Do you want to save?", "Company Legitimacy", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                    Save();
                else if (result == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            String jsonString = JsonSerializer.Serialize(DataTableToList(), options);

            //File.WriteAllText(saveFileDirectory + "/JobAppCompanyFile.json", jsonString); 
            File.WriteAllText(saveFileDirectory + "/JobAppCompanyFile2.txt", jsonString); // Used for testing.

            changesSaved = true;
        }

        
    }





    class TrustConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Trust newValue = (Trust)value;

            switch (newValue)
            {
                case Trust.TrustWorthy:
                    return Brushes.Green;
                case Trust.CantTrust:
                    return Brushes.Red;
                case Trust.Unknown:
                    return Brushes.Orange;
            }

            // I should just use if else here, but I like having practice with switch statements. This is just in case there is somehow no value here.

            return Brushes.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(); // Might need change if these start getting thrown
        }
    }


    class GovWebsiteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool onGovWebsite = (bool)value;

            if (onGovWebsite)
                return Brushes.Green;
            else
                return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(); // Might need change if these start getting thrown
        }
    }


    class ApplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as List<JobApplication>).Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(); // Might need change if these start getting thrown
        }
    }


    class NotesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "Notes";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(); // Might need change if these start getting thrown
        }
    }

    


}
