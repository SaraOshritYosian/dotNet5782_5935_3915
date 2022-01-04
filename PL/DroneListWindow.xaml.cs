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
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    /// 
    public partial class DroneListWindow : Window
    {
       private IBL.BL accseccBL1;
        
        public DroneListWindow(IBL.BL accseccBL)
        {
            InitializeComponent();
            //ניצרו רשימה של הרחפני ואז נכניס בדתה את הרשימה ואז כשעושים עוספת רחפן חדש אז עושים לרשימה שיצרנו אדד
            //List<Drone> dd = new List<Drone>();
            
            accseccBL1 = accseccBL;
           // DronesListView.DataContext= accseccBL1.GetDrons();
            DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
            ComboBoxStatuse.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.StatusDrone));
            ComboBoxMaxWeight.ItemsSource = Enum.GetValues(typeof(Enums.WeightCategories));

        }
        //public void Refresh(IBL.BL accseccBL)
        //{
        //    accseccBL1 = accseccBL;
        //    DroneListWindow we = new DroneListWindow(accseccBL1);
        //    DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
        //    we.Show();
        //}
        private void Add_Drone_Click(object sender, RoutedEventArgs e)
        {
            DronWindow dr = new DronWindow(accseccBL1,this);//מקבל גם גישה וגם את החלון כדי שיוכל לסגור אותו
            dr.Show();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList drne = DronesListView.SelectedItem as DroneToList;
            DronWindow dr = new DronWindow(accseccBL1, drne,this);
            dr.Show();
        }

        private void ComboBoxMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         //   DronesListView.ItemsSource = accseccBL1.GetDronsByWeight(sender as ComboBoxMaxWeight.SelectedItem);
        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBoxStatuse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//
        //    if
        }
    }
}
