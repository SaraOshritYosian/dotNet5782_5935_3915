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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerable<string> passwordArr=new List<string>();
        internal readonly IBL accsessBL = BlFactory.GetBl();
        public MainWindow()
        {
            InitializeComponent();
            masengerOrCustomer.Visibility = Visibility.Visible;
            enterM.Visibility = Visibility.Hidden;
            enterC.Visibility = Visibility.Hidden;
        


        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            masengerOrCustomer.Visibility = Visibility.Hidden;
            enterM.Visibility = Visibility.Visible;
            enterC.Visibility = Visibility.Hidden;
        }

        private void Costumer_Click(object sender, RoutedEventArgs e)
        {
            masengerOrCustomer.Visibility = Visibility.Hidden;
            enterC.Visibility = Visibility.Visible;
            enterM.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//המנהל הקיש סיסמה ורוצה לפנות לרשימות
        {
            if(passwordTextm.Password=="12345")
            {
                ListsWindow we = new ListsWindow(accsessBL);
                we.Show();
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//הלקוח קיים הקיש סיסמה
        {
            IEnumerable<BO.CustomerToList> a = accsessBL.GetCustomers();
            int pp = Convert.ToInt32(passwordTextc.Password);
            if (a.Any(x => x.Id == pp))//קיים
                {
                    ListsWindow we = new ListsWindow(accsessBL,true, Convert.ToInt32(passwordTextc.Password));//שולח גם שהוא לקוח וגם את הת"ז
                    we.Show();
                    Close();
                }
            else
            {
                passwordTextc.Clear();
                MessageBox.Show("The customer does not exist in the system ");
            }  
            


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//הוספת לקוח
        {
            CustomerWindow window = new CustomerWindow(accsessBL);
            window.Show();
            Close();

        }
    }
}
