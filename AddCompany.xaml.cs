using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Job_App_and_Company
{
    /// <summary>
    /// Interaction logic for AddCompany.xaml
    /// </summary>
    public partial class AddCompany : UserControl
    {
        public AddCompany()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

            Clear_Information();
        }


        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // create company object here, then send back.

            Company company = new Company();

            // make a check that a name is there, and check if there are any copies.


            if (nameTBox.Text.Length > 0) // this is going to be edited to check for similar or exact match company names, but for now it won't
            {
                company.Name = nameTBox.Text;
            }
            else
            {
                MessageBox.Show("No input on name. Please give a company's name before proceeding.", "Company wasn't given a name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            if (radio1.IsChecked == true)
                company.Trust = Trust.TrustWorthy;
            else if (radio2.IsChecked == true)
                company.Trust = Trust.CantTrust;
            else if (radio3.IsChecked == true)
                company.Trust = Trust.Unknown;
            else
            {
                MessageBox.Show("Trust Level not Chosen. Please Choose to proceed.", "Trust Level Not Chosen", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (govRadio1.IsChecked == true)
                company.OnGovWebsite = true;
            else if (govRadio2.IsChecked == true)
                company.OnGovWebsite = false;
            else
            {
                MessageBox.Show("Availability on Governement website not chosen. Please Choose to proceed.", "On Gov Website Not Chosen", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            company.Notes = notesTBox.Text;

            if (company.Trust == Trust.CantTrust && company.Notes.Length == 0)
            {
                MessageBox.Show("When set to Not TrustWorthy, notes must be provided explaining why they can't be trusted.", "No Notes", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ((MainWindow)Application.Current.MainWindow).AddCompany(company);

            this.Visibility = Visibility.Collapsed;

            Clear_Information();
        }


        private void Clear_Information()
        {
            nameTBox.Text = "";
            radio1.IsChecked = false;
            radio2.IsChecked = false;
            radio3.IsChecked = false;
            govRadio1.IsChecked = false;
            govRadio2.IsChecked = false;
            notesTBox.Text = "";
        }

        




    }
}
