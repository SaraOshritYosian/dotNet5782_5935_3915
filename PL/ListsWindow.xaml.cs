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

        private void DroneList1_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindow we = new DroneListWindow(accseccBL2);
            we.Show();
            Close();
        }

        private void CostumerList_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindow we = new DroneListWindow(accseccBL2);
            we.Show();
            Close();
        }

        private void StationList_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindow we = new DroneListWindow(accseccBL2);
            we.Show();
            Close();
        }

        private void ParcelList_Click(object sender, RoutedEventArgs e)
        {
            ParcelListWindow we = new ParcelListWindow(accseccBL2);
            we.Show();
            Close();
        }
    }
}
