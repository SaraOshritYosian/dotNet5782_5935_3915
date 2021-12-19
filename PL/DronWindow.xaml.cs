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
        public DronWindow(IBL.BL accseccBL1)//add
        {
            
            InitializeComponent();
            GridAddDrone.IsEnabled = true;
            GridUpDrone.IsEnabled = false;
            accseccBL2 = accseccBL1;
            ComboBoxWeight.ItemsSource = Enum.GetValues(typeof(Enums.WeightCategories));
            List<int> aa = new List<int>();
           for(int i = 0; i < accseccBL2.AvailableStationToChargeList().Count(); i++)
            {
                ComboBoxStation.Items.Add(accseccBL2.AvailableStationToChargeList().ElementAt(i).Id);
            }
            
        }
        public DronWindow( IBL.BL accseccBL1,IBL.BO.DroneToList drone)//update
        {
            GridUpDrone.IsEnabled = true;
            GridAddDrone.IsEnabled = false;
            InitializeComponent();
            accseccBL2 = accseccBL1;
            IBL.BO.DroneToList dd = drone;
            LabelId2.DataContext = Convert.ToString(dd.Id);
            LabelWeight2.DataContext = dd.Weight;//משקל
            LabeLocation2.DataContext = dd.LocationDrone;//מיקופ
            TexBoxModel.DataContext = dd.Model;//מודל
            //אם יש שינוי
            statuse.DataContext = dd.StatusDrone;//מצב
            TexBattery.DataContext = dd.StatusBatter;//בטריה
            
            LinearGradientBrush myBrush = new LinearGradientBrush();//צבע בטריה
            myBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            myBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.5));
            myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
            if(dd.StatusDrone==enum)
            TexBattery.Background = myBrush;
            //if (dd.StatusBatter < 21)

            //    if (dd.StatusBatter < 21)
            //        TexBattery.Background = System.Drawing.Color.Red;
            //if ((dd.StatusBatter > 21)& (dd.StatusBatter<80))
            //    TexBattery.Background = System.Drawing.Color.Red;
            //if (dd.StatusBatter < 100& dd.StatusBatter>80)
            //    TexBattery.Background = #FF60F2A1;

       
        
        
        }
        private void ComboBoxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //שומר את הבחירה
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Drone drone1=new Drone();
            drone1.Id = idDrone;//id
            idDrone++;
            drone1.Model = (Int32.Parse(TexModel.Text)).ToString();//model
            drone1.Weight = (Enums.WeightCategories)ComboBoxWeight.SelectedIndex;//weight
           // try
           // {
                accseccBL2.AddDrone(drone1, ComboBoxStation.SelectedIndex);//station
          //  }
          //  catch (Exception ex)
          //  {
               // MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
           // }

            // this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
           // DroneListWindow we = new DroneListWindow(accseccBL2);
           // we.Show();
            this.Close();
        }

      
    }
}
