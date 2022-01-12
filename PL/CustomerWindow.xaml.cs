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
        private readonly CustomerListWindow customerListWindow1;
        public CustomerWindow(IBL accseccBL1, CustomerListWindow customerList)//add
        {
            InitializeComponent();
            GridAddCustomer.Visibility = Visibility.Visible;
            GridUpdateCustomer.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            customerListWindow1 = customerList;




        }
        public CustomerWindow(IBL accseccBL1, BO.CustomerToList customer, CustomerListWindow customerList )//update
        {
            InitializeComponent();
            GridAddCustomer.Visibility = Visibility.Hidden;
            GridUpdateCustomer.Visibility = Visibility.Visible;
            accseccBL2 = accseccBL1;
            customerTo = customer;
          //  LableId.Content = (customer.Id).ToString();
          //  TextboxName.Text = customer.Name;//לעדכן
          //  TextboxPhone.Text = customer.Pone;//לעדכן
            LableLocation.Content= accseccBL2.GetCustomer(customer.Id).LocationOfCustomer;
            //עדכון

        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)//add customer
        {
            
            //לעשות בדיקות
            BO.Customer customer1;
            customer1 = new()
            {
                Id = Convert.ToInt32(TextBoxId.Text),
                Name = TextBoxName.Text,
                Pone = TextboxPhone.Text,
                LocationOfCustomer = new BO.Location() { Latitude = Convert.ToInt32(TextBoxLattidude.Text), Longitude = Convert.ToInt32(TextBoxLongtitude.Text) }
            };
            accseccBL2.AddCustomer(customer1);
            MessageBox.Show("The drone wad added succesfully!");
            customerListWindow1.CustomerListView.DataContext = accseccBL2.GetCustomers();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)//update
        {
            accseccBL2.UpdateCustomer(Convert.ToInt32(TextBoxId.Text), TextBoxName.Text, TextBoxPhone.Text);
            customerListWindow1.CustomerListView.ItemsSource = accseccBL2.GetCustomers();
            MessageBox.Show("The drone wad updated succesfully!");
        }
    }
    }

