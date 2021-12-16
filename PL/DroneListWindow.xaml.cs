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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
       private IBL.BL accseccBL1;
        public DroneListWindow(IBL.BL accseccBL)
        {
            InitializeComponent();
            accseccBL1 = accseccBL;
            DronesListView.ItemsSource = accseccBL1.GetDrons();
        }
        private void Add_Drone_Click(object sender, RoutedEventArgs e)
        {
            DronWindow dr = new DronWindow(accseccBL1);
            dr.Show();
        }
    }
}
