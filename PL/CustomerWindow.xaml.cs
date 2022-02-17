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
using BO;

namespace PL
{
    /// <summary>
   
   
    public partial class CustomerWindow : Window
    {
        BO.Customer customer;
        BO.CustomerToList customerTo;

        IBL accseccBL2;
        bool newcustome = false;
        private  CustomerListWindow customerListWindow1;
   
        public CustomerWindow(IBL accseccBL1, CustomerListWindow customerList)//add מקבל גישה לביאל וצקבל את החלון של רשימת הלקוחות
        {
            InitializeComponent();
            GridAddCustomer.Visibility = Visibility.Visible;
            GridUpdateCustomer.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            customerListWindow1 = customerList;
        }


        public CustomerWindow(IBL accseccBL1)//מקבל מהחלון ראשי כדי להכניס לקוח חדש שרוצה להרשם
        {
            InitializeComponent();
            GridAddCustomer.Visibility = Visibility.Visible;
            GridUpdateCustomer.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            newcustome = true;
        }
        public CustomerWindow(IBL accseccBL1, BO.CustomerToList customers, CustomerListWindow customerList)//update
        {
            InitializeComponent();
            ButtonAdd.IsEnabled = false;
            newcustome = false; ;
            customerListWindow1 = customerList;
            GridAddCustomer.Visibility = Visibility.Hidden;
            GridUpdateCustomer.Visibility = Visibility.Visible;
            accseccBL2 = accseccBL1;
            customerTo = customers;
            customer = accseccBL2.GetCustomer(customers.Id);
            GridUpdateCustomer.DataContext = customerTo;
            ListviewListOfPackagesFromTheCustomer.ItemsSource = accseccBL2.ListParcelFromCustomers(customer.Id);
            ListViewListOfPackagesToTheCustomer.ItemsSource = accseccBL2.ListParcelToCustomer(customer.Id);
           
            if (TextboxPhone.Text != null && TextBoxName.Text != null)
            {
                ButtonAdd.IsEnabled = true;
            }
         
            LableLocation.Content = accseccBL2.GetCustomer(customer.Id).LocationOfCustomer;
           // עדכון

        }


        public CustomerWindow(IBL accseccBL1, BO.CustomerToList customer )//פעולה בונה שמקבלת את הלקוח מחלון של הממשלוחים
        {
            InitializeComponent();
            ButtonAdd.IsEnabled = false;
            newcustome = false; ;
            GridAddCustomer.Visibility = Visibility.Hidden;
            GridUpdateCustomer.Visibility = Visibility.Visible;
            accseccBL2 = accseccBL1;
            customerTo = customer;
            ListviewListOfPackagesFromTheCustomer.ItemsSource = accseccBL2.ListParcelFromCustomers(customerTo.Id);
            ListViewListOfPackagesToTheCustomer.ItemsSource = accseccBL2.ListParcelToCustomer(customerTo.Id);
            TextboxName.Text = customer.Name;
            TextBoxPhone.Text = customer.Pone;
            if (TextboxPhone.Text != null && TextBoxName.Text != null)
            {
                ButtonAdd.IsEnabled = true;
            }
            /*ListParcelFromCustomers(customerTo.Id);*/

            LableId2.Content = (customer.Id).ToString();
            //  TextboxName.Text = customer.Name;//לעדכן
            //  TextboxPhone.Text = customer.Pone;//לעדכן
            LableLocation.Content = accseccBL2.GetCustomer(customer.Id).LocationOfCustomer;
            //עדכון

        }
        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)//add customer
        {

            //לעשות בדיקות
            if (accseccBL2.GetCustomers().Any(p => p.Id == Convert.ToInt32(TextBoxId.Text)))//קיים
            {
                MessageBox.Show("This customer is already exist!");
            }
            else
            {
                if (TextBoxId.Text != "" && TextBoxName.Text != "" && TextBoxPhone.Text != "" && TextBoxLattidude.Text != "" && TextBoxLongtitude.Text != "")
                {

                    BO.Customer customer1;
                    customer1 = new()
                    {
                        Id = Convert.ToInt32(TextBoxId.Text),
                        Name = TextBoxName.Text,
                        Pone = TextBoxPhone.Text,
                        LocationOfCustomer = new BO.Location() { Latitude = Convert.ToDouble(TextBoxLattidude.Text), Longitude = Convert.ToDouble(TextBoxLongtitude.Text) }
                    };
                    try
                    {
                        accseccBL2.AddCustomer(customer1);
                        MessageBox.Show("The customer wad added succesfully!");
                        if (newcustome == false)
                        {
                            customerListWindow1.CustomerListView.DataContext = accseccBL2.GetCustomers();
                        }
                        else
                        {
                            ListsWindow we = new ListsWindow(accseccBL2);
                            we.Show();
                        }
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }   
                else
                {
                    MessageBox.Show("Fill in what's missing");

                }
            }


        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)//update
        {
            try
            {
                accseccBL2.UpdateCustomer(Convert.ToInt32(LableId2.Content), TextboxName.Text, TextboxPhone.Text);
                
                customerListWindow1.CustomerListView.ItemsSource = accseccBL2.GetCustomers();
                MessageBox.Show("The customer wad updated succesfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void ListviewListOfPackagesFromTheCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)//?
        {
            ParcelInCustomer parcelInCustomer = ListviewListOfPackagesFromTheCustomer.SelectedItem as ParcelInCustomer;
           
            if (parcelInCustomer != null)
            {
                ParcelToLIst parcels = accseccBL2.ParcelToListToPrint(parcelInCustomer.Id);
                ParcelWindow p = new ParcelWindow(accseccBL2, parcels);

                p.Show();
            }

        }

        private void ListViewListOfPackagesToTheCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ParcelInCustomer parcelInCustomer = ListviewListOfPackagesFromTheCustomer.SelectedItem as ParcelInCustomer;
            if (parcelInCustomer != null)
            {
                ParcelToLIst parcels = accseccBL2.ParcelToListToPrint(parcelInCustomer.Id);
                ParcelWindow p = new ParcelWindow(accseccBL2, parcels);
                p.Show();
            }

        }

        
    }
    }

