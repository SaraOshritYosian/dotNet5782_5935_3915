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
        public ListsWindow(IBL accseccBL1)
        {
            InitializeComponent();
             accseccBL2 = accseccBL1;
           
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
            ParcelListWindow we = new ParcelListWindow(accseccBL2);
            we.Show();
            Close();
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
    }
}
