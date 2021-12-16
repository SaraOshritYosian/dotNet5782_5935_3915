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
using IBL.BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for DronWindow.xaml
    /// </summary>
    public partial class DronWindow : Window
    {
        static int idDrone = 0;
        IBL.BL accseccBL2;
        public DronWindow(IBL.BL accseccBL1)
        {
            InitializeComponent();
            accseccBL2 = accseccBL1;
            ComboBoxWeight.Items.Add("Light");
            //ComboBoxWeight.ItemsSource(accseccBL2.)
            ComboBoxWeight.Items.Add("Medium");
            ComboBoxWeight.Items.Add("Heavy");
            List<int> aa = new List<int>();
           for(int i = 0; i < accseccBL2.AvailableStationToChargeList().Count(); i++)
            {
                ComboBoxStation.Items.Add(accseccBL2.AvailableStationToChargeList().ElementAt(i).Id);
            }
            
        }
   

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Drone drone1=new Drone();
            drone1.Id = idDrone;//id
            idDrone++;
            drone1.Model = (Int32.Parse(TexModel.Text)).ToString();//model
            drone1.Weight = (Enums.WeightCategories)ComboBoxWeight.SelectedIndex;//weight
            try
            {
                accseccBL2.AddDrone(drone1, ComboBoxStation.SelectedIndex);//station
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
           // DroneListWindow we = new DroneListWindow(accseccBL2);
           // we.Show();
            this.Close();
        }

        private void ComboBoxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxWeight.Items.Add("Light");
            ComboBoxWeight.Items.Add("Medium");
            ComboBoxWeight.Items.Add("Heavy");
        }
    }
}
