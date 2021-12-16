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
    /// Interaction logic for DronWindow.xaml
    /// </summary>
    public partial class DronWindow : Window
    {
        IBL.BL accseccBL2;
        public DronWindow(IBL.BL accseccBL1)
        {
            InitializeComponent();
        accseccBL2=accseccBL1;
    }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
           // DroneListWindow we = new DroneListWindow(accseccBL2);
           // we.Show();
            this.Close();
        }

        private void ComboBoxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
