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
            //ניצרו רשימה של הרחפני ואז נכניס בדתה את הרשימה ואז כשעושים עוספת רחפן חדש אז עושים לרשימה שיצרנו אדד
            //List<Drone> dd = new List<Drone>();
            
            accseccBL1 = accseccBL;
           // DronesListView.DataContext= accseccBL1.GetDrons();
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

        private void ComboBoxMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x=>x.Weight==(BO.WeightCategories)ComboBoxMaxWeight.SelectedItem);
        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
        }

        private void ComboBoxStatuse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = accseccBL1.GetDronesByPerdicate(x => x.StatusDrone == (BO.StatusDrone)ComboBoxStatuse.SelectedItem);
        }
    }
}
