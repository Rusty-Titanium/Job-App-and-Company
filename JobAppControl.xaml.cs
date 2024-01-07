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

namespace Job_App_and_Company
{
    /// <summary>
    /// Interaction logic for JobAppControl.xaml
    /// </summary>
    public partial class JobAppControl : UserControl
    {

        private JobApplication application;

        public JobAppControl(JobApplication application)
        {
            InitializeComponent();

            this.application = application;
            DataContext = application;
        }













    }
}
