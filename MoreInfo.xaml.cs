using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for MoreInfo.xaml
    /// </summary>
    public partial class MoreInfo : UserControl
    {
        /**
         * Notes: appGrid will house all of the JobApplication shit and will probably take a day or 2 to do.
         * - formatting can come later. I just want something working.
         * 
         * 
         */


        private Company company;

        public MoreInfo(Company company)
        {
            InitializeComponent();

            this.company = company;

            DataContext = company;
        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Grid)this.Parent).Children.Remove(this);
        }



        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            // Currently I just stop any form of editing from happening, but will be changing that in the future probably maybe, probably not honestly.

            DataRow row = (DataRow)((DataRowView)e.Row.Item).Row;

            JobApplication jobApp = new JobApplication();

            jobApp.Title = (String)row["Title"];
            jobApp.Description = (String)row["Desctiption"];
            jobApp.Pay = (PayRange)row["Pay"];
            jobApp.Date = (DateTime)row["Date"];

            JobAppControl jobControl = new JobAppControl(jobApp);

            //mainGrid.Children.Add(more);




            e.Cancel = true;
        }






    }
}
