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
    /// Interaction logic for ListsWindow.xaml
    /// </summary>
    /// IBL accseccBL2;;
    public partial class ListsWindow : Window
    {
        IBL accseccBL2;
        bool cus;
        int idC;
        public ListsWindow(IBL accseccBL1,bool c=false,int idc=0)
        {
            InitializeComponent();
             accseccBL2 = accseccBL1;
            cus = c;
            idC = idc;
            if (cus == true& idc!=0)
            {
                
                CostumerList.Visibility = Visibility.Hidden;
                DroneList12.Visibility = Visibility.Hidden;
                CostumerList.Visibility = Visibility.Hidden;
                StationList.Visibility = Visibility.Hidden;
                ParcelList.Visibility=Visibility.Hidden;
                Addd.Visibility = Visibility.Visible;
                to.ItemsSource=accseccBL2.ListParcelFromCustomers(idc);
                From.ItemsSource = accseccBL2.ListParcelToCustomer(idc);

            }
            else
            {
                toc.Visibility = Visibility.Hidden;
                fromc.Visibility = Visibility.Hidden;
                From.Visibility = Visibility.Hidden;
                to.Visibility = Visibility.Hidden;
                Addd.Visibility = Visibility.Hidden;


            }
        }

      
        private void CostumerList_Click(object sender, RoutedEventArgs e)//customer
        {
            CustomerListWindow we = new CustomerListWindow(accseccBL2);
            we.Show();
            Close();
        }

        private void StationList_Click(object sender, RoutedEventArgs e)//station
        {
            StationListWindow we = new StationListWindow(accseccBL2);
            we.Show();
            Close();
        }

        private void ParcelList_Click(object sender, RoutedEventArgs e)//parcel
        {
            if (cus == true){
                ParcelListWindow we = new ParcelListWindow(accseccBL2,true, idC);
                we.Show();
                Close();
            }
            else
            {
                ParcelListWindow we = new ParcelListWindow(accseccBL2);
                we.Show();
                Close();
            }
          
        }

        private void DroneList12_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindow we = new DroneListWindow(accseccBL2);
            we.Show();
            Close();
        }

       

        private void DroneList12_MouseEnter(object sender, MouseEventArgs e)
        {
            DroneList12.Height = 200;
            DroneList12.Width = 450;
        }

        private void DroneList12_MouseLeave(object sender, MouseEventArgs e)
        {
            DroneList12.Height = 160;
            DroneList12.Width = 400;
        }

        private void CostumerList_MouseEnter(object sender, MouseEventArgs e)
        {
            CostumerList.Height = 200;
            CostumerList.Width = 450;
        }

        private void CostumerList_MouseLeave(object sender, MouseEventArgs e)
        {
            CostumerList.Height = 160;
            CostumerList.Width = 400;
        }

        private void StationList_MouseEnter(object sender, MouseEventArgs e)
        {
            StationList.Height = 200;
            StationList.Width = 450;
        }

        private void StationList_MouseLeave(object sender, MouseEventArgs e)
        {
            StationList.Height = 160;
            StationList.Width = 400;
        }

        private void ParcelList_MouseEnter(object sender, MouseEventArgs e)
        {
            ParcelList.Height = 200;
            ParcelList.Width = 450;
        }

        private void ParcelList_MouseLeave(object sender, MouseEventArgs e)
        {
            ParcelList.Height = 160;
            ParcelList.Width = 400;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void Addd_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow dr = new ParcelWindow(accseccBL2,true, idC,this);//מקבל גם גישה וגם את החלון כדי שיוכל לסגור אותו
            dr.Show();
            to.ItemsSource = accseccBL2.ListParcelFromCustomers(idC);
            From.ItemsSource = accseccBL2.ListParcelToCustomer(idC);
        }
    }
}
