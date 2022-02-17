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
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    /// 
    public partial class DroneListWindow : Window
    {
       private IBL accseccBL1;
        
        public DroneListWindow(IBL accseccBL)
        {
            InitializeComponent();
            accseccBL1 = accseccBL;
            DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
            ComboBoxStatuse.ItemsSource = Enum.GetValues(typeof(BO.StatusDrone));
            ComboBoxMaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
          

        }
        
        private void Add_Drone_Click(object sender, RoutedEventArgs e)
        {
            DronWindow dr = new DronWindow(accseccBL1,this);//מקבל גם גישה וגם את החלון כדי שיוכל לסגור אותו
            dr.Show();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneToList drne = DronesListView.SelectedItem as BO.DroneToList;
            if (drne != null)
            {
                DronWindow dr = new DronWindow(accseccBL1, drne, this);
                dr.Show();
            }
            
        }

        private void comoboxByDrone()
        {
            if(ComboBoxStatuse.SelectedItem==null&& ComboBoxMaxWeight.SelectedItem == null)
            {
                DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
            }
            if(ComboBoxStatuse.SelectedIndex != -1 && ComboBoxMaxWeight.SelectedIndex != -1)//גם לפי סטטוס ולפי משקל
                DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x => x.Weight == (BO.WeightCategories)ComboBoxMaxWeight.SelectedItem&&x.StatusDrone== (BO.StatusDrone)ComboBoxStatuse.SelectedItem);
            else if (ComboBoxStatuse.SelectedIndex != -1 && ComboBoxMaxWeight.SelectedIndex == -1)//רק לפיסטטוס
                DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x => x.StatusDrone == (BO.StatusDrone)ComboBoxStatuse.SelectedItem);
            else if (ComboBoxStatuse.SelectedIndex == -1 && ComboBoxMaxWeight.SelectedIndex != -1)//רק לפי משקל
                DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x => x.Weight == (BO.WeightCategories)ComboBoxMaxWeight.SelectedItem);

        }

        private void ComboBoxMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comoboxByDrone();
           // DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x=>x.Weight==(BO.WeightCategories)ComboBoxMaxWeight.SelectedItem);
        }
        private void ComboBoxStatuse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comoboxByDrone();
            //   DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x => x.StatusDrone == (BO.StatusDrone)ComboBoxStatuse.SelectedItem);
        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("You're sure you want to close", "closr", MessageBoxButton.YesNo);

            if (a == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            ListsWindow we = new ListsWindow(accseccBL1);
            we.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ComboBoxMaxWeight.SelectedIndex!= -1)
            {
                DronesListView.ItemsSource = DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x => x.Weight == (BO.WeightCategories)ComboBoxMaxWeight.SelectedItem);//ממלא את הרשימה
                
            }
            else
                DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
            ComboBoxStatuse.SelectedIndex = -1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(ComboBoxStatuse.SelectedIndex != -1)
            {
                DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x => x.StatusDrone == (BO.StatusDrone)ComboBoxStatuse.SelectedItem);//ממלא את הרשימה
                
            }
            else 
                DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
            ComboBoxMaxWeight.SelectedIndex = -1;
        }

        
    }
}
