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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        private IBL accseccBL1;
        //public DroneListWindow(IBL accseccBL)
        //{
        //    InitializeComponent();
        //    //ניצרו רשימה של הרחפני ואז נכניס בדתה את הרשימה ואז כשעושים עוספת רחפן חדש אז עושים לרשימה שיצרנו אדד
        //    DronesListView.ItemsSource = accseccBL1.GetDrons();//ממלא את הרשימה
        //ComboBoxStatuse.ItemsSource = Enum.GetValues(typeof(BO.StatusDrone));
        //    ComboBoxMaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

        //}
        public CustomerListWindow(IBL accseccBL)
        {

            InitializeComponent();
            accseccBL1 = accseccBL;
            CustomerListView.ItemsSource = accseccBL1.GetCustomers();



        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)//עדכון לקוח כשלוחצים פעמיים
        {
            //BO.DroneToList drne = DronesListView.SelectedItem as BO.DroneToList;
            //DronWindow dr = new DronWindow(accseccBL1, drne, this);
            //dr.Show();
            BO.CustomerToList cl = CustomerListView.SelectedItem as BO.CustomerToList;
            CustomerWindow cw = new CustomerWindow(accseccBL1, cl, this);//לעשות את הפעולה 
            cw.Show();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)//הוספת לקוח בלחיצה אחת
        {

        }
    }
}
