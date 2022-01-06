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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    /////  droneListWindow11 = droneList;//מקבל את החלון רשימת רחפן כשי שיוכל לעדכן ברענון
    //        LabelErrorTime.Visibility = Visibility.Hidden; 
    //        comoboxTime.Visibility = Visibility.Hidden;
    //        LabelTime.Visibility = Visibility.Hidden;
    //        LabelErrorMode.Visibility = Visibility.Hidden;
    //        accseccBL2 = accseccBL1;
    //        droneTo = drone;
    //        LabelId2.Content = Convert.ToString(droneTo.Id);
    //        LabelWeight2.Content = droneTo.Weight;//משקל
    //        LabeLocation2.Content = droneTo.LocationDrone;//מיקופ
    //        TexBoxModel.Text = droneTo.Model;//מודל
    //                                         //אם יש שינוי

    public partial class CustomerWindow : Window
    {
        BO.CustomerToList customerTo;
        IBL accseccBL2;

        public CustomerWindow(IBL accseccBL1, BO.CustomerToList customer, CustomerListWindow customerList )
        {
            InitializeComponent();
            GridAddCustomer.Visibility = Visibility.Hidden;
            GridUpdateCustomer.Visibility = Visibility.Visible;
            accseccBL2 = accseccBL1;
            customerTo = customer;
            LableId.Content = Convert.ToString(customerTo.Id);
            TextboxName.Text = customerTo.Name;//לעדכן
            TextboxPhone.Text = customerTo.Pone;//לעדכן
            LableLocation.Content= accseccBL2.GetCustomer(customerTo.Id).LocationOfCustomer.ToString();
            //עדכון

        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
